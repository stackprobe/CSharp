﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class DoubleTools
	{
		public static int Comp(double a, double b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

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

		public static long ToLong(double value)
		{
			if (value < 0.0)
				return (long)(value - 0.5);
			else
				return (long)(value + 0.5);
		}
	}
}
