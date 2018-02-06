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
				OnBoot();

				Gnd.I = new Gnd();

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
#if DEBUG
				Console.WriteLine("Press ENTER");
				Console.ReadLine();
#endif
			}
		}

		public static void PostMessage(object message)
		{
			Console.WriteLine("[TRACE] " + message);
		}

		public const string APP_IDENT = "{22eda4a5-9029-4bf3-b8d8-c687a5729ec3}";
		public const string APP_TITLE = "CCCC";

		public static string SelfFile;
		public static string SelfDir;

		public static void OnBoot()
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);
		}

		private static bool ArgIs(Queue<string> argq, string ptn)
		{
			if (1 <= argq.Count && argq.Peek().ToUpper() == ptn.ToUpper())
			{
				argq.Dequeue();
				return true;
			}
			return false;
		}

		private static void Main2(string[] args)
		{
			Queue<string> argq = new Queue<string>(args);

			while (1 <= argq.Count)
			{
				if (ArgIs(argq, "/Dummy"))
				{
					Gnd.I.Dummy = argq.Dequeue();
					continue;
				}
				throw new Exception("不明な引数が指定されました。" + argq.Peek());
			}
		}
	}
}
