using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SyncDir
{
	public static class Logger
	{
		public static void WriteLine(object message)
		{
			Console.WriteLine("[" + DateTime.Now + "] " + message);
		}
	}
}
