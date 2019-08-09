using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public static class Logger
	{
		private static readonly object SYNCROOT = new object();

		private const long LOG_FILE_SIZE_MAX = 10000L; // 10 KB

		public static void WriteLine(object message)
		{
			string file = Path.Combine(Program.SelfDir, Path.GetFileNameWithoutExtension(Program.SelfFile) + ".log");
			string file0 = file + "0";

			lock (SYNCROOT)
			{
				try
				{
					if (File.Exists(file) && LOG_FILE_SIZE_MAX < new FileInfo(file).Length)
					{
						File.Delete(file0);
						File.Move(file, file0);
					}
					using (StreamWriter writer = new StreamWriter(file, true, Encoding.UTF8))
					{
						writer.WriteLine("[" + DateTime.Now + "] " + message);
					}
				}
				catch
				{ }
			}
		}
	}
}
