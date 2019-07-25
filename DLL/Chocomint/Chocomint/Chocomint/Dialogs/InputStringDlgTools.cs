using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public class InputStringDlgTools
	{
		public static double Double(string title, string prompt, double value = 0.0, double minval = (double)-IntTools.IMAX, double maxval = (double)IntTools.IMAX, double defval = 0.0, Form parent = null)
		{
			Func<string, string> validator = v =>
			{
				double ret = double.Parse(v);

				if (ret < minval || maxval < ret)
					throw new Exception(OutOfRangeErrorMessage(minval, maxval));

				return "" + ret;
			};

			return DoubleTools.ToDouble(Show(title, prompt, "" + value, 300, null, true, validator, parent), minval, maxval, defval);
		}

		public static long Long(string title, string prompt, long value = 0L, long minval = 0L, long maxval = LongTools.IMAX_64, long defval = -1L, Form parent = null)
		{
			Func<string, string> validator = v =>
			{
				long ret = long.Parse(v);

				if (ret < minval || maxval < ret)
					throw new Exception(OutOfRangeErrorMessage(minval, maxval));

				return "" + ret;
			};

			//return long.Parse(Show(title, prompt, "" + value, 20, "" + defval, true, validator, parent)); // same
			return LongTools.ToLong(Show(title, prompt, "" + value, 20, null, true, validator, parent), minval, maxval, defval);
		}

		public static int Int(string title, string prompt, int value = 0, int minval = 0, int maxval = IntTools.IMAX, int defval = -1, Form parent = null)
		{
			Func<string, string> validator = v =>
			{
				int ret = int.Parse(v);

				if (ret < minval || maxval < ret)
					throw new Exception(OutOfRangeErrorMessage(minval, maxval));

				return "" + ret;
			};

			//return int.Parse(Show(title, prompt, "" + value, 11, "" + defval, true, validator, parent)); // same
			return IntTools.ToInt(Show(title, prompt, "" + value, 11, null, true, validator, parent), minval, maxval, defval);
		}

		private static string OutOfRangeErrorMessage(object minval, object maxval)
		{
			return string.Format("{0} 以上 {1} 以下 でなければなりません。", minval, maxval);
		}

		public static string Show(string title, string prompt, string value = "", int maxlen = 300, string defval = null, bool rightAlign = false, Func<string, string> validator = null, Form parent = null)
		{
			using (InputStringDlg f = new InputStringDlg())
			{
				f.Value = value;

				if (parent != null)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
					f.Prompt.Text = prompt;
					f.TextValue.MaxLength = maxlen;

					if (rightAlign)
						f.TextValue.TextAlign = HorizontalAlignment.Right;

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
