using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Charlotte
{
	public partial class MainWin : Form
	{
		private NRecver _mr = new NRecver();
		private Thread _th;

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
			_th = new Thread((ThreadStart)delegate
			{
				_mr.NRecv("M-Test", this.Recved);
			});
			_th.Start();
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			_mr.NRecvEnd = true;
			_th.Join();
			_th = null;
		}

		private void Message_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 0x01) // ctrl + a
			{
				this.Message.SelectAll();
				e.Handled = true;
			}
		}

		private void Recved(string message)
		{
			this.BeginInvoke((MethodInvoker)delegate
			{
				message += "\r\n";

				if (this.Message.Text.Length < 1000)
					this.Message.Text += message;
				else
					this.Message.Text = message;

				this.Message.SelectionStart = this.Message.Text.Length;
				this.Message.ScrollToCaret();
			});
		}
	}
}
