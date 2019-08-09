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
						Program.WriteLog(e);
					}
					if (File.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ファイルの削除に失敗しました。" + path);

					Program.WriteLog("ファイルの削除をリトライします。" + path);

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
						Program.WriteLog(e);
					}
					if (Directory.Exists(path) == false)
						break;

					if (10 < c)
						throw new Exception("ディレクトリの削除に失敗しました。" + path);

					Program.WriteLog("ディレクトリの削除をリトライします。" + path);

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
					Program.WriteLog(e);
				}
				if (Directory.Exists(dir))
					break;

				if (10 < c)
					throw new Exception("ディレクトリを作成出来ません。" + dir);

				Program.WriteLog("ディレクトリの作成をリトライします。" + dir);

				Thread.Sleep(c * 100);
			}
		}
	}
}
