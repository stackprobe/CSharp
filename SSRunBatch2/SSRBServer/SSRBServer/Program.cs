using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using Charlotte.Tools;
using System.Threading;
using System.Diagnostics;

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
					Program.PostMessage(e);
				}

				WorkingDir.Root.Dispose();
				WorkingDir.Root = null;
			}
			catch (Exception e)
			{
				Program.PostMessage(e);
			}
		}

		public static void PostMessage(object message)
		{
			string line = "[" + DateTime.Now + "] (PID:" + Process.GetCurrentProcess().Id + ") " + message;

			Console.WriteLine(line);

			using (Mutex m = new Mutex(false, "{46a0307c-0be5-40fc-b509-011bafac5329}"))
			using (new MtxSection(m))
			{
				try
				{
					if (File.Exists(Gnd.I.LogFile) && 10000 < new FileInfo(Gnd.I.LogFile).Length) // ? 10 KB <
					{
						File.Delete(Gnd.I.LogFile0);
						File.Move(Gnd.I.LogFile, Gnd.I.LogFile0);
					}
					using (StreamWriter writer = new StreamWriter(Gnd.I.LogFile, true, Encoding.UTF8))
					{
						writer.WriteLine(line);
					}
				}
				catch (Exception e)
				{
					Console.WriteLine(e);
				}
			}
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

				ProcessTools.Start(Path.GetFileName(callBatFile), "", tsrDir, ProcessTools.WindowStyle_e.NORMAL).WaitForExit();

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
					Consts.SERVER_2_TSR_SERVER_IDENT,
					(byte[] bCallBatFile) => ProcessTools.Start(
						Path.GetFileName(SelfFile),
						"/TSR \"" + MRecver.Deserialize(bCallBatFile) + "\"",
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
