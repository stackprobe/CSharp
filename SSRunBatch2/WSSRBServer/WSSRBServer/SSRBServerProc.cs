using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte
{
	public class SSRBServerProc
	{
		private static string ProgFile
		{
			get { return Path.Combine(Program.SelfDir, "SSRBServer.exe"); }
		}

		private static Process StartProgram(string args)
		{
			ProcessStartInfo psi = new ProcessStartInfo();

			psi.FileName = ProgFile;
			psi.Arguments = args;
			psi.CreateNoWindow = true;
			psi.UseShellExecute = false;
			psi.WorkingDirectory = Program.SelfDir;

			return Process.Start(psi);
		}

		public static Process StartTSRServer()
		{
			return StartProgram("/TSR-SERVER");
		}

		public static Process StartServer()
		{
			return StartProgram("/SERVER " + Gnd.PortNo);
		}

		public static void StopTSRServer()
		{
			StartProgram("/TSR-SERVER-S").WaitForExit();
		}

		public static void StopServer()
		{
			StartProgram("/S").WaitForExit();
		}

		public static void AbandonCurrentRunningBatch()
		{
			StartProgram("/A").WaitForExit();
		}
	}
}
