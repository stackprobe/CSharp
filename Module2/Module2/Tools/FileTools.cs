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
						Program.PostMessage(e);
					}
					if (File.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ファイルの削除に失敗しました。" + path);

					Thread.Sleep(c * 100);
					Program.PostMessage("ファイルの削除をリトライします。" + path);
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
						Program.PostMessage(e);
					}
					if (Directory.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ディレクトリの削除に失敗しました。" + path);

					Thread.Sleep(c * 100);
					Program.PostMessage("ディレクトリの削除をリトライします。" + path);
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
					Program.PostMessage(e);
				}
				if (Directory.Exists(dir))
					break;

				if (10 < c)
					throw new Exception("ディレクトリを作成出来ません。" + dir);

				Thread.Sleep(c * 100);
				Program.PostMessage("ディレクトリの作成をリトライします。" + dir);
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
	}
}
