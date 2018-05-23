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

		public static void Sort(int count, Comparison<int> comp, Action<int, int> swap)
		{
			int[] order = new int[count];

			for (int index = 0; index < count; index++)
				order[index] = index;

			Array.Sort<int>(order, comp);

			for (int index = 0; index < count; index++)
			{
				if (order[index] != -1)
				{
					for (; ; )
					{
						int prev = index;

						index = order[index];
						order[prev] = -1;

						if (order[index] == -1)
							break;

						swap(prev, index);
					}
				}
			}
		}

		public static void Sort2(int count, Comparison<int> comp, Action<int, int> swap)
		{
			int[] order = new int[count];
			int[] elementToPosition = new int[count];
			int[] positionToElement = new int[count];

			for (int index = 0; index < count; index++)
			{
				order[index] = index;
				elementToPosition[index] = index;
				positionToElement[index] = index;
			}

#if true
			Array.Sort<int>(order, comp);
#else
			Array.Sort<int>(order, (int a, int b) =>
			{
				if (a == b)
					return 0;

				return comp(a, b);
			});
#endif

			for (int index = 0; index + 1 < count; index++)
			{
				if (order[index] != positionToElement[index])
				{
					int far = elementToPosition[order[index]];

					swap(index, far);

					{
						int e1 = positionToElement[index];
						//int e2 = positionToElement[far];

						//positionToElement[index] = e2;
						positionToElement[far] = e1;

						elementToPosition[e1] = far;
						//elementToPosition[e2] = index;
					}
				}
			}
		}

		public static void Sort_Retractable(int count, Comparison<int> comp, Action<int, int> move)
		{
			int[] order = new int[count];

			for (int index = 0; index < count; index++)
				order[index] = index;

			Array.Sort<int>(order, comp);

			for (int index = 0; index < count; index++)
			{
				if (order[index] != -1 && order[index] != index)
				{
					move(index, -1);

					for (; ; )
					{
						int prev = index;

						index = order[index];
						order[prev] = -1;

						if (order[index] == -1)
						{
							move(-1, prev);
							break;
						}
						move(index, prev);
					}
				}
			}
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

		public static List<T> ToList<T>(ICollection<T> src)
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

		public T[][] GetMergedPairs<T>(T[] arr1, T[] arr2, T defval, Comparison<T> comp)
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
	}
}
