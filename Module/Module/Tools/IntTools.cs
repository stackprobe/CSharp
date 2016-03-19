using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class IntTools
	{
		public const int IMAX = 1000000000;

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (b < a)
				return 1;

			return 0;
		}

		public static bool IsRange(int value, int minval = 0, int maxval = IMAX)
		{
			return minval <= value && value <= maxval;
		}

		public static int ToRange(int value, int minval = 0, int maxval = IMAX)
		{
			return Math.Min(Math.Max(value, minval), maxval);
		}

		public int Parse(string str, int minval = 0, int maxval = IMAX, int defval = -1)
		{
			try
			{
				int value = int.Parse(str);

				if (IsRange(value, minval, maxval))
				{
					return value;
				}
			}
			catch
			{ }

			return defval;
		}
	}
}
