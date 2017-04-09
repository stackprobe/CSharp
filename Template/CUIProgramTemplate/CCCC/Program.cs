﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				onBoot();

				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
#if DEBUG
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
#endif
		}

		public const string APP_IDENT = "{22eda4a5-9029-4bf3-b8d8-c687a5729ec3}";
		public const string APP_TITLE = "CCCC";

		public static string selfFile;
		public static string selfDir;

		public static void onBoot()
		{
			selfFile = Assembly.GetEntryAssembly().Location;
			selfDir = Path.GetDirectoryName(selfFile);
		}

		private static void main2(string[] args)
		{
			System.Windows.Forms.MessageBox.Show("a");
		}
	}
}
