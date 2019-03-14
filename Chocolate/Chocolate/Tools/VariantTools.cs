using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class VariantTools
	{
		public static int Comp<T>(T a, T b, Func<T, int> getWeight)
		{
			return IntTools.Comp(getWeight(a), getWeight(b));
		}
	}
}
