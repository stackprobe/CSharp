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
			FileTools.Delete(dir);
			FileTools.CreateDir(dir);

			this.Dir = dir;
		}

		public string GetDirectory()
		{
			return this.Dir;
		}

		public WorkingDir Create()
		{
			return new WorkingDir(this.MakePath());
		}

		private long MakePathCounter = 0;

		public string MakePath()
		{
			return this.GetPath("$" + this.MakePathCounter++);
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
					Utils.PostMessage(e);
				}

				this.Dir = null;
			}
		}
	}
}
