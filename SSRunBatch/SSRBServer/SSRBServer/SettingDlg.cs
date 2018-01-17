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
	public partial class SettingDlg : Form
	{
		public SettingDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			this.PortNo.Text = "" + Gnd.PortNo;
		}

		private void SettingDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void SettingDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			Gnd.PortNo = 55985; // def

			try
			{
				int value = int.Parse(this.PortNo.Text);

				if (1 <= value && value <= 65535)
				{
					Gnd.PortNo = value;
				}
			}
			catch
			{ }
		}

		private void PortNo_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void PortNo_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.Close();
				e.Handled = true;
			}
		}
	}
}
