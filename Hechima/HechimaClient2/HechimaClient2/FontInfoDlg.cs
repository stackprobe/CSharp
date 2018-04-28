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
			this.CB背景を暗く.Checked = Common.IsBrightColor(this.FontInfo.Color);
			this.UIRefresh();
		}

		private void LoadData()
		{
			// set Family
			{
				int index = this.GetFamilyIndex(this.FontInfo.Family);

				if (index == -1)
					index = this.GetFamilyIndex("メイリオ");

				if (index == -1)
					index = 0;

				this.Families.SelectedIndex = index;
			}

			this.SizeTxt.Text = "" + this.FontInfo.Size;

			this.CB太字.Checked = (this.FontInfo.Style & FontStyle.Bold) != 0;
			this.CB斜体.Checked = (this.FontInfo.Style & FontStyle.Italic) != 0;
			this.CB取り消し線.Checked = (this.FontInfo.Style & FontStyle.Strikeout) != 0;
			this.CB下線.Checked = (this.FontInfo.Style & FontStyle.Underline) != 0;

			this.L文字色.Text = Common.ToHexString(this.FontInfo.Color);
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

		private void CB背景を暗く_CheckedChanged(object sender, EventArgs e)
		{
			this.UIRefresh();
		}

		private void UIRefresh()
		{
			try
			{
				this.SampleTxt.Font = this.GetFont();
				this.SampleTxt.ForeColor = Common.ToColorHex(this.L文字色.Text);

				this.LErrorMessage.Text = "";
				this.OKBtn.Enabled = true;
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);

				this.LErrorMessage.Text = e.Message;
				this.OKBtn.Enabled = false;
			}

			{
				Color bc = this.CB背景を暗く.Checked ? Color.Black : Color.White;

				if (this.SampleTxt.BackColor != bc)
					this.SampleTxt.BackColor = bc;
			}
		}

		private Font GetFont()
		{
			string family = this.Families.Text;
			int size = this.GetFontSize();
			FontStyle style =
				(this.CB太字.Checked ? FontStyle.Bold : 0) |
				(this.CB斜体.Checked ? FontStyle.Italic : 0) |
				(this.CB取り消し線.Checked ? FontStyle.Strikeout : 0) |
				(this.CB下線.Checked ? FontStyle.Underline : 0);

			return new Font(family, (float)size, style);
		}

		private int GetFontSize()
		{
			return IntTools.toInt(this.SizeTxt.Text, 1, 99);
		}

		private void 文字色Btn_Click(object sender, EventArgs e)
		{
			Color color = Common.ToColorHex(this.L文字色.Text);

			//ColorDialogクラスのインスタンスを作成
			using (ColorDialog cd = new ColorDialog())
			{
				//はじめに選択されている色を設定
				cd.Color = color;
				//色の作成部分を表示可能にする
				//デフォルトがTrueのため必要はない
				cd.AllowFullOpen = true;
				//純色だけに制限しない
				//デフォルトがFalseのため必要はない
				cd.SolidColorOnly = false;
				//[作成した色]に指定した色（RGB値）を表示する
				cd.CustomColors = Common.MakeCustomColors();

				//ダイアログを表示する
				if (cd.ShowDialog() == DialogResult.OK)
				{
					//選択された色の取得
					color = cd.Color;

					this.L文字色.Text = Common.ToHexString(color);
					this.UIRefresh();
				}
			}
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
				Font font = this.GetFont(); // フォント作成テストを兼ねる。

				this.FontInfo.Family = this.Families.Text;
				this.FontInfo.Size = this.GetFontSize();
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
