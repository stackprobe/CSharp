using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Utils2
{
	public static class ArrayUtils
	{
		public static void Distinct<T>(List<T> list, Comparison<T> comp)
		{
			list.Sort(comp);

			for (int r = list.Count - 1; 0 <= r; )
			{
				int l;

				for (l = r - 1; 0 <= l; l--)
					if (comp(list[l], list[r]) != 0)
						break;

				l += 2;

				if (l <= r)
					list.RemoveRange(l, r - l + 1);

				r = l - 2;
			}
		}
	}
}
