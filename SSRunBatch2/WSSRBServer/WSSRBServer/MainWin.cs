﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class MainWin : Form
	{
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
			this.Close();
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

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			foreach (string message in Program.MessageBuffer.Dequeue())
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
	}
}
