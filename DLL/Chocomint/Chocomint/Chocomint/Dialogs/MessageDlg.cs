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
					this.Width = w;
					this.Left -= w / 2;
				}
			}

			if (this.DetailMessage != null)
				this.DetailLabel.Visible = true;

			this.PostShown();
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

			if (this.TextDetail.Visible)
			{
				this.MinimumSize = new Size(500, 250);
				this.Size = this.MinimumSize;

				this.TextDetail.Visible = false;

				this.DetailLabel.Text = "詳細を表示";
			}
			else
			{
				this.MinimumSize = new Size(500, 500);
				this.Size = this.MinimumSize;

				this.TextDetail.Text = this.DetailMessage;
				this.TextDetail.Left = 25;
				this.TextDetail.Top = 105;
				this.TextDetail.Width = 440;
				this.TextDetail.Height = 235;
				this.TextDetail.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
				this.TextDetail.Visible = true;

				this.DetailLabel.Text = "詳細を隠す";
			}
			this.Width = w;
		}

		private void TextDetail_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)1)
			{
				this.TextDetail.SelectAll();
				e.Handled = true;
			}
		}
	}
}
