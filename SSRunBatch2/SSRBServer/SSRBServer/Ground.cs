using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace Charlotte
{
	public class Gnd
	{
		public static Gnd I;

		public string LogFile;
		public string LogFile0;

		public Gnd()
		{
			this.LogFile = Path.Combine(Program.SelfDir, Path.GetFileNameWithoutExtension(Program.SelfFile) + ".log");
			this.LogFile0 = this.LogFile + "0";
		}

		public EventWaitHandle StopServer = new EventWaitHandle(false, EventResetMode.AutoReset, "{0c9b3ba8-3e04-4c00-94f4-b0ff0510335d}");
		public EventWaitHandle AbandonCurrentRunningBatch = new EventWaitHandle(false, EventResetMode.AutoReset, "{71c29668-24ad-4898-b607-ab19dd76c530}");
		public EventWaitHandle StopTSRServer = new EventWaitHandle(false, EventResetMode.AutoReset, "{70ee0f1d-bf10-46eb-bd9d-4d6f85b5e656}");
		public EventWaitHandle StartTSR = new EventWaitHandle(false, EventResetMode.AutoReset, "{428c17a6-fc24-4e15-a549-fc7491da78da}");
		public EventWaitHandle TSRStarted = new EventWaitHandle(false, EventResetMode.AutoReset, "{2cf63a15-b276-4b70-aa4f-e45ec21e2398}");
	}
}
