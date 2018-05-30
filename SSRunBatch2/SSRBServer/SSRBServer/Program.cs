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
					Utils.PostMessage(e);
				}

				WorkingDir.Root.Dispose();
				WorkingDir.Root = null;
			}
			catch (Exception e)
			{
				Utils.PostMessage(e);
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
				Utils.PostMessage("バッチファイルを起動しました。(TSR)");

				string callBatFile = ar.NextArg();
				string tsrDir = Path.GetDirectoryName(callBatFile);
				ProcessTools.WindowStyle_e winStyle = (ProcessTools.WindowStyle_e)int.Parse(ar.NextArg());

				Utils.PostMessage("TSR_callBatFile: " + callBatFile);
				Utils.PostMessage("TSR_winStyle: " + winStyle);

				ProcessTools.Start("cmd", "/c " + Path.GetFileName(callBatFile), tsrDir, winStyle).WaitForExit();
				//ProcessTools.Start(Path.GetFileName(callBatFile), "", tsrDir, winStyle).WaitForExit(); // winStyle == INVISIBLE のとき例外を投げる。

				Utils.PostMessage("TSR_Ended");

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

				Utils.PostMessage("バッチファイルは終了しました。(TSR)");
			}
			else if (ar.ArgIs("/TSR-SERVER"))
			{
				Utils.PostMessage("/TSR-SERVER Started");

				int winStyle = int.Parse(ar.NextArg());

				Gnd.I.StopTSRServer.WaitOne(0); // reset

				MRecver.MRecv(
					Consts.SERVER_2_TSR_SERVER_IDENT,
					(byte[] bCallBatFile) => ProcessTools.Start(
						Path.GetFileName(SelfFile),
						"/TSR \"" + MRecver.Deserialize(bCallBatFile) + "\" " + winStyle,
						SelfDir
						),
					() => Gnd.I.StopTSRServer.WaitOne(0) == false
					);

				Utils.PostMessage("/TSR-SERVER Ended");
			}
			else if (ar.ArgIs("/TSR-SERVER-S"))
			{
				Gnd.I.StopTSRServer.Set();
			}
			else if (ar.ArgIs("/SERVER"))
			{
				Gnd.I.StopServer.WaitOne(0); // reset

				SockServer.Backlog = int.Parse(ar.NextArg());

				Utils.PostMessage("/SERVER Starting...");
				BatchServer server = new BatchServer(int.Parse(ar.NextArg()));
				Utils.PostMessage("/SERVER Started");

				while (Gnd.I.StopServer.WaitOne(2000) == false)
				{
					for (; ; )
					{
						Exception e = server.SockServer.GetException();

						if (e == null)
							break;

						Utils.PostMessage(e);
					}
				}
				Utils.PostMessage("/SERVER Ending...");
				server.SockServer.Stop_B();
				Utils.PostMessage("/SERVER Ended");
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
