using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{f65314a5-60f0-43b4-9086-998d300e9de4}";
		public const string APP_TITLE = "ConsoleInputTest";

		public static string selfFile
		{
			get { return Assembly.GetEntryAssembly().Location; }
		}

		public static string selfDir
		{
			get { return Path.GetDirectoryName(selfFile); }
		}
	}
}
