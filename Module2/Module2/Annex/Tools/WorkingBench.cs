using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;

namespace Charlotte.Annex.Tools
{
	public class WorkingBench : IDisposable
	{
		private const int W_DIR_MAX = 20;

		private string Dir;
		private Mutex GlobalMtx;

		public WorkingBench(string ident)
		{
			ident = IdentFilter(ident);

			for (int c = 0; c < W_DIR_MAX; c++)
			{
				try
				{
					using (new MSection(TryOpenGlobalMtx(ident + c)))
					{
						DeleteDirectory_If_Exists(GetWorkingDirectory(ident, c));
					}
				}
				catch
				{ }
			}
			int millis = 0;

			for (; ; )
			{
				for (int c = 0; c < W_DIR_MAX; c++)
				{
					try
					{
						this.GlobalMtx = TryOpenGlobalMtx(ident + c);

						try
						{
							this.Dir = GetWorkingDirectory(ident, c);

							DeleteDirectory_If_Exists(this.Dir);
							CreateDirectory(this.Dir);

							return;
						}
						catch
						{ }

						CloseGlobalMtx(this.GlobalMtx);

						this.GlobalMtx = null;
					}
					catch
					{ }
				}
				if (millis < 2000)
					millis++;

				Thread.Sleep(millis);
			}
		}

		private static string GetWorkingDirectory(string ident, int c)
		{
			return Path.Combine(Environment.GetEnvironmentVariable("TMP"), ident + "_" + c);
		}

		public string MakePath()
		{
			return this.GetPath(Guid.NewGuid().ToString("B"));
		}

		public string GetPath(string localName)
		{
			return Path.Combine(this.Dir, localName);
		}

		public void Dispose()
		{
			if (this.GlobalMtx != null)
			{
				try { Directory.Delete(this.Dir, true); }
				catch { }

				CloseGlobalMtx(this.GlobalMtx);

				this.GlobalMtx = null;
			}
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

		private static Mutex TryOpenGlobalMtx(string ident)
		{
			Mutex m = null;

			try
			{
				m = new Mutex(false, @"Global\Global_" + ident); // 別ユーザーによって作成されている場合、権限が無くて例外を投げることがある。

				if (m.WaitOne(0))
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

		private static bool IsFairIdent(string format)
		{
			if (format.Length < 1 || 40 < format.Length)
				return false;

			foreach (char chr in
				"{-}" +
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
