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
		public static void createFile(string file)
		{
			File.WriteAllBytes(file, new byte[0]);
		}

		public static void deletePath(string path)
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

		public static string getTMP()
		{
			if (_tmp == null)
			{
				string tmp = getTMP_EnvName("TMP");

				if (tmp == null)
				{
					tmp = getTMP_EnvName("TEMP");

					if (tmp == null)
					{
						tmp = getTMP_EnvName("ProgramData");

						// 書き込みテスト -- ProgramDataってゲストでも書けるっぽい。
						{
							string dir = Path.Combine(tmp, StringTools.getUUID() + "_test");

							Directory.CreateDirectory(dir);
							Directory.Delete(dir);
						}

						if (tmp == null)
							throw null;
					}
				}
				tmp = Path.Combine(tmp, Program.APP_IDENT);
				deletePath(tmp);
				Directory.CreateDirectory(tmp);
				_tmp = tmp;
			}
			return _tmp;
		}

		private static string getTMP_EnvName(string envName)
		{
			string tmp = Environment.GetEnvironmentVariable(envName);

			if (
				tmp == null ||
				tmp.Length < 3 ||
				tmp.Substring(1, 2) != ":\\" ||
				Directory.Exists(tmp) == false ||
				tmp != Encoding.ASCII.GetString(Encoding.ASCII.GetBytes(tmp)) ||
				tmp.Contains(' ')
				)
				return null;

			return tmp;
		}

		public static void clearTMP()
		{
			if (_tmp != null)
			{
				deletePath(_tmp);
				_tmp = null;
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dir">相対パスの場合、戻り値のリストも相対パスになる。</param>
		/// <returns></returns>
		public static string[] lsFiles(string dir)
		{
			return Directory.GetFiles(dir);
		}

		public static string[] lsDirs(string dir)
		{
			return Directory.GetDirectories(dir);
		}

		public static string[] lssFiles(string dir)
		{
			return Directory.GetFiles(dir, "*", SearchOption.AllDirectories);
		}

		public static string[] lssDirs(string dir)
		{
			return Directory.GetDirectories(dir, "*", SearchOption.AllDirectories);
		}

		public static string changeRoot(string path, string rootOld, string rootNew)
		{
			rootOld = putYen(rootOld);
			rootNew = putYen(rootNew);

			if (StringTools.startsWithIgnoreCase(path, rootOld) == false)
				throw new Exception("[" + path + "] のルートは [" + rootOld + "] ではありません。");

			return rootNew + path.Substring(rootOld.Length);
		}

		public static string putYen(string path)
		{
			if (path.EndsWith("\\") == false)
				path += "\\";

			return path;
		}

		public static string makeFullPath(string path)
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

			path = putYen(path) + ".";
			path = Path.GetFullPath(path);

			return path;
		}

		public static string toFullPath(string path)
		{
			path = Path.GetFullPath(path);
			path = putYen(path) + ".";
			path = Path.GetFullPath(path);

			return path;
		}
	}
}
