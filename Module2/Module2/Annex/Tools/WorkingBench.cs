using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Charlotte.Annex.Tools
{
	public class WorkingBench
	{
		private const int TIME_SECTION_HOURS = 1;

		private long TIME_SECTION_TICKS
		{
			get { return TIME_SECTION_HOURS * 60L * 60L * 10000000L; }
		}

		private string RootDir;
		private string CurrDir;
		private string NextDir;
		private string PrevDir;

		public WorkingBench(string ident)
		{
			ident = IdentFilter(ident);

			this.RootDir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), ident);

			long h = DateTime.Now.Ticks / TIME_SECTION_TICKS;

			this.CurrDir = Path.Combine(this.RootDir, h.ToString());
			this.NextDir = Path.Combine(this.RootDir, (h + 1).ToString());
			this.PrevDir = Path.Combine(this.RootDir, (h - 1).ToString());

			using (Mutex m = new Mutex(false, ident))
			using (new MSection(m))
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
						try { Directory.Delete(dir, true); }
						catch { }
					}
				}
			}
		}

		public string MakePath()
		{
			return Path.Combine(this.CurrDir, Guid.NewGuid().ToString("B"));
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
				_m.WaitOne();
			}

			public void Dispose()
			{
				if (_m != null)
				{
					_m.ReleaseMutex();
					_m = null;
				}
			}
		}

		private static void CreateDirectory_If_Not_Exists(string dir)
		{
			if (Directory.Exists(dir) == false)
			{
				CreateDirectory(dir);
			}
		}

		private static void CreateDirectory(string dir)
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
