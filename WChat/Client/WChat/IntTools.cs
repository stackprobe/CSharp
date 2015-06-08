using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class IntTools
	{
		public static bool IsRange(int value, int minval, int maxval)
		{
			return minval <= value && value <= maxval;
		}

		public static int ToRange(int value, int minval, int maxval)
		{
			return Math.Min(Math.Max(value, minval), maxval);
		}

		public static int TryParse(string str, int defval)
		{
			try
			{
				return int.Parse(str);
			}
			catch
			{
				return defval;
			}
		}
	}
}
