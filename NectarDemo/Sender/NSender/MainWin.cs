using System;
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
		}

		private void MainWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void SendBtn_Click(object sender, EventArgs e)
		{
			new NSender().NSend("M-Test", this.Message.Text);
			this.Message.Text = "";
		}

		private void Message_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 0x0a) // ctrl + enter
			{
				this.SendBtn_Click(null, null);
				e.Handled = true;
			}
			if (e.KeyChar == 0x01) // ctrl + a
			{
				this.Message.SelectAll();
				e.Handled = true;
			}
		}
	}
}
