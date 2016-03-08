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

		public static bool IsRange(int value, int minval, int maxval)
		{
			return minval <= value && value <= maxval;
		}
	}
}
