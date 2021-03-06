﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	/// <summary>
	/// thread safe
	/// </summary>
	public class Logger
	{
		private const long LOG_FILE_SIZE_MAX = 1000000; // 1 MB

		private string _file;
		private string _file2;

		public Logger()
			: this(
				//Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".log")
				//Path.Combine(Environment.GetEnvironmentVariable("TMP"), Program.APP_IDENT + ".log")
				Path.Combine(Environment.GetEnvironmentVariable("TMP"), Program.LOG_IDENT + ".log")
				)
		{ }

		public Logger(string file)
		{
			file = FileTools.makeFullPath(file);
			_file = file;
			_file2 = Path.Combine(Path.GetDirectoryName(file), Path.GetFileNameWithoutExtension(file) + "0" + Path.GetExtension(file));
		}

		private object SYNCROOT = new object();

#if false // del
		public void clear()
		{
			lock (SYNCROOT)
			{
				try
				{
					File.Delete(_file);
					File.Delete(_file2);
				}
				catch
				{ }
			}
		}
#endif

		public void writeLine(object message)
		{
			lock (SYNCROOT)
			{
				try
				{
					if (File.Exists(_file) && LOG_FILE_SIZE_MAX < new FileInfo(_file).Length)
					{
						File.Delete(_file2);
						File.Move(_file, _file2);
					}

					using (StreamWriter writer = new StreamWriter(_file, true, Encoding.UTF8))
					{
						writeLine(writer, message);
					}
				}
				catch
				{ }
			}
		}

		private void writeLine(StreamWriter writer, object message)
		{
			writer.WriteLine("[" + DateTime.Now + "] " + message);
		}
	}
}
