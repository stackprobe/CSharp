using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Tests.Tools;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			OnBoot();

			WorkingDir.Root = WorkingDir.CreateRoot();

			try
			{
				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					Main2(args);
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

			WorkingDir.Root.Dispose();
			WorkingDir.Root = null;
		}

		public static void WriteLog(object message)
		{
			Console.WriteLine("[TRACE] " + message);
		}

		public const string APP_IDENT = "{4add233b-a05e-4985-82ed-8dd01d8d6892}";
		public const string APP_TITLE = "Module2";

		public static string SelfFile;
		public static string SelfDir;

		private static void OnBoot()
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);
		}

		private static void Main2(string[] args)
		{
			//new LogQueueTest().Test01();
			//new LogQueueTest().Test01();
			new LogQueueTest().Test01();
		}
	}
}
