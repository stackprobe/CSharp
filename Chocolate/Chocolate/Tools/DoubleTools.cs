using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class DoubleTools
	{
		public static double Range(double value, double minval, double maxval)
		{
			return Math.Max(minval, Math.Min(maxval, value));
		}

		public static double ToDouble(string str, double minval, double maxval, double defval)
		{
			try
			{
				return Range(double.Parse(str), minval, maxval);
			}
			catch
			{
				return defval;
			}
		}

		public static int ToInt(double value)
		{
			if (value < 0.0)
				return (int)(value - 0.5);
			else
				return (int)(value + 0.5);
		}
	}
}
