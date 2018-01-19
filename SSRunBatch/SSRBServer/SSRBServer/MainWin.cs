using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
				return;

			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.Visible = false;
			this.TaskTrayIcon.Visible = true;
			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
			this.TaskTrayIcon.Visible = false;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private long SuppressBalloon_MTCount = -1;

		private int BatchServerStopCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (Gnd.BatchServer != null)
				{
					Exception ex = Gnd.BatchServer.SockServer.GetException();

					if (ex != null)
					{
						Logger.WriteLine(ex);

						if (this.SuppressBalloon_MTCount < this.MTCount)
						{
							Logger.WriteLine("Show Balloon Tip !");

							this.TaskTrayIcon.BalloonTipTitle = ex.Message;
							this.TaskTrayIcon.BalloonTipText = "" + ex;
							this.TaskTrayIcon.BalloonTipIcon = ToolTipIcon.Warning;
							this.TaskTrayIcon.ShowBalloonTip(10000);

							this.SuppressBalloon_MTCount = long.MaxValue;
						}
						return;
					}
					if (Gnd.BatchServer.SockServer.IsRunning() == false)
					{
						Gnd.BatchServer = null;
						return;
					}
				}

				if (Gnd.BatchServer == null)
				{
					this.BatchServerStopCount++;

					if (300 < this.BatchServerStopCount) // 30 sec <
					{
						Gnd.BatchServer = new BatchServer(); // 自動的に再起動する。
						this.BatchServerStopCount = 0;
						return;
					}
				}
				else
					this.BatchServerStopCount = 0;

				if (this.MTCount % 20 == 0) // per 2 sec
				{
					string text;

					if (Gnd.BatchServer != null)
						text = "SSRBServer / 稼働中 (TCPポート番号：" + Gnd.PortNo + ")";
					else
						text = "SSRBServer / 停止中";

					if (this.TaskTrayIcon.Text != text)
						this.TaskTrayIcon.Text = text;
				}
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void TaskTrayIcon_BalloonTipClicked(object sender, EventArgs e)
		{
			Logger.WriteLine("Balloon Tip Clicked !");

			this.SuppressBalloon_MTCount = this.MTCount + 10; // + 1 sec
		}

		private void TaskTrayIcon_BalloonTipClosed(object sender, EventArgs e)
		{
			Logger.WriteLine("Balloon Tip Closed !");

			this.SuppressBalloon_MTCount = this.MTCount + 10; // + 1 sec
		}

		private void ポート番号PToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.MTEnabled = false;
			this.TaskTrayIcon.Visible = false;

			Gnd.BatchServer_Stop_B();

			using (SettingDlg f = new SettingDlg())
			{
				f.ShowDialog();
			}
			Gnd.Save(Gnd.SettingFile);

			Gnd.BatchServer = new BatchServer();

			this.TaskTrayIcon.Visible = true;
			this.MTEnabled = true;
		}

		private void Abandon_実行AToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.AbandonCurrentRunningBatchFlag = true;
		}
	}
}
