using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ArrayTools
	{
		public static T[] GetPart<T>(T[] src, int startPos, int count)
		{
			T[] dest = new T[count];
			Array.Copy(src, startPos, dest, 0, count);
			return dest;
		}

		public static T[] Join<T>(T[][] src)
		{
			int count = 0;

			foreach (T[] part in src)
				count += part.Length;

			T[] dest = new T[count];

			foreach (T[] part in src)
			{
				Array.Copy(part, 0, dest, count, part.Length);
				count += part.Length;
			}
			return dest;
		}

		public static void Swap<T>(T[] array, int i, int j)
		{
			T tmp = array[i];
			array[i] = array[j];
			array[j] = tmp;
		}

		public static void Swap<T>(List<T> list, int i, int j)
		{
			T tmp = list[i];
			list[i] = list[j];
			list[j] = tmp;
		}

		public static void Shuffle<T>(T[] array)
		{
			for (int index = array.Length; 1 < index; index--)
			{
				Swap(array, MathTools.Random(index), index - 1);
			}
		}

		public static void Shuffle<T>(List<T> list)
		{
			for (int index = list.Count; 1 < index; index--)
			{
				Swap(list, MathTools.Random(index), index - 1);
			}
		}
	}
}
