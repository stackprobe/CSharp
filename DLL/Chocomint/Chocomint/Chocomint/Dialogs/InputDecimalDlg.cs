using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class InputDecimalDlg : Form
	{
		public bool OkPressed = false;
		public decimal MinValue = 0;
		public decimal MaxValue = 100;
		public decimal Value = 0;
		public Action PostShown = () => { };
		public Func<decimal, decimal> Validator = ret => ret;

		// <---- prm

		public InputDecimalDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputNumberDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputNumberDlg_Shown(object sender, EventArgs e)
		{
			this.NumValue.Minimum = this.MinValue;
			this.NumValue.Maximum = this.MaxValue;
			this.NumValue.Value = this.Value;
			SelectAll(this.NumValue);

			this.PostShown();
			ChocomintCommon.DlgCommonPostShown(this);
		}

		private static void SelectAll(NumericUpDown numValue)
		{
			try // 例外は投げないと思うけど、念のため
			{
				numValue.Select(0, numValue.Value.ToString().Length);
			}
			catch (Exception e)
			{
				ProcMain.WriteLog(e);
			}
		}

		private void InputNumberDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputNumberDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void NumValue_KeyPress(object sender, KeyPressEventArgs e)
		{
#if false // 警告音が出る。
			if (e.KeyChar == (char)13)
			{
				this.BtnOk.Focus();
				e.Handled = true;
			}
#endif
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				decimal ret = this.NumValue.Value;

				ret = this.Validator(ret);

				this.OkPressed = true;
				this.Value = ret;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("入力エラー", ex, this);

				this.NumValue.Focus();
				SelectAll(this.NumValue);
			}
		}
	}
}
