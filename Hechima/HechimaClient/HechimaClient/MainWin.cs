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
		public MainWin()
		{
			InitializeComponent();

			this.RemarksText.ForeColor = new TextBox().ForeColor;
			this.RemarksText.BackColor = new TextBox().BackColor;

			this.MinimumSize = new Size(300, 300);

			this.LoadSetting();
		}

		private void LoadSetting()
		{
			// TODO
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

		private void 終了XToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void MessageText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 1) // ctrl + a
			{
				this.MessageText.SelectAll();
				e.Handled = true;
			}
			else if (e.KeyChar == 10) // ctrl + enter
			{
				// TODO
			}
			else if (e.KeyChar == 13) // enter
			{
				// TODO
			}
		}

		private void MessageText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void RemarksText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void 設定SSMenuItem_Click(object sender, EventArgs e)
		{
			this.Visible = false;

			Common.WaitToBgServiceEndable();

			Gnd.bgService.Destroy();
			Gnd.bgService = null;

			// TODO

			Gnd.bgService = new BgService();

			this.LoadSetting();

			this.Visible = true;
		}

		private void MainTimer_Tick(object sender, EventArgs e)
		{
			Gnd.bgService.Perform();
		}
	}
}
