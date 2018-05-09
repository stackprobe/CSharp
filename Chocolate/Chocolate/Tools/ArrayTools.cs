﻿using System;
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

			Array.Sort<int>(order, (int a, int b) =>
			{
				if (a == b)
					return 0;

				return comp(a, b);
			});

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
	}
}
