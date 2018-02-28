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
	public partial class PortNoDlg : Form
	{
		public PortNoDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.PortNo.Text = "" + Gnd.I.PortNo;
		}

		private void PortNoDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void PortNoDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			Gnd.I.PortNo = IntTools.ToInt(this.PortNo.Text, 1, 65535, Consts.DEF_PORT_NO);
		}

		private void PortNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				e.Handled = true;
				this.Close();
			}
		}
	}
}
