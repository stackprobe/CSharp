using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Security.Permissions;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		#region ALT_F4 抑止

		private bool XPressed = false;

		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			const int WM_SYSCOMMAND = 0x112;
			const long SC_CLOSE = 0xF060L;

			if (m.Msg == WM_SYSCOMMAND && (m.WParam.ToInt64() & 0xFFF0L) == SC_CLOSE)
			{
				this.XPressed = true;
				return;
			}
			base.WndProc(ref m);
		}

		#endregion

		public MainWin()
		{
			InitializeComponent();

			this.Status.Text = "";
			this.EastStatus.Text = "";

			this.MinimumSize = this.Size;
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			if (Gnd.I.MainWin_W != -1)
			{
				this.Left = Gnd.I.MainWin_L;
				this.Top = Gnd.I.MainWin_T;
				this.Width = Gnd.I.MainWin_W;
				this.Height = Gnd.I.MainWin_H;
			}
			this.WindowState = Gnd.I.MainWin_WindowState;

			this.MainTimer.Enabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			this.TrySave();
			Gnd.I.Save(Gnd.I.SettingFile);
		}

		private void TrySave()
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Gnd.I.MainWin_L = this.Left;
				Gnd.I.MainWin_T = this.Top;
				Gnd.I.MainWin_W = this.Width;
				Gnd.I.MainWin_H = this.Height;
			}
			Gnd.I.MainWin_WindowState = this.WindowState;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.XPressed = true;
		}

		private void ポート番号PToolStripMenuItem_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void 現在実行中のバッチファイルを強制終了するAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.AbandonCurrentRunningBatchFlag = true;
		}

		private void 現在実行中のTSRバッチファイルを全て強制終了するTToolStripMenuItem_Click(object sender, EventArgs e)
		{
			foreach (Gnd.TSRInfo info in Gnd.I.TSRInfos)
			{
				info.Proc.Kill();
			}
		}

		private void MainOutput_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void MainOutput_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				this.MainOutput.SelectAll();
				e.Handled = true;
			}
		}

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.XPressed)
			{
				this.XPressed = false;

				if (1 <= Gnd.I.TSRInfos.Count)
				{
					this.Status.Text = "現在実行中の TSR バッチファイルがあるため終了出来ません。";
					return;
				}

				Gnd.I.StopServer();

				this.MainTimer.Enabled = false;
				this.Close();
				return;
			}

			if (Gnd.I.BatchServer != null && Gnd.I.BatchServer.SockServer.IsRunning() == false)
				Gnd.I.BatchServer = null;

			Gnd.I.MonitorTSR();

			{
				string text = "S=" + (Gnd.I.BatchServer == null ? "停止中" : "実行中") + " TSR=" + Gnd.I.TSRInfos.Count;

				if (this.EastStatus.Text != text)
					this.EastStatus.Text = text;
			}

			if (0 < this.KeepStatusCount && --this.KeepStatusCount <= 0)
				this.Status.Text = "";
		}

		private int KeepStatusCount;

		private void SetStatus(string text)
		{
			this.KeepStatusCount = 150; // 15 sec
			this.Status.Text = text;
		}
	}
}
