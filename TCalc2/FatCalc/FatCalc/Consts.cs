using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace Charlotte
{
	public class Consts
	{
		private static Color _defForeColor;

		public static Color DefForeColor
		{
			get
			{
				if (_defForeColor == null)
					_defForeColor = new TextBox().ForeColor;

				return _defForeColor;
			}
		}

		private static Color _defBackColor;

		public static Color DefBackColor
		{
			get
			{
				if (_defBackColor == null)
					_defBackColor = new TextBox().BackColor;

				return _defBackColor;
			}
		}

		public static readonly Color AnswerForeColor = Color.DarkGreen;
		public static readonly Color AnswerBackColor = Color.FromArgb(240, 255, 255);
		public static readonly Color ErrorForeColor = Color.Red;
		public static readonly Color ErrorBackColor = Color.FromArgb(255, 255, 220);

		public const int PR_OPERATION_POWER = 0;
		public const int PR_OPERATION_ROOT = 1;
	}
}
