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
	public partial class MemberFontDlg : Form
	{
		public MemberFont MemberFont = new MemberFont(); // 編集対象

		// <---- prm

		public MemberFontDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void MemberFontDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void MemberFontDlg_Shown(object sender, EventArgs e)
		{
			// load
			{
				this.IdentMidPtn.Text = this.MemberFont.IdentMidPtn;
				this.LStamp.Text = this.MemberFont.Stamp.GetString();
				this.LIdent.Text = this.MemberFont.Ident.GetString();
				this.LMessage.Text = this.MemberFont.Message.GetString();
			}

			this.T最近のIdents.Text = string.Join("\r\n", Gnd.RecentlyIdents);
		}

		private void MemberFontDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void MemberFontDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void StampBtn_Click(object sender, EventArgs e)
		{
			this.LStamp.Text = this.EditFontInfoStr(this.LStamp.Text);
		}

		private void IdentBtn_Click(object sender, EventArgs e)
		{
			this.LIdent.Text = this.EditFontInfoStr(this.LIdent.Text);
		}

		private void MessageBtn_Click(object sender, EventArgs e)
		{
			this.LMessage.Text = this.EditFontInfoStr(this.LMessage.Text);
		}

		private string EditFontInfoStr(string str)
		{
			FontInfo fi = new FontInfo();
			fi.SetString(str);

			this.Visible = false;

			using (FontInfoDlg f = new FontInfoDlg() { FontInfo = fi })
			{
				f.ShowDialog();
			}
			this.Visible = true;

			return fi.GetString();
		}

		private void CorrectBtn_Click(object sender, EventArgs e)
		{
			this.CorrectItems();
		}

		private void CorrectItems()
		{
			{
				string value = this.IdentMidPtn.Text;

				value = JString.toJString(value, true, false, false, false);

				if (value == "")
					value = "＠～";

				this.IdentMidPtn.Text = value;
			}
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.CorrectItems();

			// save
			{
				this.MemberFont.IdentMidPtn = this.IdentMidPtn.Text;
				this.MemberFont.Stamp.SetString(this.LStamp.Text);
				this.MemberFont.Ident.SetString(this.LIdent.Text);
				this.MemberFont.Message.SetString(this.LMessage.Text);
			}

			this.Close();
		}
		private void CancelBtn_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
