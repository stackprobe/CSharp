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
				Exception ex = null;

				for (int c = 1; ; c++)
				{
					try
					{
						File.Delete(path);
					}
					catch (Exception e)
					{
						ex = e;
					}

					if (File.Exists(path) == false)
						break;

					if (10 <= c)
						throw new Exception("ファイルを削除出来ません。" + path, ex);

					Thread.Sleep(c * 100);
				}
			}
			else if (Directory.Exists(path))
			{
				Exception ex = null;

				for (int c = 1; ; c++)
				{
					try
					{
						Directory.Delete(path, true);
					}
					catch (Exception e)
					{
						ex = e;
					}

					if (Directory.Exists(path) == false)
						break;

					if (10 <= c)
						throw new Exception("ディレクトリを削除出来ません。" + path, ex);

					Thread.Sleep(c * 100);
				}
			}
		}

		public static void CreateDir(string dir)
		{
			Exception ex = null;

			for (int c = 1; ; c++)
			{
				try
				{
					Directory.CreateDirectory(dir); // dirが存在するときは何もしない。
				}
				catch (Exception e)
				{
					ex = e;
				}

				if (Directory.Exists(dir))
					break;

				if (10 <= c)
					throw new Exception("ディレクトリを作成出来ません。" + dir, ex);

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
	}
}
