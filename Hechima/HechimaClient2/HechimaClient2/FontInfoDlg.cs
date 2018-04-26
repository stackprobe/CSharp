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
	public partial class FontInfoDlg : Form
	{
		public FontInfo FontInfo = new FontInfo(); // 編集対象

		// <---- prm

		public FontInfoDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;

			// init Families
			{
				this.Families.Items.Clear();

				foreach (string family in Common.GetAllFontFamily())
					this.Families.Items.Add(family);
			}

			this.LErrorMessage.Text = "";
		}

		private void FontInfoDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void FontInfoDlg_Shown(object sender, EventArgs e)
		{
			this.LoadData();
			this.UIRefresh();
		}

		private void LoadData()
		{
			// set Family
			{
				int index = this.GetFamilyIndex(this.FontInfo.Family);

				if (index == -1)
					index = this.GetFamilyIndex("メイリオ");

				if (index != -1)
					index = 0;

				this.Families.SelectedIndex = index;
			}

			this.SizeTxt.Text = "" + this.FontInfo.Size;

			this.CB太字.Checked = (this.FontInfo.Style | FontStyle.Bold) != 0;
			this.CB斜体.Checked = (this.FontInfo.Style | FontStyle.Italic) != 0;
			this.CB取り消し線.Checked = (this.FontInfo.Style | FontStyle.Strikeout) != 0;
			this.CB下線.Checked = (this.FontInfo.Style | FontStyle.Underline) != 0;

			this.L文字色.Text = "" + Common.ToHexString(this.FontInfo.Color);
		}

		private int GetFamilyIndex(string family)
		{
			for (int index = 0; index < this.Families.Items.Count; index++)
				if ((string)this.Families.Items[index] == family)
					return index;

			return -1;
		}

		private void FontInfoDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void FontInfoDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void Families_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void SizeTxt_TextChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void CB太字_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void CB斜体_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void CB取り消し線_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void CB下線_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void UIRefresh()
		{
			this.LErrorMessage.Text = "";

			try
			{
				this.SampleTxt.Font = this.GetFont();
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);

				this.LErrorMessage.Text = e.Message;
			}
		}

		private Font GetFont()
		{
			string family = this.Families.Text;
			int size = IntTools.toInt(this.SizeTxt.Text, 1, 99);
			FontStyle style =
				(this.CB太字.Checked ? FontStyle.Bold : 0) |
				(this.CB斜体.Checked ? FontStyle.Italic : 0) |
				(this.CB取り消し線.Checked ? FontStyle.Strikeout : 0) |
				(this.CB下線.Checked ? FontStyle.Underline : 0);

			return new Font(family, (float)size, style);
		}

		private void 文字色Btn_Click(object sender, EventArgs e)
		{
			// TODO
		}

		private void OKBtn_Click(object sender, EventArgs e)
		{
			this.SaveData();
			this.Close();
		}

		private void SaveData()
		{
			try
			{
				Font font = this.GetFont(); // フォント作成テスト

				this.FontInfo.Family = this.Families.Text;
				this.FontInfo.Size = int.Parse(this.SizeTxt.Text);
				this.FontInfo.Style = font.Style;
				this.FontInfo.Color = Common.ToColorHex(this.L文字色.Text);
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
		}

		private void CancelBtn_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
