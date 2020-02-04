using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace Charlotte
{
	public static class Consts
	{
		public const int MPIC_W_MIN = 10;
		public const int MPIC_H_MIN = 10;
		public const int MPIC_W_MAX = 10000;
		public const int MPIC_H_MAX = 10000;

		public static readonly Color DefaultBackColor = new Button().BackColor;
		public static readonly Color DefaultForeColor = new Button().ForeColor;

		public const int HISTORY_MAX = 999;
	}
}
