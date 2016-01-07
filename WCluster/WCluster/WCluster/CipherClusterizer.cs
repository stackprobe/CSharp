using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace WCluster
{
	public class CipherClusterizer : Clusterizer
	{
		private string Passphrase;

		public CipherClusterizer(string passphrase)
		{
			Passphrase = passphrase;
		}

		public override void DirectoryToFile(string rDir, string wFile)
		{
			string midFile = GetTempPath();

			try
			{
				base.DirectoryToFile(rDir, midFile);
				Cipher(midFile, true);
				File.Move(midFile, wFile);
			}
			finally
			{
				File.Delete(midFile);
			}
		}

		public override void FileToDirectory(string rFile, string wDir)
		{
			string midFile = GetTempPath();

			try
			{
				File.Copy(rFile, midFile);

				if (Cipher(midFile, false) != 0)
					throw new Exception("復号に失敗しました。パスフレーズに間違があるか、入力ファイルが破損しています。");

				base.FileToDirectory(midFile, wDir);
			}
			finally
			{
				File.Delete(midFile);
			}
		}

		private string GetTempPath()
		{
			return Path.Combine(Environment.GetEnvironmentVariable("TMP"), Guid.NewGuid().ToString("B"));
		}

		private static readonly Encoding Encoding_SJIS = Encoding.GetEncoding(932);

		private int Cipher(string file, bool encFlag)
		{
			string exeFile = null;
			string prmFile = null;

			try
			{
				if (IsFairPassphrase(Passphrase) == false)
					throw new Exception("パスフレーズの書式に問題があります。");

				exeFile = GetTempPath() + ".exe";
				prmFile = GetTempPath() + ".prm";

				File.Copy(GetZClusterFile(), exeFile);

				File.WriteAllLines(
					prmFile,
					new string[]
					{
						"/KB",
						"*" + Passphrase,
						encFlag ? "/E+" : "/D+",
						file,
					},
					Encoding_SJIS
					);

				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = exeFile;
				psi.Arguments = "//R \"" + prmFile + "\"";
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;

				Process p = Process.Start(psi);
				p.WaitForExit();
				int ret = p.ExitCode;

				return ret;
			}
			finally
			{
				if (exeFile != null)
					File.Delete(exeFile);

				if (prmFile != null)
					File.Delete(prmFile);
			}
		}

		public static bool IsFairPassphrase(string passphrase)
		{
			// 空文字列は /KB * オプションに指定出来ない！

			if (passphrase == "")
				return false;

			if (passphrase[0] <= ' ')
				return false;

			if (passphrase[passphrase.Length - 1] <= ' ')
				return false;

			foreach (char chr in passphrase)
				if (chr < ' ')
					return false;

			return true;
		}

		private static string ZClusterFile;

		public static string GetZClusterFile()
		{
			if (ZClusterFile == null)
			{
				string selfFile = System.Reflection.Assembly.GetEntryAssembly().Location;
				string selfDir = Path.GetDirectoryName(selfFile);

				string file = Path.Combine(selfDir, "ZCluster.exe");

				if (File.Exists(file) == false)
					file = @"C:\Factory\Tools\ZCluster.exe";

				ZClusterFile = file;
			}
			return ZClusterFile;
		}
	}
}
