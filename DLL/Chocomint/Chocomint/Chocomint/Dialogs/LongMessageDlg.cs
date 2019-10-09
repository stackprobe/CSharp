using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Chocomint.Dialogs.Resource;
using System.Media;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class LongMessageDlg : Form
	{
		public enum Mode_e
		{
			Error = 1,
			Information,
			Warning,
		};

		public Mode_e Mode = Mode_e.Information;
		public string Message = "メッセージを準備しています。";
		public I2Size DlgSize = new I2Size(0, 0);
		public Action PostShown = () => { };

		public LongMessageDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void LongMessageDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private int Base_Dlg_H;
		private int Base_MessageIcon_T;

		private void LongMessageDlg_Shown(object sender, EventArgs e)
		{
			this.Base_Dlg_H = this.Height;
			this.Base_MessageIcon_T = this.MessageIcon.Top;

			switch (this.Mode)
			{
				case Mode_e.Error:
					this.MessageIcon.Image = new Resource0001().ErrorIcon.Image;
					SystemSounds.Hand.Play();
					break;

				case Mode_e.Information:
					this.MessageIcon.Image = new Resource0001().InformationIcon.Image;
					SystemSounds.Asterisk.Play();
					break;

				case Mode_e.Warning:
					this.MessageIcon.Image = new Resource0001().WarningIcon.Image;
					SystemSounds.Exclamation.Play();
					break;

				default:
					throw null; // never
			}
			this.MessageIcon.Size = this.MessageIcon.Image.Size;
			this.TextMessage.Text = this.Message;
			this.TextMessage.SelectionStart = this.TextMessage.TextLength;
			this.TextMessage.BackColor = new TextBox().BackColor;
			this.TextMessage.ForeColor = new TextBox().ForeColor;

			if (this.DlgSize.W != -1)
			{
				int dw = this.DlgSize.W - this.Width;
				int dh = this.DlgSize.H - this.Height;

				dw = Math.Max(0, dw);
				dh = Math.Max(0, dh);

				this.Left -= dw / 2;
				this.Top -= dh / 2;
				this.Width += dw;
				this.Height += dh;
			}
			this.BtnOk.Focus();

			this.PostShown();
			ChocomintDialogsCommon.DlgCommonPostShown(this);
		}

		private void LongMessageDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void LongMessageDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void LongMessageDlg_Resize(object sender, EventArgs e)
		{
			int diff_h = this.Height - this.Base_Dlg_H;

			this.MessageIcon.Top = this.Base_MessageIcon_T + diff_h / 2;
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void TextMessage_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				this.TextMessage.SelectAll();
				e.Handled = true;
			}
		}
	}
}
