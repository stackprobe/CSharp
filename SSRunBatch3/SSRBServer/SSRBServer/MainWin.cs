﻿using System;
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

		private bool XPressed;

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

			if (Gnd.I.MainWin_Minimized)
				this.WindowState = FormWindowState.Minimized;
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			this.MTEnabled = true;
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gnd.I.MainWin_Minimized = this.WindowState == FormWindowState.Minimized;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			this.MTEnabled = false;
		}

		private void BeforeDialog()
		{
			this.MTEnabled = false;
			this.Visible = false;
		}

		private void AfterDialog()
		{
			this.Visible = true;
			this.MTEnabled = true;
		}

		private void CloseWindow()
		{
			this.MTEnabled = false;
			this.Visible = false;

			// プロセス終了時にすること
			{
				Gnd.I.StopServer();
			}

			this.Close();
		}

		private bool MTEnabled;
		private bool MTBusy;
		private long MTCount;

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			if (this.MTEnabled == false || this.MTBusy)
				return;

			this.MTBusy = true;

			try
			{
				if (Gnd.I.BatchServer != null && Gnd.I.BatchServer.SockServer.IsRunning() == false)
					Gnd.I.BatchServer = null;

				Gnd.I.MonitorTSR();

				{
					string text = "";

					if (1 <= Gnd.I.TSRInfos.Count)
						text = "【情報】実行中の TSR バッチファイルがあります。";

					if (Gnd.I.BatchServer == null)
						text = "【情報】サーバーは停止しています。";

					if (this.Status.Text != text)
						this.Status.Text = text;
				}

				{
					string text = "S=" + (Gnd.I.BatchServer == null ? "停止中" : "実行中") + " TSR=" + Gnd.I.TSRInfos.Count;

					if (this.EastStatus.Text != text)
						this.EastStatus.Text = text;
				}

				foreach (string message in Utils.StringMessages.DequeueAll())
				{
					this.MainOutput.AppendText(message + "\r\n");

					if (Consts.MAIN_OUTPUT_LEN_MAX < this.MainOutput.Text.Length)
						this.MainOutput.Text = "... " + this.MainOutput.Text.Substring(Consts.MAIN_OUTPUT_LEN_MAX / 2);

					this.MainOutput.SelectionStart = this.MainOutput.Text.Length;
					this.MainOutput.ScrollToCaret();
				}

				if (this.XPressed)
				{
					this.XPressed = false;

					if (Gnd.I.UnableToStopServerWhenTSRRunning && 1 <= Gnd.I.TSRInfos.Count)
					{
						Utils.PostMessage("実行中の TSR バッチファイルがあるため終了出来ません！[X]");
						return;
					}
					this.CloseWindow();
					return;
				}
			}
			finally
			{
				this.MTBusy = false;
				this.MTCount++;
			}
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Gnd.I.UnableToStopServerWhenTSRRunning && 1 <= Gnd.I.TSRInfos.Count)
			{
				Utils.PostMessage("実行中の TSR バッチファイルがあるため終了出来ません！");
				return;
			}
			this.CloseWindow();
		}

		private void サーバーの再起動RToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.サーバーの停止TToolStripMenuItem_Click(null, null);
			this.サーバーの起動SToolStripMenuItem_Click(null, null);
		}

		private void サーバーの起動SToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.StartServer();
		}

		private void サーバーの停止TToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Gnd.I.BatchServer != null)
			{
				if (Gnd.I.UnableToStopServerWhenTSRRunning && 1 <= Gnd.I.TSRInfos.Count)
				{
					Utils.PostMessage("実行中の TSR バッチファイルがあるため停止出来ません！");
					return;
				}
				this.BeforeDialog();
				Gnd.I.StopServer();
				this.AfterDialog();
			}
		}

		private void ポート番号PToolStripMenuItem_Click(object sender, EventArgs e)
		{
			if (Gnd.I.UnableToStopServerWhenTSRRunning && 1 <= Gnd.I.TSRInfos.Count)
			{
				Utils.PostMessage("実行中の TSR バッチファイルがあるため設定を開けません！");
				return;
			}
			bool serverRunning = Gnd.I.BatchServer != null;

			this.BeforeDialog();
			Gnd.I.StopServer();

			using (PortNoDlg f = new PortNoDlg())
			{
				f.ShowDialog();
			}
			this.AfterDialog();

			if (serverRunning)
				Gnd.I.StartServer();
		}

		private void 現在実行中のバッチファイルを強制終了するAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.AbandonCurrentRunningBatchFlag = true;
		}

		private void 現在実行中のTSRバッチファイルを全て強制終了するAToolStripMenuItem_Click(object sender, EventArgs e)
		{
			Gnd.I.TSRInfos.Rotate(
				info =>
				{
					info.Stop();
					return false;
				},
				int.MaxValue
				);
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
	}
}
