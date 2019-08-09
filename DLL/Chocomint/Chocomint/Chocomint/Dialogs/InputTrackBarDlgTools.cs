using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public static class InputTrackBarDlgTools
	{
		public static int Show(string title, string prompt, bool hasParent = false, int value = 0, int minval = 0, int maxval = 10, int defval = -1, Func<int, int> validator = null)
		{
			using (InputTrackBarDlg f = new InputTrackBarDlg())
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

					int tf = GetTickFrequency(maxval - minval);

					f.BarValue.TickFrequency = tf;
					//f.BarValue.SmallChange = 1; // デフォルト
					f.BarValue.LargeChange = tf;

					if (validator != null)
						f.Validator = validator;
				};

				f.ShowDialog();

				if (f.OkPressed)
					return f.Value;

				return defval;
			}
		}

		private static int GetTickFrequency(int range)
		{
			for (int i = DoubleTools.ToInt(Math.Sqrt((double)range)); 2 <= i; i--)
				if (range % i == 0)
					return i;

			return 1;
		}
	}
}
