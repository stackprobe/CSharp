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
	public partial class InputDlg : Form
	{
		public InputDlg()
		{
			InitializeComponent();
		}

		// prm {

		public string PrmPrompt = "";
		public UInt64 MinVal = 0;
		public UInt64 MaxVal = UInt64Tools.IMAX_64;
		public UInt64 InitVal = 0;

		// } ret {

		public bool OkPressed = false;
		public UInt64 RetValue = 0;

		// }

		private void InputDlg_Load(object sender, EventArgs e)
		{
			int h1 = this.Prompt.Height;

			this.Prompt.Text = this.PrmPrompt;

			int h2 = this.Prompt.Height;
			int hd = h2 - h1;

			this.Height += hd;
			this.MinimumSize = this.Size;
			this.InputText.Text = "" + this.InitVal;
			this.InputText.SelectAll();
		}

		private void InputDlg_Shown(object sender, EventArgs e)
		{
			// noop
		}

		private void InputDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			this.OkPressed = true;
			this.RetValue = UInt64Tools.ToUInt64(this.InputText.Text, this.MinVal, this.MaxVal, this.InitVal);
			this.Close();
		}

		private void InputText_TextChanged(object sender, EventArgs e)
		{
			Common.UIntChanged(this.InputText, this.MinVal, this.MaxVal);
		}

		private void InputText_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == 13) // enter
			{
				this.BtnOk.Focus();
				e.Handled = true;
			}
		}
	}
}
