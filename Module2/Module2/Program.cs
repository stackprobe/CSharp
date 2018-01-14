using System;
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
		public const string APP_IDENT = "{b85575ee-4b6f-4501-874f-7aeff90c4019}";
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
