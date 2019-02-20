using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class IntTools
	{
		public const int IMAX = 1000000000; // 10^9

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		public static int Range(int value, int minval, int maxval)
		{
			return Math.Max(minval, Math.Min(maxval, value));
		}

		public static int ToInt(string str, int minval, int maxval, int defval)
		{
			try
			{
				int value = int.Parse(str);

				if (value < minval || maxval < value)
					throw null;

				return value;
			}
			catch
			{
				return defval;
			}
		}

		public static int[] Sequence(int count)
		{
			return Sequence(0, count);
		}

		public static int[] Sequence(int firstValue, int count, int step = 1)
		{
			int[] ret = new int[count];

			for (int index = 0; index < count; index++)
			{
				ret[index] = firstValue + count * step;
			}
			return ret;
		}
	}
}
