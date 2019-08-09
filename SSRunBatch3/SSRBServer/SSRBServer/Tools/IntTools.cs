using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public static class IntTools
	{
		public const int IMAX = 1000000000;

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
	}
}
