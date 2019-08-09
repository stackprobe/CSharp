using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public static class FileTools
	{
		public static void Delete(string path)
		{
			if (File.Exists(path))
			{
				for (int c = 1; ; c++)
				{
					Exception ex = null;

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
				for (int c = 1; ; c++)
				{
					Exception ex = null;

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
			for (int c = 1; ; c++)
			{
				Exception ex = null;

				try
				{
					Directory.CreateDirectory(dir);
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

		public static void MoveDir(string rDir, string wDir)
		{
			Directory.CreateDirectory(wDir);

			foreach (string file in Directory.GetFiles(rDir))
			{
				File.Move(file, Path.Combine(wDir, Path.GetFileName(file)));
			}
			foreach (string dir in Directory.GetDirectories(rDir))
			{
				MoveDir(dir, Path.Combine(wDir, Path.GetFileName(dir)));
			}
			Directory.Delete(rDir);
		}
	}
}
