using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class IntTools
	{
		public const int IMAX = 1000000000;

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}

		public static int ToInt(string value, int minval, int maxval, int defval)
		{
			try
			{
				int ret = int.Parse(value);

				if (ret < minval || maxval < ret)
					throw null;

				return ret;
			}
			catch
			{
				return defval;
			}
		}
	}
}
