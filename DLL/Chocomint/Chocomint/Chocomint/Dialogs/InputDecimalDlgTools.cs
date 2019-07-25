using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public class InputDecimalDlgTools
	{
		public static decimal Show(string title, string prompt, decimal value = 0, decimal minval = 0, decimal maxval = 100, decimal defval = -1, Func<decimal, decimal> validator = null, Form parent = null)
		{
			using (InputDecimalDlg f = new InputDecimalDlg())
			{
				f.Value = value;
				f.MinValue = minval;
				f.MaxValue = maxval;

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
