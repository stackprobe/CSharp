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
	public partial class InputTrackBarDlg : Form
	{
		public bool OkPressed = false;
		public int MinValue = 0;
		public int MaxValue = 10;
		public int Value = 0;
		public Action PostShown = () => { };
		public Func<int, int> Validator = ret => ret;

		// <---- prm

		public InputTrackBarDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputTrackBarDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputTrackBarDlg_Shown(object sender, EventArgs e)
		{
			this.BarValue.Minimum = this.MinValue;
			this.BarValue.Maximum = this.MaxValue;
			this.BarValue.Value = this.Value;
			this.BarValue_Scroll(null, null);

			this.PostShown();
			ChocomintCommon.DlgCommonPostShown(this);
		}

		private void InputTrackBarDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputTrackBarDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				int ret = this.BarValue.Value;

				ret = this.Validator(ret);

				this.OkPressed = true;
				this.Value = ret;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("入力エラー", ex, true);

				this.BarValue.Focus();
			}
		}

		private void BarValue_Scroll(object sender, EventArgs e)
		{
			this.CurrValue.Text = string.Format("{0} ( {1:F3} % )",
				this.BarValue.Value,
				(this.BarValue.Value - this.BarValue.Minimum) * 100.0 / (this.BarValue.Maximum - this.BarValue.Minimum)
				);
		}

		private void BarValue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.BtnOk.Focus();
				e.Handled = true;
			}
		}
	}
}
