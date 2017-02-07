using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class IntTools
	{
		public const int IMAX = 1000000000;

		public static int toInt(string str, int minval = 0, int maxval = IMAX)
		{
			return toRange(int.Parse(str), minval, maxval);
		}

		public static int toRange(int value, int minval = 0, int maxval = IMAX)
		{
			return Math.Min(
				Math.Max(value, minval),
				maxval
				);
		}

		public static int toInt(string str, int minval, int maxval, int defval)
		{
			try
			{
				return toRange(int.Parse(str), minval, maxval, defval);
			}
			catch
			{
				return defval;
			}
		}

		public static int toRange(int value, int minval, int maxval, int defval)
		{
			return isRange(value, minval, maxval) ? value : defval;
		}

		public static bool isRange(int value, int minval, int maxval)
		{
			return minval <= value && value <= maxval;
		}
	}
}
