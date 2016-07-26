using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace Charlotte
{
	public class Common
	{
		public static void SetOperand(TextBox tb, string text)
		{
			if (Gnd.OperandLenMax < text.Length)
			{
				MessageBox.Show(
					"オペランドの最大文字数を超えました。",
					"エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
				tb.Text = "";
				return;
			}
			tb.Text = text;
		}

		public static void SetOperandStatus(TextBox tb, bool ok)
		{
			if (ok)
			{
				tb.ForeColor = Consts.DefForeColor;
				tb.BackColor = Consts.DefBackColor;
			}
			else
			{
				tb.ForeColor = Consts.ErrorForeColor;
				tb.BackColor = Consts.ErrorBackColor;
			}
		}

		public static void SetAnswer(TextBox tb, string text)
		{
			if (Gnd.AnswerLenMax < text.Length)
			{
				MessageBox.Show(
					"計算結果の最大文字数を超えました。",
					"エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
				tb.Text = "";
				return;
			}
			tb.Text = text;
		}

		public static bool UIntChanged(TextBox tb, UInt64 minval, UInt64 maxval)
		{
			try
			{
				{
					string str = tb.Text.Trim();

					if (tb.Text != str)
						tb.Text = str;
				}

				{
					string format = tb.Text;

					format = StringTools.ReplaceChar(format, StringTools.DIGIT, '9');
					format = StringTools.ReplaceLoop(format, "99", "9");

					if (format != "9")
						throw null;
				}

				{
					string str = tb.Text;
					UInt64 value = UInt64.Parse(str);

					if (str != "" + value)
						throw null;

					if (value < minval)
						throw null;

					if (maxval < value)
						throw null;
				}

				tb.ForeColor = Consts.DefForeColor;
				tb.BackColor = Consts.DefBackColor;

				return true;
			}
			catch
			{
				tb.ForeColor = Consts.ErrorForeColor;
				tb.BackColor = Consts.ErrorBackColor;

				return false;
			}
		}

		public static string GetClipboard()
		{
			try
			{
				IDataObject data = Clipboard.GetDataObject();

				if (data.GetDataPresent(DataFormats.Text))
					return (string)data.GetData(DataFormats.Text);
			}
			catch
			{ }

			return null;
		}

		public static void SetClipboard(string text)
		{
			try
			{
				Clipboard.SetDataObject(text, true);
			}
			catch
			{ }
		}

		public static void KeyPressed(KeyPressEventArgs e, Control ctrl, Control nextCtrl)
		{
			if (e.KeyChar == 13) // enter
			{
				nextCtrl.Focus();

				if (nextCtrl is TextBox)
					((TextBox)nextCtrl).SelectAll();
				else if (nextCtrl is ComboBox)
					((ComboBox)nextCtrl).DroppedDown = true;

				e.Handled = true;
			}
		}

		public delegate void Background_d();

		public static void Busy(Background_d d_background)
		{
			Gnd.MainWin.Visible = false;
			GC.Collect();

			BusyDlg.StatusBox.Clear();

			Thread th = new Thread((ThreadStart)delegate
			{
				d_background();
			});
			th.Start();

			using (BusyDlg dlg = new BusyDlg(th))
			{
				dlg.ShowDialog();
			}

			GC.Collect();
			Gnd.MainWin.Visible = true;
		}

		public static void StartKillAndBoot()
		{
			string file = "KillAndBoot.exe";

			if (File.Exists(file) == false)
				file = @"..\..\..\..\KillAndBoot.exe";

			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = file;
				psi.Arguments = "" + Process.GetCurrentProcess().Id;
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;

				Process.Start(psi);
			}
		}
	}
}
