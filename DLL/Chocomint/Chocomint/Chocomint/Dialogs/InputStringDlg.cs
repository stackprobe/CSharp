using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class InputStringDlg : Form
	{
		public bool OkPressed = false;
		public string Value = "";
		public Action PostShown = () => { };
		public Action<string> Validator = v => { };

		// <---- prm

		public InputStringDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputStringDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputStringDlg_Shown(object sender, EventArgs e)
		{
			this.TextValue.Text = this.Value;
			this.TextValue.SelectAll();

			this.PostShown();
		}

		private void InputStringDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputStringDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				string ret = this.TextValue.Text;

				this.Validator(ret);

				this.OkPressed = true;
				this.Value = ret;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("入力エラー", ex);
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
