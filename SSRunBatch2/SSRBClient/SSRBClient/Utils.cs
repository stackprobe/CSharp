using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Utils
	{
		public static void PostMessage(object message)
		{
			Console.WriteLine("[TRACE] " + message);
		}
	}
}
