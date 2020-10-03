using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public static class ChcolateExtensions
	{
		#region IEnumerable<T>

		public static void ForEach<T>(this IEnumerable<T> src, Action<T> routine)
		{
			foreach (T element in src)
			{
				routine(element);
			}
		}

		#endregion

		#region string

#if false // こちらに置き換える予定
		public static bool EqualsIgnoreCase(this string a, string b)
		{
			return StringTools.EqualsIgnoreCase(a, b);
		}

		public static bool StartsWithIgnoreCase(this string str, string ptn)
		{
			return StringTools.StartsWithIgnoreCase(str, ptn);
		}

		public static bool EndsWithIgnoreCase(this string str, string ptn)
		{
			return StringTools.EndsWithIgnoreCase(str, ptn);
		}

		public static bool ContainsIgnoreCase(this string str, string ptn)
		{
			return StringTools.ContainsIgnoreCase(str, ptn);
		}

		public static int IndexOfIgnoreCase(this string str, string ptn)
		{
			return StringTools.IndexOfIgnoreCase(str, ptn);
		}

		public static int IndexOfIgnoreCase(this string str, char chr)
		{
			return StringTools.IndexOfIgnoreCase(str, chr);
		}

		public static bool Contains(this string str, Predicate<char> match)
		{
			return StringTools.Contains(str, match);
		}

		public static int IndexOf(this string str, Predicate<char> match)
		{
			return StringTools.IndexOf(str, match);
		}
#endif

		#endregion
	}
}
