using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.DateSpans.Tools
{
	public static class StringTools
	{
		public static int Comp(string a, string b)
		{
			return Comp(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
		}

		public static int Comp(byte[] a, byte[] b)
		{
			return Comp(a, b, Comp);
		}

		public static int Comp<T>(T[] a, T[] b, Comparison<T> comp)
		{
			int minlen = Math.Min(a.Length, b.Length);

			for (int index = 0; index < minlen; index++)
			{
				int ret = comp(a[index], b[index]);

				if (ret != 0)
					return ret;
			}
			return Comp(a.Length, b.Length);
		}

		public static int Comp(byte a, byte b)
		{
			return Comp((int)a, (int)b);
		}

		public static int Comp(int a, int b)
		{
			if (a < b)
				return -1;

			if (a > b)
				return 1;

			return 0;
		}
	}
}
