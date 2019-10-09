using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public static class InputOptionDlgTools
	{
		public static int Show(string title, string message, string[] options, bool hasParent = false)
		{
			return Show_Mode(InputOptionDlg.Mode_e.Default, title, message, options, hasParent);
		}

		public static int Warning(string title, string message, string[] options, bool hasParent = false)
		{
			return Show_Mode(InputOptionDlg.Mode_e.Warning, title, message, options, hasParent);
		}

		public static int Show_Mode(InputOptionDlg.Mode_e mode, string title, string message, string[] options, bool hasParent = false)
		{
			using (InputOptionDlg f = new InputOptionDlg())
			{
				f.Mode = mode;
				f.Message = message;
				f.Options = options;

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
				};

				f.ShowDialog();

				return f.SelectedIndex; // 0 ～ (options.Length - 1) == 選択項目, -1 == キャンセル
			}
		}
	}
}
