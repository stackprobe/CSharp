using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ArrayTools
	{
		public static bool contains<T>(T[] arr, T target, Comparison<T> comp)
		{
			return indexOf<T>(arr, target, comp) != -1;
		}

		public static int indexOf<T>(T[] arr, T target, Comparison<T> comp)
		{
			for (int index = 0; index < arr.Length; index++)
				if (comp(arr[index], target) == 0)
					return index;

			return -1;
		}

		public static int arrComp<T>(T[] a, T[] b, Comparison<T> comp)
		{
			int minlen = Math.Min(a.Length, b.Length);

			for (int index = 0; index < minlen; index++)
			{
				int ret = comp(a[index], b[index]);

				if (ret != 0)
					return ret;
			}
			return IntTools.comp(a.Length, b.Length);
		}

		public static void sort<T>(T[] arr, Comparison<T> comp)
		{
			Array.Sort(arr, comp);
		}

		public static void sort<T>(List<T> list, Comparison<T> comp)
		{
			list.Sort(comp);
		}

		public static List<T> toList<T>(params T[] arr)
		{
			return toList2(arr);
		}

		public static T[] toArray<T>(List<T> list)
		{
			T[] dest = new T[list.Count];

			for (int index = 0; index < dest.Length; index++)
				dest[index] = list[index];

			return dest;
		}

		public static List<T> toList2<T>(IEnumerable<T> src)
		{
			List<T> dest = new List<T>();

			foreach (T element in src)
				dest.Add(element);

			return dest;
		}

		public static T[] toArray2<T>(IEnumerable<T> src)
		{
			return toArray<T>(toList2<T>(src));
		}

		public static List<T> repeate<T>(T element, int count)
		{
			List<T> dest = new List<T>();

			for (int index = 0; index < count; index++)
				dest.Add(element);

			return dest;
		}

		public class Reader<T>
		{
			private T[] _src;

			public Reader(T[] src)
			{
				_src = src;
			}

			private int _index = 0;

			public T next(T defval)
			{
				if (_index < _src.Length)
				{
					return _src[_index++];
				}
				return defval;
			}

			public T _defval; // = null | 0;

			public T next()
			{
				return next(_defval);
			}

			public int index
			{
				get
				{
					return _index;
				}
			}
		}
	}
}
