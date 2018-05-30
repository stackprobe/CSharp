using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public partial class BacklogDlg : Form
	{
		public BacklogDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.Backlog.Text = "" + Gnd.I.Backlog;
		}

		private void BacklogDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void BacklogDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			Gnd.I.Backlog = IntTools.ToInt(this.Backlog.Text, 1, IntTools.IMAX, Consts.DEF_BACKLOG);
		}

		private void Backlog_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void Backlog_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				e.Handled = true;
				this.Close();
			}
		}
	}
}
