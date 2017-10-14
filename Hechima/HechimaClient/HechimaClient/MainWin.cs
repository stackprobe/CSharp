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
			this.RemarksText.Text = "";

			this.MessageText.Text = "";
			this.MessageText.Focus();

			this.MinimumSize = new Size(300, 300);

			this.LoadConf();
			this.LoadSetting();
		}

		private void LoadConf()
		{
			if (Gnd.conf.MessageTextFontFamily != Consts.S_DUMMY && Gnd.conf.MessageTextFontSize != 0)
			{
				this.MessageText.Font = new Font(Gnd.conf.MessageTextFontFamily, Gnd.conf.MessageTextFontSize);
			}
			if (Gnd.conf.MessageText_H != 0)
			{
				int ha = Gnd.conf.MessageText_H - this.MessageText.Height;

				this.RemarksText.Height -= ha;
				this.MessageText.Top -= ha;
				this.MessageText.Height += ha;
			}
		}

		private void LoadSetting()
		{
			this.RemarksText.Font = new Font(Gnd.setting.RemarksTextFontFamily, Gnd.setting.RemarksTextFontSize);
			this.RemarksText.ForeColor = Gnd.setting.RemarksTextForeColor;
			this.RemarksText.BackColor = Gnd.setting.RemarksTextBackColor;

			this.MessageText.ForeColor = Gnd.setting.MessageTextForeColor;
			this.MessageText.BackColor = Gnd.setting.MessageTextBackColor;

			if (Gnd.setting.MainWin_W != -1)
			{
				this.Left = Gnd.setting.MainWin_L;
				this.Top = Gnd.setting.MainWin_T;
				this.Width = Gnd.setting.MainWin_W;
				this.Height = Gnd.setting.MainWin_H;
			}
		}

		private void SaveSetting()
		{
			if (this.WindowState == FormWindowState.Normal)
			{
				Gnd.setting.MainWin_L = this.Left;
				Gnd.setting.MainWin_T = this.Top;
				Gnd.setting.MainWin_W = this.Width;
				Gnd.setting.MainWin_H = this.Height;
			}
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
			this.SaveSetting();
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

			Common.WaitToBgServiceDisposable();

			Gnd.bgService.Dispose();
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
