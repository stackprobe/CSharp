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
		private string _viewRtf;

		public ViewWin(string viewRtf)
		{
			_viewRtf = viewRtf;

			InitializeComponent();

			this.MinimumSize = new Size(300, 300);


			// TODO -- init RemarksRTB


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
			this.RemarksRTB.Rtf = _viewRtf;
		}

		private void 閉じるCToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void RemarksRTB_TextChanged(object sender, EventArgs e)
		{
			// noop
		}
	}
}
