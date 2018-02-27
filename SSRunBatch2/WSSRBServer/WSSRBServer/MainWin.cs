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
			// noop
		}

		private void MainWin_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gnd.I.MainWin_Minimized = this.WindowState == FormWindowState.Minimized;
		}

		private void MainWin_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MainOutput_TextChanged(object sender, EventArgs e)
		{
			// noop
		}
	}
}
