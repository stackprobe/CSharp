using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Security.Permissions;
using Charlotte.Tools;

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

			this.WindowState = Gnd.I.MainWin_Minimized ? FormWindowState.Minimized : FormWindowState.Normal;
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MainTimer.Enabled = true;

			using (StartServerDlg f = new StartServerDlg())
			{
				f.ShowDialog();
			}
			this.UIRefresh();
		}

		private void UIRefresh()
		{
			this.TSRWinStyleMenuItem_Invisible.Checked = Gnd.I.TSR_WinStyle == ProcessTools.WindowStyle_e.INVISIBLE;
			this.TSRWinStyleMenuItem_Minimized.Checked = Gnd.I.TSR_WinStyle == ProcessTools.WindowStyle_e.MINIMIZED;
			this.TSRWinStyleMenuItem_Normal.Checked = Gnd.I.TSR_WinStyle == ProcessTools.WindowStyle_e.NORMAL;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gnd.I.MainWin_Minimized = this.WindowState == FormWindowState.Minimized;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MainTimer.Enabled = false;
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.XPressed = true;
		}

		private void MainOutput_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void ポート番号PToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.StopServer();
			this.MainTimer.Enabled = false;

			using (PortNoDlg f = new PortNoDlg())
			{
				f.ShowDialog();
			}
			this.MainTimer.Enabled = true;
			Gnd.I.StartServer();
		}

		private void backlogBToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.StopServer();
			this.MainTimer.Enabled = false;

			using (BacklogDlg f = new BacklogDlg())
			{
				f.ShowDialog();
			}
			this.MainTimer.Enabled = true;
			Gnd.I.StartServer();
		}

		private bool XPressedTreated;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.XPressed && this.XPressedTreated == false)
			{
				this.XPressedTreated = true;

				StopServerDlg.BeforeCloseWaitCounter = 10; // 1 sec
				Gnd.I.StopServer();

				this.MainTimer.Enabled = false;
				this.Close();
				return;
			}
			foreach (string message in Utils.StringMessages.DequeueAll())
			{
				this.WriteToMainOutput(message);
			}
		}

		private void WriteToMainOutput(string message)
		{
			if (100000 < this.MainOutput.Text.Length)
				this.MainOutput.Text = "..." + this.MainOutput.Text.Substring(0, 50000);

			this.MainOutput.AppendText(message);
			this.MainOutput.AppendText("\r\n");
			this.MainOutput.SelectionStart = this.MainOutput.Text.Length;
			this.MainOutput.ScrollToCaret();
		}

		private void MainOutput_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				this.MainOutput.SelectAll();
				e.Handled = true;
			}
		}

		private void 現在実行中のバッチファイルを強制終了するAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			SSRBServerProc.AbandonCurrentRunningBatch();
		}

		private void TSRWinStyleMenuItem_Invisible_Click(object sender, EventArgs e)
		{
			Gnd.I.StopServer();
			Gnd.I.TSR_WinStyle = ProcessTools.WindowStyle_e.INVISIBLE;
			Gnd.I.StartServer();

			this.UIRefresh();
		}

		private void TSRWinStyleMenuItem_Minimized_Click(object sender, EventArgs e)
		{
			Gnd.I.StopServer();
			Gnd.I.TSR_WinStyle = ProcessTools.WindowStyle_e.MINIMIZED;
			Gnd.I.StartServer();

			this.UIRefresh();
		}

		private void TSRWinStyleMenuItem_Normal_Click(object sender, EventArgs e)
		{
			Gnd.I.StopServer();
			Gnd.I.TSR_WinStyle = ProcessTools.WindowStyle_e.NORMAL;
			Gnd.I.StartServer();

			this.UIRefresh();
		}
	}
}
