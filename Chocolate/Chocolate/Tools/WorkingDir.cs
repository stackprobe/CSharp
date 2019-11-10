using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Tools
{
	public class WorkingDir : IDisposable
	{
		public static RootInfo Root;

		public class RootInfo
		{
			private string Dir;
			private bool Created = false;

			public RootInfo(string dir)
			{
				this.Dir = dir;
			}

			public string GetDir()
			{
				if (this.Created == false)
				{
					FileTools.Delete(this.Dir);
					FileTools.CreateDir(this.Dir);

					this.Created = true;
				}
				return this.Dir;
			}

			public void Delete()
			{
				if (this.Created)
				{
					FileTools.Delete(this.Dir);

					this.Created = false;
				}
			}
		}

		public static RootInfo CreateRoot()
		{
			return new RootInfo(Path.Combine(Environment.GetEnvironmentVariable("TMP"), ProcMain.APP_IDENT));
		}

		public static RootInfo CreateProcessRoot()
		{
			const string prefix = "{41266ce2-7655-413e-b8bb-780aaf640f4d}_";
			DeleteDebris(prefix);
			return new RootInfo(Path.Combine(Environment.GetEnvironmentVariable("TMP"), prefix + Process.GetCurrentProcess().Id));
		}

		private static void DeleteDebris(string prefix)
		{
			int idLimit = Process.GetCurrentProcess().Id;

			if (idLimit < 8)
				return;

			using (new MSection(MutexTools.CreateGlobal("{f711a91e-6cf9-4310-8f27-c9832321380e}")))
			{
				for (int c = 0; c < 100; c++)
				{
					int id = ((int)SecurityTools.CRandom.GetRandom((uint)idLimit / 4 - 1) + 1) * 4;

					if (CanGetProcessById(id) == false)
					{
						string dir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), prefix + id);

						FileTools.Delete(dir);
					}
				}
			}
		}

		private static bool CanGetProcessById(int id)
		{
			try
			{
				return Process.GetProcessById(id) != null;
			}
			catch
			{
				return false;
			}
		}

		private static long CtorCounter = 0L;

		private string Dir;

		public WorkingDir()
		{
			//this.Dir = Path.Combine(Root.GetDir(), Guid.NewGuid().ToString("B"));
			//this.Dir = Path.Combine(Root.GetDir(), SecurityTools.MakePassword_9A());
			this.Dir = Path.Combine(Root.GetDir(), "$" + CtorCounter++);

			FileTools.CreateDir(this.Dir);
		}

		private long PathCounter = 0L;

		public string MakePath()
		{
			//return this.GetPath(Guid.NewGuid().ToString("B"));
			//return this.GetPath(SecurityTools.MakePassword_9A());
			return this.GetPath("$" + this.PathCounter++);
		}

		public string GetPath(string localName)
		{
			return Path.Combine(this.Dir, localName);
		}

		public void Dispose()
		{
			if (this.Dir != null)
			{
				try
				{
					Directory.Delete(this.Dir, true);
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}

				this.Dir = null;
			}
		}
	}
}
