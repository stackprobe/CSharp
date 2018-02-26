using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;
using System.Threading;

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

				WorkingDir.Root = WorkingDir.CreateProcessRoot();

				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					Main2(args);
				}

				WorkingDir.Root.Dispose();
				WorkingDir.Root = null;
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		public static void PostMessage(object message)
		{
			Console.WriteLine("[" + DateTime.Now + "] " + message);
		}

		public const string APP_IDENT = "{ad65120b-d9a0-429a-a98e-0ccbebcfb0fd}";
		public const string APP_TITLE = "SSRBServer";

		public static string SelfFile;
		public static string SelfDir;

		public static void OnBoot()
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);
		}

		private static void Main2(string[] args)
		{
			ArgsReader ar = new ArgsReader(args);

			if (ar.ArgIs("/TSR"))
			{
				string callBatFile = ar.NextArg();
				string tsrDir = Path.GetDirectoryName(callBatFile);

				ProcessTools.Start(Path.GetFileName(callBatFile), "", tsrDir, ProcessTools.WindowStyle_e.NORMAL);

				try // Try twice
				{
					Directory.Delete(tsrDir, true);
				}
				catch
				{
					Thread.Sleep(100);

					try { Directory.Delete(tsrDir, true); }
					catch { }
				}
			}
			else if (ar.ArgIs("/TSR-SERVER"))
			{
				MRecver.MRecv(
					Consts.MSR_IDENT,
					(string callBatFile) => ProcessTools.Start(
						Path.GetFileName(SelfFile),
						"/TSR \"" + callBatFile + "\"",
						SelfDir
						),
					() => Gnd.I.StopTSRServer.WaitOne(0) == false
					);
			}
			else if (ar.ArgIs("/TSR-SERVER-S"))
			{
				Gnd.I.StopTSRServer.Set();
			}
			else if (ar.ArgIs("/SERVER"))
			{
				BatchServer server = new BatchServer(int.Parse(ar.NextArg()));

				while (Gnd.I.StopServer.WaitOne(2000) == false)
				{
					for (; ; )
					{
						Exception e = server.SockServer.GetException();

						if (e == null)
							break;

						Program.PostMessage(e);
					}
				}
				server.SockServer.Stop_B();
			}
			else if (ar.ArgIs("/S"))
			{
				Gnd.I.StopServer.Set();
			}
			else if (ar.ArgIs("/A"))
			{
				Gnd.I.AbandonCurrentRunningBatch.Set();
			}
		}
	}
}
