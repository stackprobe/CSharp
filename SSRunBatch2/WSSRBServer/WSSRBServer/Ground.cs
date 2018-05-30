using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Diagnostics;
using Charlotte.Tools;

namespace Charlotte
{
	public class Gnd
	{
		public static Gnd I;

		public string SettingFile = Path.Combine(Program.SelfDir, Path.GetFileNameWithoutExtension(Program.SelfFile) + ".dat");

		public void Load(string file)
		{
			try
			{
				string[] lines = File.ReadAllLines(file, Encoding.UTF8);
				int c = 0;

				this.PortNo = int.Parse(lines[c++]);
				this.Backlog = int.Parse(lines[c++]);
				this.MainWin_Minimized = int.Parse(lines[c++]) != 0;
				this.TSR_WinStyle = (ProcessTools.WindowStyle_e)int.Parse(lines[c++]);
				// ここへ追加...
			}
			catch
			{ }
		}

		public void Save(string file)
		{
			{
				List<string> lines = new List<string>();

				lines.Add("" + this.PortNo);
				lines.Add("" + this.Backlog);
				lines.Add("" + (this.MainWin_Minimized ? 1 : 0));
				lines.Add("" + (int)this.TSR_WinStyle);
				// ここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public int PortNo = Consts.DEF_PORT_NO;
		public int Backlog = Consts.DEF_BACKLOG;
		public bool MainWin_Minimized = false;
		public ProcessTools.WindowStyle_e TSR_WinStyle = ProcessTools.WindowStyle_e.MINIMIZED;

		// 設定ここまで

		public EventWaitHandle TSRServerStarted = new EventWaitHandle(false, EventResetMode.AutoReset, "{2cf63a15-b276-4b70-aa4f-e45ec21e2398}"); // shared_uuid

		public Process TSRServerProc;
		public Process ServerProc;

		public void StartServer()
		{
			this.TSRServerStarted.WaitOne(0); // reset
			this.TSRServerProc = SSRBServerProc.StartTSRServer();
			this.TSRServerStarted.WaitOne();
			this.ServerProc = SSRBServerProc.StartServer();
		}

		public void StopServer()
		{
			using (StopServerDlg f = new StopServerDlg())
			{
				f.ShowDialog();
			}
			this.TSRServerProc = null;
			this.ServerProc = null;
		}
	}
}
