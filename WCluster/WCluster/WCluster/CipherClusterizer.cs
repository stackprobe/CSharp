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
					throw new Exception("復号に失敗しました。");

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

		private int Cipher(string file, bool encFlag)
		{
			string prmFile = GetTempPath();

			File.WriteAllLines(prmFile, new string[]
			{
				"/KB",
				"*" + Passphrase,
				encFlag ? "/E+" : "/D+",
				file,
			});

			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = GetZClusterFile();
			psi.Arguments = "//R \"" + prmFile + "\"";
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;

			Process p = Process.Start(psi);
			p.WaitForExit();
			int ret = p.ExitCode;

			File.Delete(prmFile);

			return ret;
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
