using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class ArrayTools
	{
		public static List<T> GetRange<T>(List<T> src, int startPos)
		{
			return GetRange(src, startPos, src.Count - startPos);
		}

		public static List<T> GetRange<T>(List<T> src, int startPos, int count)
		{
			List<T> dest = new List<T>();

			for (int index = 0; index < count; index++)
				dest.Add(src[startPos + index]);

			return dest;
		}

		public static List<T> GetClone<T>(List<T> src)
		{
			List<T> dest = new List<T>();

			foreach (T element in src)
				dest.Add(element);

			return dest;
		}

		public static T GetOne<T, U>(List<U> src) where T : class
		{
			foreach (U element in src)
			{
				T ret = element as T;

				if (ret == null)
					continue;

				return ret;
			}
			return null; // not found
		}

		public static List<T> GetPlur<T, U>(List<U> src) where T : class
		{
			List<T> ret = new List<T>();

			foreach (U element in src)
			{
				T elem = element as T;

				if (elem == null)
					continue;

				ret.Add(elem);
			}
			return ret;
		}

		public static void FastRemoveAt<T>(List<T> list, int index)
		{
			list[index] = list[list.Count - 1];
			list.RemoveAt(list.Count - 1);
		}

		public static bool IsSame<T>(List<T> list1, List<T> list2, SystemTools.IsSame_d<T> d_isSame)
		{
			if (list1 == null && list2 == null)
				return true;

			if (list1 == null || list2 == null)
				return false;

			if (list1.Count != list2.Count)
				return false;

			for (int index = 0; index < list1.Count; index++)
				if (d_isSame(list1[index], list2[index]) == false)
					return false;

			return true;
		}

		public static bool IsSameRange<T>(List<T> list1, int startPos1, List<T> list2, int startPos2, SystemTools.IsSame_d<T> d_isSame, int count)
		{
			for (int index = 0; index < count; index++)
				if (d_isSame(list1[startPos1 + index], list2[startPos2 + index]) == false)
					return false;

			return true;
		}

		public static void RemoveTopLoop<T>(List<T> list, int count)
		{
			for (int index = 0; index < count; index++)
				list.RemoveAt(0);
		}

		public static void RemoveTailLoop<T>(List<T> list, int count)
		{
			for (int index = 0; index < count; index++)
				list.RemoveAt(list.Count - 1);
		}

		public static bool Contains(byte[] block, byte target)
		{
			foreach (byte chr in block)
				if (chr == target)
					return true;

			return false;
		}

		/// <summary>
		/// XXX
		/// </summary>
		/// <typeparam name="T"></typeparam>
		/// <param name="list"></param>
		public static void RemoveNull<T>(List<T> list)
		{
			for (int index = 0; index < list.Count; index++)
			{
				if (list[index] == null)
				{
					list.RemoveAt(index);
					index--;
				}
			}
		}
	}
}
