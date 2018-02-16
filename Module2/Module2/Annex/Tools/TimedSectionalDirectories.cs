using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Charlotte.Annex.Tools
{
	public class TimedSectionalDirectories
	{
		private const int TIME_SECTION_HOURS = 2;

		private long TIME_SECTION_TICKS
		{
			get { return TIME_SECTION_HOURS * 60L * 60L * 10000000L; }
		}

		private string RootDir;
		private string CurrDir;
		private string NextDir;
		private string PrevDir;

		public TimedSectionalDirectories(string ident)
		{
			ident = IdentFilter(ident);

			this.RootDir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), ident);

			long h = DateTime.Now.Ticks / TIME_SECTION_TICKS;

			this.CurrDir = Path.Combine(this.RootDir, h.ToString());
			this.NextDir = Path.Combine(this.RootDir, (h + 1).ToString());
			this.PrevDir = Path.Combine(this.RootDir, (h - 1).ToString());

			using (new MSection(OpenGlobalMtx(ident)))
			{
				CreateDirectory_If_Not_Exists(this.RootDir);
				CreateDirectory_If_Not_Exists(this.CurrDir);
				CreateDirectory_If_Not_Exists(this.NextDir);
				CreateDirectory_If_Not_Exists(this.PrevDir);

				foreach (string dir in Directory.GetDirectories(this.RootDir))
				{
					try
					{
						long d = long.Parse(Path.GetFileName(dir));

						if (d < h - 1 || h + 1 < d)
						{
							throw null;
						}
					}
					catch
					{
						DeleteDirectory(dir);
					}
				}
			}
		}

		public string GetPath(string localName)
		{
			CheckFairIdent(localName);

			return Path.Combine(this.CurrDir, localName);
		}

		public string FindFileOrDirectory(string localName)
		{
			CheckFairIdent(localName);

			foreach (string dir in new string[]
			{
				this.CurrDir,
				this.PrevDir,
				this.NextDir,
			})
			{
				string path = Path.Combine(dir, localName);

				if (
					File.Exists(path) ||
					Directory.Exists(path)
					)
					return path;
			}
			return null;
		}

		private class MSection : IDisposable
		{
			private Mutex _m;

			public MSection(Mutex m)
			{
				_m = m;
			}

			public void Dispose()
			{
				if (_m != null)
				{
					CloseGlobalMtx(_m);

					_m = null;
				}
			}
		}

		private static Mutex OpenGlobalMtx(string ident)
		{
			int millis = 0;

			for (; ; )
			{
				try
				{
					return TryOpenGlobalMtx(ident);
				}
				catch
				{ }

				if (millis < 2000)
					millis++;

				Thread.Sleep(millis);
			}
		}

		private static Mutex TryOpenGlobalMtx(string ident)
		{
			Mutex m = null;

			try
			{
				m = new Mutex(false, @"Global\Global_" + ident); // 別ユーザーによって作成されている場合、権限が無くて例外を投げることがある。

				if (m.WaitOne(5000))
					return m;
			}
			catch
			{ }

			CloseGlobalMtx(m);

			throw new Exception("ミューテックスの取得に失敗しました。");
		}

		private static void CloseGlobalMtx(Mutex m)
		{
			try { m.ReleaseMutex(); }
			catch { }

			try { m.Close(); }
			catch { }
		}

		public static void DeleteDirectory_If_Exists(string dir)
		{
			if (Directory.Exists(dir))
			{
				DeleteDirectory(dir);
			}
		}

		public static void CreateDirectory_If_Not_Exists(string dir)
		{
			if (Directory.Exists(dir) == false)
			{
				CreateDirectory(dir);
			}
		}

		public static void DeleteDirectory(string dir)
		{
			for (int c = 0; c < 10; c++)
			{
				try
				{
					Directory.Delete(dir, true);
				}
				catch
				{ }

				if (Directory.Exists(dir) == false)
					return;

				Thread.Sleep(c * 100);
			}
			throw new Exception("ディレクトリ \"" + dir + "\" の削除に失敗しました。");
		}

		public static void CreateDirectory(string dir)
		{
			for (int c = 0; c < 10; c++)
			{
				try
				{
					Directory.CreateDirectory(dir);
				}
				catch
				{ }

				if (Directory.Exists(dir))
					return;

				Thread.Sleep(c * 100);
			}
			throw new Exception("ディレクトリ \"" + dir + "\" の作成に失敗しました。");
		}

		private static string IdentFilter(string ident)
		{
			ident = "" + ident;

			if (IsFairIdent(ident) == false)
			{
				using (SHA512 sha512 = SHA512.Create())
				{
					ident = BitConverter.ToString(sha512.ComputeHash(Encoding.UTF8.GetBytes(ident)), 0, 16).Replace("-", "");
				}
			}
			return ident;
		}

		private static void CheckFairIdent(string ident)
		{
			if (IsFairIdent(ident) == false)
			{
				throw new Exception("128ビットのハッシュ値やUUIDなどの文字列を使用して下さい。");
			}
		}

		private static bool IsFairIdent(string format)
		{
			if (format.Length < 1 || 50 < format.Length)
				return false;

			foreach (char chr in
				"_{-}" +
				"012345678" +
				"ABCDEF" +
				"abcdef"
				)
				format = format.Replace(chr, '9');

			for (int c = 0; c < 10; c++)
				format = format.Replace("99", "9");

			return format == "9";
		}
	}
}
