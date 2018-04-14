﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Tools
{
	public class WorkingDir : IDisposable
	{
		public static WorkingDir Root;

		public static WorkingDir CreateRoot()
		{
			return new WorkingDir(Path.Combine(Environment.GetEnvironmentVariable("TMP"), Program.APP_IDENT));
		}

		public static WorkingDir CreateProcessRoot()
		{
			return new WorkingDir(Path.Combine(Environment.GetEnvironmentVariable("TMP"), Program.APP_IDENT + "_" + Process.GetCurrentProcess().Id));
		}

		private string Dir;

		public WorkingDir(string dir)
		{
#if true
			FileTools.Delete(dir);
			FileTools.CreateDir(dir);
#else
			if (Directory.Exists(dir))
				Directory.Delete(dir, true);

			Directory.CreateDirectory(dir);
#endif

			this.Dir = dir;
		}

		public WorkingDir Create()
		{
			return new WorkingDir(this.MakePath());
		}

		//private readonly object MakePath_SYNCROOT = new object();
		private long MakePathCounter = 0;

		public string MakePath()
		{
			//lock (this.MakePath_SYNCROOT)
			{
				//return this.GetPath(Guid.NewGuid().ToString("B"));
				//return this.GetPath(SecurityTools.MakePassword_9A());
				return this.GetPath("$" + this.MakePathCounter++);
			}
		}

		public string GetPath(string localName)
		{
			return Path.Combine(this.Dir, localName);
		}

		public void Dispose()
		{
			if (this.Dir != null)
			{
				FileTools.Delete(this.Dir);
				this.Dir = null;
			}
		}
	}
}