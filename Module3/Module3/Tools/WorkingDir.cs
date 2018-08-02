using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class WorkingDir : IDisposable
	{
		private string Dir;

		public WorkingDir()
			: this(Path.Combine(Environment.GetEnvironmentVariable("TMP"), Guid.NewGuid().ToString("B")))
		{ }

		public WorkingDir(string dir)
		{
			FileTools.Delete(dir);
			FileTools.CreateDir(dir);

			this.Dir = dir;
		}

		private long PathCounter = 0L;

		public string MakePath()
		{
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
				try { Directory.Delete(this.Dir, true); }
				catch { }

				this.Dir = null;
			}
		}
	}
}
