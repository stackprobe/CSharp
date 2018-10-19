using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ArrayTools
	{
		public static int Comp<T>(T[] a, T[] b, Comparison<T> comp)
		{
			int minlen = Math.Min(a.Length, b.Length);

			for (int index = 0; index < minlen; index++)
			{
				int ret = comp(a[index], b[index]);

				if (ret != 0)
					return ret;
			}
			return IntTools.Comp(a.Length, b.Length);
		}

		public static void Swap<T>(T[] arr, int a, int b)
		{
			T tmp = arr[a];
			arr[a] = arr[b];
			arr[b] = tmp;
		}

		public static int IndexOf<T>(T[] arr, T target, Comparison<T> comp, int defval = -1)
		{
			for (int index = 0; index < arr.Length; index++)
				if (comp(arr[index], target) == 0)
					return index;

			return defval;
		}

		public static int IndexOf<T>(T[] arr, Func<T, bool> predicate, int defval = -1)
		{
			for (int index = 0; index < arr.Length; index++)
				if (predicate(arr[index]))
					return index;

			return defval;
		}

		public static bool Contains<T>(T[] arr, T target, Comparison<T> comp)
		{
			return IndexOf<T>(arr, target, comp) != -1;
		}

		public static bool Contains<T>(T[] arr, Func<T, bool> predicate)
		{
			return IndexOf<T>(arr, predicate) != -1;
		}

		public static List<T> ToList<T>(IEnumerable<T> src)
		{
			List<T> dest = new List<T>();

			foreach (T element in src)
				dest.Add(element);

			return dest;
		}

		public static T[] ToArray<T>(List<T> src)
		{
			T[] dest = new T[src.Count];

			for (int index = 0; index < src.Count; index++)
				dest[index] = src[index];

			return dest;
		}

		public static T[] Repeat<T>(T element, int count)
		{
			T[] dest = new T[count];

			for (int index = 0; index < count; index++)
				dest[index] = element;

			return dest;
		}

		public static void Merge<T>(T[] arr1, T[] arr2, List<T> destOnly1, List<T> destBoth1, List<T> destBoth2, List<T> destOnly2, Comparison<T> comp)
		{
			Array.Sort(arr1, comp);
			Array.Sort(arr2, comp);

			int index1 = 0;
			int index2 = 0;

			for (; ; )
			{
				int ret;

				if (arr1.Length <= index1)
				{
					if (arr2.Length <= index2)
						break;

					ret = 1;
				}
				else if (arr2.Length <= index2)
				{
					ret = -1;
				}
				else
				{
					ret = comp(arr1[index1], arr2[index2]);
				}

				if (ret < 0)
				{
					if (destOnly1 != null)
						destOnly1.Add(arr1[index1]);

					index1++;
				}
				else if (0 < ret)
				{
					if (destOnly2 != null)
						destOnly2.Add(arr2[index2]);

					index2++;
				}
				else
				{
					if (destBoth1 != null)
						destBoth1.Add(arr1[index1]);

					if (destBoth2 != null)
						destBoth2.Add(arr2[index2]);

					index1++;
					index2++;
				}
			}
		}

		public static T[][] GetMergedPairs<T>(T[] arr1, T[] arr2, T defval, Comparison<T> comp)
		{
			Array.Sort(arr1, comp);
			Array.Sort(arr2, comp);

			int index1 = 0;
			int index2 = 0;

			List<T[]> dest = new List<T[]>();

			for (; ; )
			{
				int ret;

				if (arr1.Length <= index1)
				{
					if (arr2.Length <= index2)
						break;

					ret = 1;
				}
				else if (arr2.Length <= index2)
				{
					ret = -1;
				}
				else
				{
					ret = comp(arr1[index1], arr2[index2]);
				}

				if (ret < 0)
				{
					dest.Add(new T[] { arr1[index1++], defval });
				}
				else if (0 < ret)
				{
					dest.Add(new T[] { defval, arr2[index2++] });
				}
				else
				{
					dest.Add(new T[] { arr1[index1++], arr2[index2++] });
				}
			}
			return dest.ToArray();
		}

		public static void Transfer<T>(Action<T[], long, int> readerWriter, long index, long size, int buffSize = 65536)
		{
			T[] buff = new T[buffSize];

			for (long offset = 0L; offset < size; )
			{
				int readSize = (int)Math.Min((long)buffSize, size - offset);

				readerWriter(buff, index + offset, readSize);
				offset += (long)readSize;
			}
		}

		public static IEnumerable<T> Distinct<T>(IEnumerable<T> src, Comparison<T> comp)
		{
			IEnumerator<T> reader = src.GetEnumerator();

			if (reader.MoveNext())
			{
				T lastElement = reader.Current;

				yield return lastElement;

				while (reader.MoveNext())
				{
					T element = reader.Current;

					if (comp(element, lastElement) != 0)
					{
						yield return element;

						lastElement = element;
					}
				}
			}
		}
	}
}
