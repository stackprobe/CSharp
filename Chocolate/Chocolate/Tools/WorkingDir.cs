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
			private bool DirCreated = false;

			public RootInfo(string dir)
			{
				this.Dir = dir;
			}

			public string GetDir()
			{
				if (this.DirCreated == false)
				{
					FileTools.Delete(this.Dir);
					FileTools.CreateDir(this.Dir);

					this.DirCreated = true;
				}
				return this.Dir;
			}
		}

		public static RootInfo CreateRoot()
		{
			return new RootInfo(Path.Combine(Environment.GetEnvironmentVariable("TMP"), ProcMain.APP_IDENT));
		}

		public static RootInfo CreateProcessRoot()
		{
			return new RootInfo(Path.Combine(Environment.GetEnvironmentVariable("TMP"), "{41266ce2-7655-413e-b8bb-780aaf640f4d}_" + Process.GetCurrentProcess().Id));
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
