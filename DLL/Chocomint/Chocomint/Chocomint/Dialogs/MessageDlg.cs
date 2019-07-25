using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Chocomint.Dialogs.Resource;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class MessageDlg : Form
	{
		public enum Mode_e
		{
			Error = 1,
			Information,
			Warning,
		};

		public Mode_e Mode = Mode_e.Warning;
		public string Message = "メッセージを準備しています。";
		public string DetailMessage = null; // null == 詳細情報なし
		public Action PostShown = () => { };

		public MessageDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void MessageDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MessageDlg_Shown(object sender, EventArgs e)
		{
			switch (this.Mode)
			{
				case Mode_e.Error:
					this.MessageIcon.Image = new Resource0001().ErrorIcon.Image;
					break;

				case Mode_e.Information:
					this.MessageIcon.Image = new Resource0001().InformationIcon.Image;
					break;

				case Mode_e.Warning:
					this.MessageIcon.Image = new Resource0001().WarningIcon.Image;
					break;

				default:
					throw null; // never
			}
			this.MessageIcon.Size = this.MessageIcon.Image.Size;
			this.TextMessage.Text = this.Message;
			this.TextMessage.Top += (this.MessageIcon.Height - this.TextMessage.Height) / 2;

			{
				int w = this.TextMessage.Left + this.TextMessage.Width + 30;

				if (this.Width < w)
				{
					this.Left -= (w - this.Width) / 2;
					this.Width = w;
				}
			}

			if (this.DetailMessage != null)
				this.DetailLabel.Visible = true;

			this.PostShown();
			ChocomintGeneral.CommonPostShown(this);
		}

		private void MessageDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MessageDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void DetailLabel_Click(object sender, EventArgs e)
		{
			int w = this.Width;

			if (this.TextDetailMessage.Visible)
			{
				this.MinimumSize = new Size(500, 250);
				this.Size = this.MinimumSize;

				this.TextDetailMessage.Visible = false;

				this.DetailLabel.Text = "詳細を表示";
			}
			else
			{
				this.MinimumSize = new Size(500, 500);
				this.Size = this.MinimumSize;

				this.TextDetailMessage.Text = this.DetailMessage;
				this.TextDetailMessage.Left = 25;
				this.TextDetailMessage.Top = 105;
				this.TextDetailMessage.Width = 440;
				this.TextDetailMessage.Height = 235;
				this.TextDetailMessage.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
				this.TextDetailMessage.Visible = true;

				this.DetailLabel.Text = "詳細を隠す";
			}
			this.Width = w;
		}

		private void TextDetailMessage_TextChanged(object sender, EventArgs e)
		{
			// noop
		}

		private void TextDetailMessage_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1) // ctrl_a
			{
				this.TextDetailMessage.SelectAll();
				e.Handled = true;
			}
		}
	}
}
