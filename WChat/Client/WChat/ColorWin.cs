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
	public partial class ColorWin : Form
	{
		public static void Edit(TextBox tb)
		{
			tb.Text = Edit(tb.Text);
		}

		public static string Edit(string str)
		{
			return DataConv.GetString(Edit(DataConv.GetColor(str)));
		}

		public static Color Edit(Color color)
		{
			using (ColorWin f = new ColorWin())
			{
				f._color = color;
				f.ShowDialog();

				if (f._ok)
					color = f._color;
			}
			return color;
		}

		private Color _color;
		private bool _ok;

		public ColorWin()
		{
			InitializeComponent();
		}

		private void ColorWin_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void ColorWin_Shown(object sender, EventArgs e)
		{
			this.MinimumSize = this.Size;
			this.RefreshUi_FieldChanged();
		}

		private void BtnColorDlg_Click(object sender, EventArgs e)
		{
			int[] customs = new int[16];

			{
				Random r = new Random();

				for (int c = 0; c < customs.Length; c++)
					customs[c] = r.Next(0x1000000);
			}

			using (ColorDialog f = new ColorDialog())
			{
				f.Color = _color;
				f.CustomColors = customs;

				if (f.ShowDialog() == DialogResult.OK)
				{
					_color = f.Color;
					this.RefreshUi_FieldChanged();
				}
			}
		}

		private void RefreshUi_FieldChanged()
		{
			this.ValText.Text = DataConv.GetString(_color);
			this.RefreshUi();
		}

		private void RefreshUi()
		{
			this.サンプル.BackColor = _color;
		}

		private void ValText_TextChanged(object sender, EventArgs e)
		{
			try
			{
				_color = DataConv.GetColor(this.ValText.Text);
				this.ValText.ForeColor = Color.Black;
			}
			catch
			{
				_color = Color.FromArgb(100, 140, 120); // dummy
				this.ValText.ForeColor = Color.Red;
			}
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
