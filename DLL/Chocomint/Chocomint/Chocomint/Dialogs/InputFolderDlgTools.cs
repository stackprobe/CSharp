using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Charlotte.Chocomint.Dialogs
{
	public class InputFolderDlgTools
	{
		public static string Existing(string title, string prompt, string dir = "", string defval = null, Form parent = null)
		{
			Func<string, string> validator = v =>
			{
				if (Directory.Exists(v) == false)
					throw new Exception("指定されたフォルダは存在しません。");

				return v;
			};

			return Show(title, prompt, dir, defval, validator, parent);
		}

		public static string Show(string title, string prompt, string dir = "", string defval = null, Func<string, string> validator = null, Form parent = null)
		{
			using (InputFolderDlg f = new InputFolderDlg())
			{
				f.Value = dir;

				if (parent != null)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
					f.Prompt.Text = prompt;

					if (validator != null)
						f.Validator = validator;
				};

				f.ShowDialog();

				if (f.OkPressed)
					return f.Value;

				return defval;
			}
		}
	}
}
