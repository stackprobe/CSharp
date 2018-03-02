using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using System.Diagnostics;
using System.Threading;
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

				this.MainWin_L = int.Parse(lines[c++]);
				this.MainWin_T = int.Parse(lines[c++]);
				this.MainWin_W = int.Parse(lines[c++]);
				this.MainWin_H = int.Parse(lines[c++]);

				this.MainWin_WindowState = (FormWindowState)int.Parse(lines[c++]);
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

				lines.Add("" + this.MainWin_L);
				lines.Add("" + this.MainWin_T);
				lines.Add("" + this.MainWin_W);
				lines.Add("" + this.MainWin_H);

				lines.Add("" + (int)this.MainWin_WindowState);
				// ここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public int PortNo = 55985;

		public int MainWin_L;
		public int MainWin_T;
		public int MainWin_W = -1; // -1 == 未設定
		public int MainWin_H;

		public FormWindowState MainWin_WindowState = FormWindowState.Normal;

		// 設定ここまで

		public BatchServer BatchServer = null;

		public void StartServer()
		{
			if (this.BatchServer == null)
				this.BatchServer = new BatchServer();
		}

		public void StopServer()
		{
			if (this.BatchServer != null)
			{
				using (StopServerDlg f = new StopServerDlg())
				{
					f.ShowDialog();
				}
			}
		}

		public bool AbandonCurrentRunningBatchFlag;

		public class TSRInfo
		{
			public Process Proc;
			public string WorkDir;
		}

		public List<TSRInfo> TSRInfos = new List<TSRInfo>();

		public void MonitorTSR()
		{
			if (1 <= this.TSRInfos.Count)
			{
				int index = (int)SecurityTools.CRandom.GetRandom((uint)this.TSRInfos.Count); // FIXME

				if (this.TSRInfos[index].Proc.HasExited)
				{
					try // Try twice
					{
						Directory.Delete(this.TSRInfos[index].WorkDir, true);
					}
					catch
					{
						Thread.Sleep(100);
						Directory.Delete(this.TSRInfos[index].WorkDir, true);
					}

					this.TSRInfos.RemoveAt(index);
				}
			}
		}
	}
}
