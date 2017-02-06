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
		public static void CreateFile(string file)
		{
			File.WriteAllBytes(file, new byte[0]);
		}

		public static void DeletePath(string path)
		{
			for (int c = 0; File.Exists(path) || Directory.Exists(path); c++)
			{
				if (20 < c)
					throw new Exception("[" + path + "] の削除に失敗しました。");

				if (1 <= c)
					Thread.Sleep(100);

				try
				{
					File.Delete(path);
				}
				catch
				{ }

				try
				{
					Directory.Delete(path, true);
				}
				catch
				{ }
			}
		}

		private static string _tmp = null;

		public static string GetTMP()
		{
			if (_tmp == null)
			{
				string tmp = Environment.GetEnvironmentVariable("TMP");

				if (
					tmp == null ||
					tmp.Length < 3 ||
					tmp.Substring(1, 2) != ":\\" ||
					Directory.Exists(tmp) == false ||
					tmp != Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(tmp)) ||
					tmp.Contains(' ')
					)
				{
					tmp = Environment.GetEnvironmentVariable("ProgramData");

					if (
						tmp == null ||
						tmp.Length < 3 ||
						tmp.Substring(1, 2) != ":\\" ||
						Directory.Exists(tmp) == false ||
						tmp != Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(tmp)) ||
						tmp.Contains(' ')
						)
					{
						tmp = Environment.GetEnvironmentVariable("SystemDrive");

						if (
							tmp == null ||
							tmp.Length != 2 ||
							tmp[1] != ':'
							)
							throw null;

						tmp += "\\";
					}
				}
				tmp = Path.Combine(tmp, Program.APP_IDENT);
				DeletePath(tmp);
				Directory.CreateDirectory(tmp);
				_tmp = tmp;
			}
			return _tmp;
		}

		public static void ClearTMP()
		{
			if (_tmp != null)
			{
				DeletePath(_tmp);
				_tmp = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dir">相対パスの場合、戻り値のリストも相対パスになる。</param>
		/// <returns></returns>
		public static string[] LsFiles(string dir)
		{
			return Directory.GetFiles(dir);
		}

		public static string[] LsDirs(string dir)
		{
			return Directory.GetDirectories(dir);
		}

		public static string[] LssFiles(string dir)
		{
			return Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
		}

		public static string[] LssDirs(string dir)
		{
			return Directory.GetDirectories(dir, "*", SearchOption.AllDirectories);
		}

		public static string ChangeRoot(string path, string rootOld, string rootNew)
		{
			rootOld = PutYen(rootOld);
			rootNew = PutYen(rootNew);

			if (StringTools.StartsWithIgnoreCase(path, rootOld) == false)
				throw new Exception("[" + path + "] のルートは [" + rootOld + "] ではありません。");

			return rootNew + path.Substring(rootOld.Length);
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
	}
}
