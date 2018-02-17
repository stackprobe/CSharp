﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Text.RegularExpressions;

namespace Charlotte.Annex.Tools
{
	public class WorkingDirectory : IDisposable
	{
		private int TimeoutSec;

		private long TimeoutTicks
		{
			get { return this.TimeoutSec * 10000000L; }
		}

		private string RootDir;
		private string CurrDir;
		private string NextDir;
		private string PrevDir;

		public WorkingDirectory(string ident, int timeoutSec = 3600 * 2)
		{
			ident = IdentFilter(ident);

			this.TimeoutSec = Math.Max(1, timeoutSec);

			this.RootDir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), ident);

			long h = DateTime.Now.Ticks / this.TimeoutTicks;

			this.CurrDir = Path.Combine(this.RootDir, h.ToString());
			this.NextDir = Path.Combine(this.RootDir, (h + 1).ToString());
			this.PrevDir = Path.Combine(this.RootDir, (h - 1).ToString());

			using (new GlobalMtxSection(ident))
			{
				CreateDirectory_If_Not_Exists(this.RootDir);
				CreateDirectory_If_Not_Exists(this.CurrDir);
				CreateDirectory_If_Not_Exists(this.NextDir);
				CreateDirectory_If_Not_Exists(this.PrevDir);

				foreach (string dir in Directory.GetDirectories(this.RootDir))
				{
					long d;

					if (long.TryParse(Path.GetFileName(dir), out d) == false || d < h - 1 || h + 1 < d)
					{
						try { Directory.Delete(dir, true); }
						catch { }
					}
				}
			}
		}

		public void Dispose()
		{ }

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

		private class GlobalMtxSection : IDisposable
		{
			private Mutex _m;

			public GlobalMtxSection(string ident)
			{
				for (int c = 0; ; c++)
				{
					try
					{
						MutexSecurity security = new MutexSecurity();

						security.AddAccessRule(
							new MutexAccessRule(
								new SecurityIdentifier(
									WellKnownSidType.WorldSid,
									null
									),
								MutexRights.FullControl,
								AccessControlType.Allow
								)
							);

						bool createdNew;
						_m = new Mutex(false, @"Global\Global_" + ident, out createdNew, security);

						if (_m.WaitOne())
							return;

						Program.PostMessage(new Exception());
					}
					catch (Exception e)
					{
						Program.PostMessage(e);
					}

					CloseGlobalMtx(_m);
					_m = null;

					if (8 < c)
						throw new Exception("Globalミューテックスの作成に失敗しました。");

					//Thread.Sleep(100);
				}
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

		private static void CloseGlobalMtx(Mutex m)
		{
			try { m.ReleaseMutex(); }
			catch { }

			try { m.Close(); }
			catch { }
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
				try { Directory.CreateDirectory(dir); }
				catch { }

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
			if (ident == null || IsFairIdent(ident) == false)
			{
				throw new Exception("128ビットのハッシュ値やUUIDなどの文字列を使用して下さい。");
			}
		}

		private static bool IsFairIdent(string ident)
		{
			if (ident.Length < 1 || 50 < ident.Length)
				return false;

			string ALLOW_CHRS =
				"_{-}" +
				"0123456789" +
				//"ABCDEF" +
				//"abcdef";
				"ABCDEFGHIJKLMNOPQRSTUVWXYZ" +
				"abcdefghijklmnopqrstuvwxyz";

			foreach (char chr in ident)
				if (ALLOW_CHRS.Contains(chr) == false)
					return false;

			return true;
		}
	}
}
