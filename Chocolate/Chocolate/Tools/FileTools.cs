﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class FileTools
	{
		public static void Delete(string path)
		{
			if (File.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						File.Delete(path);
					}
					catch (Exception e)
					{
						Common.WriteLog(e);
					}
					if (File.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ファイルの削除に失敗しました。" + path);

					Common.WriteLog("ファイルの削除をリトライします。" + path);

					Thread.Sleep(c * 100);
				}
			}
			else if (Directory.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						Directory.Delete(path, true);
					}
					catch (Exception e)
					{
						Common.WriteLog(e);
					}
					if (Directory.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ディレクトリの削除に失敗しました。" + path);

					Common.WriteLog("ディレクトリの削除をリトライします。" + path);

					Thread.Sleep(c * 100);
				}
			}
		}

		public static void CreateDir(string dir)
		{
			for (int c = 1; ; c++)
			{
				try
				{
					Directory.CreateDirectory(dir); // dirが存在するときは何もしない。
				}
				catch (Exception e)
				{
					Common.WriteLog(e);
				}
				if (Directory.Exists(dir))
					break;

				if (10 < c)
					throw new Exception("ディレクトリを作成出来ません。" + dir);

				Common.WriteLog("ディレクトリの作成をリトライします。" + dir);

				Thread.Sleep(c * 100);
			}
		}

		public static void CleanupDir(string dir)
		{
			foreach (Func<IEnumerable<string>> getPaths in new Func<IEnumerable<string>>[]
			{
				() => Directory.EnumerateFiles(dir),
				() => Directory.EnumerateDirectories(dir),
			})
			{
				foreach (string path in getPaths())
				{
					Delete(path);
				}
			}
		}

		public static void CopyDir(string rDir, string wDir)
		{
			FileTools.CreateDir(wDir);

			foreach (string dir in Directory.GetDirectories(rDir))
				CopyDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));

			foreach (string file in Directory.GetFiles(rDir))
				File.Copy(file, Path.Combine(wDir, Path.GetFileName(file)));
		}

		public static void MoveDir(string rDir, string wDir)
		{
			FileTools.CreateDir(wDir);

			foreach (string dir in Directory.GetDirectories(rDir))
				MoveDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));

			foreach (string file in Directory.GetFiles(rDir))
				File.Move(file, Path.Combine(wDir, Path.GetFileName(file)));

			FileTools.Delete(rDir);
		}
	}
}