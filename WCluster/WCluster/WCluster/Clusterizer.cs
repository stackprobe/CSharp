﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.IO.Compression;

namespace WCluster
{
	public class Clusterizer
	{
		private GZipStream Wfs;
		private byte[] RWBuff = new byte[1024 * 1024 * 4];

		public virtual void DirectoryToFile(string rDir, string wFile)
		{
			rDir = Path.GetFullPath(rDir);
			wFile = Path.GetFullPath(wFile);

			using (FileStream wfs = new FileStream(wFile, FileMode.Create, FileAccess.Write))
			using (GZipStream gzs = new GZipStream(wfs, CompressionMode.Compress))
			{
				Wfs = gzs;
				IntoDir(rDir);
				Wfs = null;
			}
		}

		private void IntoDir(string rDir)
		{
			foreach (string dir in Directory.GetDirectories(rDir))
			{
				string lDir = Path.GetFileName(dir);
				byte[] bLDir = Encoding.UTF8.GetBytes(lDir);
				string aDir = Path.Combine(rDir, lDir);

				Wfs.WriteByte(0x0d);
				Add(GetBytes(RWBuff, (ulong)bLDir.Length), 8);
				Add(bLDir);
				Add(GetBytes(RWBuff, (ulong)File.GetAttributes(aDir)), 8);
				Add(GetBytes(RWBuff, GetValue(Directory.GetCreationTimeUtc(aDir))), 8);

				IntoDir(aDir);

				Wfs.WriteByte(0x0e);
			}
			foreach (string file in Directory.GetFiles(rDir))
			{
				string lFile = Path.GetFileName(file);
				byte[] bLFile = Encoding.UTF8.GetBytes(lFile);
				string aFile = Path.Combine(rDir, lFile);

				Wfs.WriteByte(0x0f);
				Add(GetBytes(RWBuff, (ulong)bLFile.Length), 8);
				Add(bLFile);
				Add(GetBytes(RWBuff, (ulong)File.GetAttributes(aFile)), 8);
				Add(GetBytes(RWBuff, GetValue(File.GetCreationTimeUtc(aFile))), 8);
				Add(GetBytes(RWBuff, GetValue(File.GetLastAccessTimeUtc(aFile))), 8);
				Add(GetBytes(RWBuff, GetValue(File.GetLastWriteTimeUtc(aFile))), 8);
				Add(GetBytes(RWBuff, (ulong)new FileInfo(aFile).Length), 8);

				using (FileStream rfs = new FileStream(aFile, FileMode.Open, FileAccess.Read))
				{
					for (; ; )
					{
						int readSize = rfs.Read(RWBuff, 0, RWBuff.Length);

						if (readSize <= 0)
							break;

						Wfs.Write(RWBuff, 0, readSize);
					}
				}
			}
		}

		private ulong GetValue(DateTime time)
		{
			return (ulong)((time - DateTime.MinValue).TotalSeconds + 0.5);
		}

		private byte[] GetBytes(byte[] buff, ulong value)
		{
			buff[0] = (byte)(value >> 0);
			buff[1] = (byte)(value >> 8);
			buff[2] = (byte)(value >> 16);
			buff[3] = (byte)(value >> 24);
			buff[4] = (byte)(value >> 32);
			buff[5] = (byte)(value >> 40);
			buff[6] = (byte)(value >> 48);
			buff[7] = (byte)(value >> 56);

			return buff;
		}

		private void Add(byte[] block)
		{
			Add(block, block.Length);
		}

		private void Add(byte[] block, int size)
		{
			Wfs.Write(block, 0, size);
		}

		private GZipStream Rfs;

		public virtual void FileToDirectory(string rFile, string wDir)
		{
			rFile = Path.GetFullPath(rFile);
			wDir = Path.GetFullPath(wDir);

			using (FileStream rfs = new FileStream(rFile, FileMode.Open, FileAccess.Read))
			using (GZipStream gzs = new GZipStream(rfs, CompressionMode.Decompress))
			{
				Rfs = gzs;
				WriteDir(wDir);
				Rfs = null;
			}
		}

