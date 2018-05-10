using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using Charlotte.Tools;
using System.Threading;

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
				this.MainWin_Minimized = int.Parse(lines[c++]) != 0;
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
				lines.Add("" + (this.MainWin_Minimized ? 1 : 0));
				// ここへ追加...

				File.WriteAllLines(file, lines, Encoding.UTF8);
			}
		}

		// 設定ここから

		public int PortNo = Consts.DEF_PORT_NO;
		public bool MainWin_Minimized = false;

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
			this.StopTSR();
		}

		public bool AbandonCurrentRunningBatchFlag = false;

		public class TSRInfo
		{
			public Process Proc;
			public string WorkDir;

			// ここまで引数

			public bool IsEnded()
			{
				if (this.Proc.HasExited)
				{
					this.Ended();
					return true;
				}
				return false;
			}

			private void Ended()
			{
				Utils.PostMessage("TSR バッチファイルは終了しました。L=" + Path.GetFileName(this.WorkDir));

#if true
				FileTools.Delete(this.WorkDir);
#else
				try // Try twice
				{
					Directory.Delete(this.WorkDir, true);
				}
				catch
				{
					Thread.Sleep(100);
					Directory.Delete(this.WorkDir, true); // .Stop(); -> .IsEnded() したときに例外を投げることがある。*.out を掴んでいて。
				}
#endif
			}

			public void Stop()
			{
				Utils.PostMessage("実行中の TSR バッチファイルを強制終了します。L=" + Path.GetFileName(this.WorkDir));

				try
				{
					this.Proc.Kill();
				}
				catch (Exception e)
				{
					Utils.PostMessage(e);
				}
			}
		}

		public SyncQueue<TSRInfo> TSRInfos = new SyncQueue<TSRInfo>();

		public void MonitorTSR()
		{
			this.TSRInfos.Rotate(info => info.IsEnded() == false);
		}

		private void StopTSR()
		{
			if (1 <= this.TSRInfos.Count)
			{
				using (StopTSRDlg f = new StopTSRDlg())
				{
					f.ShowDialog();
				}
			}
		}
	}
}
