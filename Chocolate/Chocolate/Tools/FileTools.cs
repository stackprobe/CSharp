using System;
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
			if (string.IsNullOrEmpty(dir))
				throw new Exception("作成しようとしたディレクトリは null 又は空文字列です。");

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
				throw new Exception("パス \"" + path + "\" は \"" + oldRoot + "\" の配下ではありません。");

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

		public static int CompBinFile(string file1, string file2)
		{
#if false
			byte[] hash1 = SecurityTools.GetSHA512File(file1);
			byte[] hash2 = SecurityTools.GetSHA512File(file2);

			return BinTools.Comp(hash1, hash2);
#else
			const int buffSize = 50000000; // 50 MB

			using (FileStream nb_reader1 = new FileStream(file1, FileMode.Open, FileAccess.Read))
			using (FileStream nb_reader2 = new FileStream(file2, FileMode.Open, FileAccess.Read))
			using (BufferedStream reader1 = new BufferedStream(nb_reader1, buffSize))
			using (BufferedStream reader2 = new BufferedStream(nb_reader2, buffSize))
			{
				for (; ; )
				{
					int chr1 = reader1.ReadByte();
					int chr2 = reader2.ReadByte();

					if (chr1 != chr2)
						return chr1 - chr2;

					if (chr1 == -1)
						return 0;
				}
			}
#endif
		}
	}
}