		private void WriteDir(string wDir)
		{
			if (Directory.Exists(wDir) == false)
				Directory.CreateDirectory(wDir);

			for (; ; )
			{
				int chr = Rfs.ReadByte();

				if (chr == -1 || chr == 0x0e)
					break;

				if (chr != 0x0d && chr != 0x0f)
					throw new Exception("エントリー種別エラー");

				ulong bLPathLen = GetValue(Next(RWBuff, 8));

				if (1000 < bLPathLen)
					throw new Exception("ローカル名の長さエラー");

				string lPath = Encoding.UTF8.GetString(Next(RWBuff, (int)bLPathLen), 0, (int)bLPathLen);

				if (!IsFairLocalPath(lPath))
					throw new Exception("ローカル名のエラー");

				string aPath = Path.Combine(wDir, lPath);

				if (chr == 0x0d)
				{
					FileAttributes attr = (FileAttributes)GetValue(Next(RWBuff, 8));
					DateTime creationTimeUtc = GetDateTime(GetValue(Next(RWBuff, 8)));

					WriteDir(aPath);

					Directory.SetCreationTimeUtc(aPath, creationTimeUtc);
					File.SetAttributes(aPath, attr); // 最後に！
				}
				else // ? chr == 0x0f
				{
					FileAttributes attr = (FileAttributes)GetValue(Next(RWBuff, 8));
					DateTime creationTimeUtc = GetDateTime(GetValue(Next(RWBuff, 8)));
					DateTime lastAccessTimeUtc = GetDateTime(GetValue(Next(RWBuff, 8)));
					DateTime lastWriteTimeUtc = GetDateTime(GetValue(Next(RWBuff, 8)));
					ulong fileSize = GetValue(Next(RWBuff, 8));

					using (FileStream wfs = new FileStream(aPath, FileMode.Create, FileAccess.Write))
					{
						for (ulong rPos = 0; rPos < fileSize; )
						{
							int readSize = (int)Math.Min((ulong)RWBuff.Length, fileSize - rPos);
							wfs.Write(Next(RWBuff, readSize), 0, readSize);
							rPos += (ulong)readSize;
						}
					}
					File.SetCreationTimeUtc(aPath, creationTimeUtc);
					File.SetLastAccessTimeUtc(aPath, lastAccessTimeUtc);
					File.SetLastWriteTimeUtc(aPath, lastWriteTimeUtc);
					File.SetAttributes(aPath, attr); // 最後にやること。書き込み禁止の場合、日時の変更が出来なくなる！
				}
			}
		}

		private byte[] Next(byte[] buff, int size)
		{
			if (Rfs.Read(buff, 0, size) != size)
			{
				throw new Exception("読み込みエラー");
			}
			return buff;
		}

		private ulong GetValue(byte[] buff)
		{
			return
				((ulong)buff[0] << 0) |
				((ulong)buff[1] << 8) |
				((ulong)buff[2] << 16) |
				((ulong)buff[3] << 24) |
				((ulong)buff[4] << 32) |
				((ulong)buff[5] << 40) |
				((ulong)buff[6] << 48) |
				((ulong)buff[7] << 56);
		}

		private DateTime GetDateTime(ulong sec)
		{
			return DateTime.MinValue + TimeSpan.FromSeconds((double)sec);
		}

		private const string LPATH_NG_CHRS = "\"*/:<>?\\|";

		public static bool IsFairLocalPath(string lPath)
		{
			return
				//lPath != "." &&
				//lPath != ".." &&
				lPath.EndsWith(".") == false &&
				ContainsChars(lPath, LPATH_NG_CHRS) == false &&
				IsWindowsReserveLocalPath(lPath) == false;
		}

		private static bool ContainsChars(string str, string chrs)
		{
			foreach (char chr in chrs)
				if (str.Contains(chr))
					return true;

			return false;
		}

		private static string[] WindowsReserveNodeList;

		public static bool IsWindowsReserveLocalPath(string lPath)
		{
			if (WindowsReserveNodeList == null)
			{
				List<string> buff = new List<string>();

				buff.Add("AUX");
				buff.Add("CON");
				buff.Add("NUL");
				buff.Add("PRN");

				for (int i = 1; i <= 9; i++)
				{
					buff.Add("COM" + i);
					buff.Add("LPT" + i);
				}

				// グレーゾーン {
				buff.Add("COM0");
				buff.Add("LPT0");
				buff.Add("CLOCK$");
				buff.Add("CONFIG$");
				// }

				WindowsReserveNodeList = buff.ToArray();
			}
			lPath = lPath.ToUpper();

			foreach (string wrn in WindowsReserveNodeList)
				if (lPath.Equals(wrn) || lPath.StartsWith(wrn + "."))
					return true;

			return false;
		}
	}
}