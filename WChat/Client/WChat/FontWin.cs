using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class FontWin : Form
	{
		public static void Edit(TextBox tb)
		{
			tb.Text = Edit(tb.Text);
		}

		public static string Edit(string str)
		{
			return Edit(new FontMan(str)).ToString();
		}

		public static FontMan Edit(FontMan fo)
		{
			using (FontWin f = new FontWin())
			{
				f._fo = fo.GetClone();
				f.ShowDialog();

				if (f._ok)
					fo = f._fo;
			}
			return fo;
		}

		private FontMan _fo;
		private bool _ok;

		public FontWin()
		{
			InitializeComponent();
		}

		private void FontWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void FontWin_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;

			{
				this.FontName.Items.Clear(); // 2bs

				foreach (string name in SystemTools.GetAllFontFamily())
				{
					this.FontName.Items.Add(name);
				}
			}

			this.RefreshUi_FieldChanged();
		}

		private void RefreshUi_FieldChanged()
		{
			FontMan fo = _fo.GetClone();

			this.FontName.Text = fo.Name;
			this.FontSize.Text = "" + fo.Size;
			this.FSBold.Checked = (fo.Style & FontStyle.Bold) != 0;
			this.FSItalic.Checked = (fo.Style & FontStyle.Italic) != 0;
			this.FSStrikeout.Checked = (fo.Style & FontStyle.Strikeout) != 0;
			this.FSUnderline.Checked = (fo.Style & FontStyle.Underline) != 0;
			this.FontColor.Text = DataConv.GetString(fo.Color);

			this.RefreshUi();
		}

		private void RefreshUi()
		{
			try
			{
				int size = int.Parse(this.FontSize.Text);

				if (IntTools.IsRange(size, 1, 999) == false)
					throw new Exception("フォントサイズは 1～999 の範囲で指定して下さい。");

				_fo.Name = this.FontName.Text;
				_fo.Size = size;
				//_fo.Size = IntTools.ToRange(IntTools.TryParse(this.FontSize.Text, 10), 1, 999); // old
				_fo.Style =
					(this.FSBold.Checked ? FontStyle.Bold : 0) |
					(this.FSItalic.Checked ? FontStyle.Italic : 0) |
					(this.FSStrikeout.Checked ? FontStyle.Strikeout : 0) |
					(this.FSUnderline.Checked ? FontStyle.Underline : 0);
				_fo.Color = DataConv.GetColor(this.FontColor.Text);

				_fo.GetFont(); // フォント生成テスト

				this.サンプル.Font = _fo.GetFont();
				this.サンプル.ForeColor = _fo.GetColor();

				if (SystemTools.IsBrightColor(_fo.GetColor()))
					this.サンプル.BackColor = Color.Black;
				else
					this.サンプル.BackColor = Color.White;

				// ----

				WinTools.SetEnabled(this.BtnOk, true);
				this.StatusMessage.Text = "";
			}
			catch (Exception e)
			{
				WinTools.SetEnabled(this.BtnOk, false);
				this.StatusMessage.Text = e.Message;
			}
		}

		private void FontName_SelectedIndexChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void FontSize_TextChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void FSBold_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void FSItalic_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void FSStrikeout_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void FSUnderline_CheckedChanged(object sender, EventArgs e)
		{
			this.RefreshUi();
		}

		private void BtnColor_Click(object sender, EventArgs e)
		{
			ColorWin.Edit(this.FontColor);
			this.RefreshUi();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			_ok = true;
			this.Close();
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
