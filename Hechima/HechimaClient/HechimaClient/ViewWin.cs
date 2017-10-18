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
	public partial class ViewWin : Form
	{
		private string _viewText;

		public ViewWin(string viewText)
		{
			_viewText = viewText;

			InitializeComponent();

			this.MinimumSize = new Size(300, 300);

			this.RemarksText.Font = new Font(Gnd.setting.RemarksTextFontFamily, Gnd.setting.RemarksTextFontSize);
			this.RemarksText.ForeColor = Gnd.setting.RemarksTextForeColor;
			this.RemarksText.BackColor = Gnd.setting.RemarksTextBackColor;

			if (Gnd.setting.MainWin_W != -1)
			{
				this.Left = Gnd.setting.MainWin_L;
				this.Top = Gnd.setting.MainWin_T;
				this.Width = Gnd.setting.MainWin_W;
				this.Height = Gnd.setting.MainWin_H;
			}
		}

		private void WiewWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void WiewWin_Shown(object sender, EventArgs e)
		{
			this.RemarksText.Text = _viewText;
			this.RemarksText.SelectionStart = this.RemarksText.TextLength;
			this.RemarksText.ScrollToCaret();
		}

		private void RemarksText_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void RemarksText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 1) // ctrl + a
			{
				this.RemarksText.SelectAll();
				e.Handled = true;
			}
		}

		private void 閉じるCToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
