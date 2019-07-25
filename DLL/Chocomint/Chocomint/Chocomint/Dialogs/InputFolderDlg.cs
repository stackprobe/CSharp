using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public partial class InputFolderDlg : Form
	{
		public string DefaultDir = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
		public string BrowseTitle = "フォルダ入力";
		public bool OkPressed = false;
		public string Value = "";
		public Action PostShown = () => { };
		public Func<string, string> Validator = v => v;

		// <---- prm

		public InputFolderDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputFolderDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputFolderDlg_Shown(object sender, EventArgs e)
		{
			this.TextValue.Text = this.Value;
			this.TextValue.SelectAll();

			this.PostShown();
		}

		private void InputFolderDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputFolderDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnBrowse_Click(object sender, EventArgs e)
		{
			try
			{
				string dir = this.TextValue.Text;

				try
				{
					dir = FileTools.MakeFullPath(dir);
				}
				catch
				{
					dir = this.DefaultDir;
				}

				SaveLoadDialogs.SelectFolder(ref dir, "フォルダを選択して下さい");

				this.TextValue.Text = dir;
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("フォルダ選択エラー", ex, this);
			}
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				string ret = this.TextValue.Text;

				ret = FileTools.MakeFullPath(ret);
				ret = this.Validator(ret);

				this.OkPressed = true;
				this.Value = ret;
				this.Close();
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("入力エラー", ex, this);

				this.TextValue.Focus();
				this.TextValue.SelectAll();
			}
		}

		private void TextValue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.BtnOk.Focus();
				e.Handled = true;
			}
		}
	}
}
