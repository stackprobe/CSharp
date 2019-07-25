using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;

namespace Charlotte
{
	public partial class InputSizeDlg : Form
	{
		public Size RefSize;
		public bool OkPressed = false;

		// <---- prm

		public InputSizeDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputSizeDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputSizeDlg_Shown(object sender, EventArgs e)
		{
			this.TxtWidth.Text = "" + this.RefSize.Width;
			this.TxtHeight.Text = "" + this.RefSize.Height;

			this.TxtWidth.SelectAll();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				int w = int.Parse(this.TxtWidth.Text);
				int h = int.Parse(this.TxtHeight.Text);

				if (w != IntTools.Range(w, Consts.MPIC_W_MIN, Consts.MPIC_W_MAX))
					throw new Exception("幅 は " + Consts.MPIC_W_MIN + " 以上 " + Consts.MPIC_W_MAX + " 以下 でなければなりません。");

				if (h != IntTools.Range(h, Consts.MPIC_H_MIN, Consts.MPIC_H_MAX))
					throw new Exception("高さ は " + Consts.MPIC_H_MIN + " 以上 " + Consts.MPIC_H_MAX + " 以下 でなければなりません。");

				this.RefSize = new Size(w, h);
				this.OkPressed = true;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("保存できません", ex, true);
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}
