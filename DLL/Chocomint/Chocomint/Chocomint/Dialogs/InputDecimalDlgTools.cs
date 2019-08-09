using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public static class InputDecimalDlgTools
	{
		public static decimal Show(string title, string prompt, bool hasParent = false, decimal value = 0, decimal minval = 0, decimal maxval = 100, decimal defval = -1, Func<decimal, decimal> validator = null)
		{
			using (InputDecimalDlg f = new InputDecimalDlg())
			{
				f.Value = value;
				f.MinValue = minval;
				f.MaxValue = maxval;

				if (hasParent)
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
