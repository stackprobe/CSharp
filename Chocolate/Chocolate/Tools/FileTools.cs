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
			if (string.IsNullOrEmpty(path))
				throw new Exception("削除しようとしたパスは null 又は空文字列です。");

			if (File.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					try
					{
						File.Delete(path);

						if (File.Exists(path) == false)
							break;
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e);
					}
					if (10 < c)
						throw new Exception("ファイルの削除に失敗しました。" + path);

					ProcMain.WriteLog("ファイルの削除をリトライします。" + path);

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

						if (Directory.Exists(path) == false)
							break;
					}
					catch (Exception e)
					{
						ProcMain.WriteLog(e);
					}
					if (10 < c)
						throw new Exception("ディレクトリの削除に失敗しました。" + path);

					ProcMain.WriteLog("ディレクトリの削除をリトライします。" + path);

					Thread.Sleep(c * 100);
				}
			}
		}

		public static void CreateDir(string dir)
		{
			if (string.IsNullOrEmpty(dir))
				throw new Exception("作成しようとしたディレクトリは null 又は空文字列です。");

			for (int c = 1; ; c++)
			{
				try
				{
					Directory.CreateDirectory(dir); // dirが存在するときは何もしない。

					if (Directory.Exists(dir))
						break;
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}
				if (10 < c)
					throw new Exception("ディレクトリを作成出来ません。" + dir);

				ProcMain.WriteLog("ディレクトリの作成をリトライします。" + dir);

				Thread.Sleep(c * 100);
			}
		}

		public static void CleanupDir(string dir)
		{
			if (string.IsNullOrEmpty(dir))
				throw new Exception("配下を削除しようとしたディレクトリは null 又は空文字列です。");

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
			CreateDir(wDir);

			foreach (string dir in Directory.GetDirectories(rDir))
				CopyDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));

			foreach (string file in Directory.GetFiles(rDir))
				File.Copy(file, Path.Combine(wDir, Path.GetFileName(file)));
		}

		public static void MoveDir(string rDir, string wDir)
		{
			CreateDir(wDir);

			foreach (string dir in Directory.GetDirectories(rDir))
				MoveDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));

			foreach (string file in Directory.GetFiles(rDir))
				File.Move(file, Path.Combine(wDir, Path.GetFileName(file)));

			Delete(rDir);
		}

		public static string ChangeRoot(string path, string oldRoot, string rootNew)
		{
			oldRoot = PutYen(oldRoot);
			rootNew = PutYen(rootNew);

			if (StringTools.StartsWithIgnoreCase(path, oldRoot) == false)
				throw new Exception("パスの配下ではありません。" + oldRoot + " -> " + path);

			return rootNew + path.Substring(oldRoot.Length);
		}

		public static string PutYen(string path)
		{
			if (path.EndsWith("\\") == false)
				path += "\\";

			return path;
		}

		public static string MakeFullPath(string path)
		{
			if (path == null)
				throw new Exception("パスが定義されていません。(null)");

			if (path == "")
				throw new Exception("パスが定義されていません。(空文字列)");

			path = Path.GetFullPath(path);

			if (path.Contains('/'))
				throw null;

			if (path.StartsWith("\\\\"))
				throw new Exception("ネットワークパスまたはデバイス名は使用出来ません。");

			if (path.Substring(1, 2) != ":\\")
				throw null;

			path = PutYen(path) + ".";
			path = Path.GetFullPath(path);

			return path;
		}

		public static string ToFullPath(string path)
		{
			path = Path.GetFullPath(path);
			path = PutYen(path) + ".";
			path = Path.GetFullPath(path);

			return path;
		}

		public static void Write(FileStream writer, byte[] data, int offset = 0)
		{
			writer.Write(data, offset, data.Length - offset);
		}
	}
}
