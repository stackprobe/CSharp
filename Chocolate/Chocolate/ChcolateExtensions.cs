using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class ChcolateExtensions
	{
		public static void ForEach<T>(this IEnumerable<T> src, Action<T> rtn)
		{
			foreach (T element in src)
			{
				rtn(element);
			}
		}
	}
}
