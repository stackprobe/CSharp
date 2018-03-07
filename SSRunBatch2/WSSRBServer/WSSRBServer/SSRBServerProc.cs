using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class SSRBServerProc
	{
		private static string ExeFile
		{
			get
			{
				string file = "SSRBServer.exe";

				if (File.Exists(file) == false)
					file = @"..\..\..\..\SSRBServer\SSRBServer\bin\Release\SSRBServer.exe";

				return file;
			}
		}

		public static Process StartTSRServer()
		{
			return ProcessTools.Start(ExeFile, "/TSR-SERVER " + (int)Gnd.I.TSR_WinStyle);
		}

		public static Process StartServer()
		{
			return ProcessTools.Start(ExeFile, "/SERVER " + Gnd.I.PortNo);
		}

		public static void StopTSRServer()
		{
			ProcessTools.Start(ExeFile, "/TSR-SERVER-S").WaitForExit();
		}

		public static void StopServer()
		{
			ProcessTools.Start(ExeFile, "/S").WaitForExit();
		}

		public static void AbandonCurrentRunningBatch()
		{
			ProcessTools.Start(ExeFile, "/A").WaitForExit();
		}
	}
}
