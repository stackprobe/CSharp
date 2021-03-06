﻿using System;
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
	public partial class InputFileDlg : Form
	{
		public enum Mode_e
		{
			LOAD,
			SAVE,
		};

		public Mode_e Mode = Mode_e.LOAD;
		public string DefaultFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Default.txt");
		public string FilterString = "txt.bin";
		public string BrowseTitle = "ファイル選択";
		public bool OkPressed = false;
		public string Value = "";
		public Action PostShown = () => { };
		public Func<string, string> Validator = v => v;

		// <---- prm

		public InputFileDlg()
		{
			InitializeComponent();

			this.MinimumSize = this.Size;
		}

		private void InputFileDlg_Load(object sender, EventArgs e)
		{
			// noop
		}

		private void InputFileDlg_Shown(object sender, EventArgs e)
		{
			this.TextValue.Text = this.Value;
			this.TextValue.SelectAll();

			this.PostShown();
			ChocomintDialogsCommon.DlgCommonPostShown(this);
		}

		private void InputFileDlg_FormClosing(object sender, FormClosingEventArgs e)
		{
			// noop
		}

		private void InputFileDlg_FormClosed(object sender, FormClosedEventArgs e)
		{
			// noop
		}

		private void BtnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		public static bool FAT32 = false;

		private static string S_MakeFullPath(string path)
		{
			if (FAT32)
			{
				return ExtraTools.MakeFullPath_FAT32(path);
			}
			else
			{
				return FileTools.MakeFullPath(path);
			}
		}

		private void BtnOk_Click(object sender, EventArgs e)
		{
			try
			{
				string ret = this.TextValue.Text;

				ret = S_MakeFullPath(ret);
				ret = this.Validator(ret);

				this.OkPressed = true;
				this.Value = ret;
				this.Close();
			}
			catch (Returning)
			{ }
			catch (Exception ex)
			{
				MessageDlgTools.Warning("入力エラー", ex, true);

				this.TextValue.Focus();
				this.TextValue.SelectAll();
			}
		}

		private void BtnBrowse_Click(object sender, EventArgs e)
		{
			SaveLoadDialogs.SaveFileOverwritePrompt = false;
			try
			{
				string file = this.TextValue.Text;

				try
				{
					file = S_MakeFullPath(file);
				}
				catch
				{
					file = this.DefaultFile;
				}

				switch (this.Mode)
				{
					case Mode_e.LOAD:
						file = SaveLoadDialogs.LoadFile(this.BrowseTitle, this.FilterString, file);
						break;

					case Mode_e.SAVE:
						file = SaveLoadDialogs.SaveFile(this.BrowseTitle, this.FilterString, file);
						break;

					default:
						throw null; // never
				}
				if (file != null)
				{
					this.TextValue.Text = file;
					this.TextValue.SelectAll();
					this.TextValue.Focus();
				}
			}
			catch (Exception ex)
			{
				MessageDlgTools.Warning("ファイル選択エラー", ex, true);
			}
			SaveLoadDialogs.SaveFileOverwritePrompt = true; // 復元
		}

		private void TextValue_KeyPress(object sender, KeyPressEventArgs e)
		{
			if (e.KeyChar == (char)13) // enter
			{
				this.BtnOk.Focus();
				e.Handled = true;
			}
		}

		private void InputFileDlg_DragEnter(object sender, DragEventArgs e)
		{
			e.Effect = DragDropEffects.Copy;
		}

		private void InputFileDlg_DragDrop(object sender, DragEventArgs e)
		{
			try
			{
				this.TextValue.Text = ((string[])e.Data.GetData(DataFormats.FileDrop)).First(file => File.Exists(file));
				this.TextValue.SelectAll();
				this.TextValue.Focus();
			}
			catch
			{
				this.TextValue.Text = "";
			}
		}
	}
}
