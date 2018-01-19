﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Reflection;
using System.IO;
using Charlotte.Tests.Tools;
using Charlotte.Tools;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{2a8d45e6-b40e-49e3-ab14-1d30635e09af}";
		public const string APP_TITLE = "Module2";

		public static string SelfDir;
		public static string SelfFile;

		static void Main(string[] args)
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);

			try
			{
				Main2();
				Console.WriteLine("OK!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();

			// ----

			WorkingDir.Root.Dispose();
			WorkingDir.Root = null;
		}

		private static void Main2()
		{
			//new WorkingDirTest().Test01();
			new Test01().Main01();
		}
	}
}