using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ArrayTools
	{
		public static T[] Join<T>(T[][] src)
		{
			int count = 0;

			foreach (T[] part in src)
				count += part.Length;

			T[] dest = new T[count];
			count = 0;

			foreach (T[] part in src)
			{
				Array.Copy(part, 0, dest, count, part.Length);
				count += part.Length;
			}
			return dest;
		}

		public static T[] SubArray<T>(T[] src, int startPos, int count)
		{
			T[] dest = new T[count];
			Array.Copy(src, startPos, dest, 0, count);
			return dest;
		}

		public static T[] Insert<T>(T[] src, int insertPos, T[] appendix)
		{
			T[] dest = new T[src.Length + appendix.Length];
			Array.Copy(src, 0, dest, 0, insertPos);
			Array.Copy(appendix, 0, dest, insertPos, appendix.Length);
			Array.Copy(src, insertPos, dest, insertPos + appendix.Length, src.Length - insertPos);
			return dest;
		}

		public static T[] Add<T>(T[] src, T[] appendix)
		{
			return Insert(src, src.Length, appendix);
		}

		public static T[] Remove<T>(T[] src, int removePos, int count)
		{
			T[] dest = new T[src.Length - count];
			Array.Copy(src, 0, dest, 0, removePos);
			Array.Copy(src, removePos + count, dest, removePos, src.Length - count - removePos);
			return dest;
		}

		public static void Swap<T>(T[] array, int index1, int index2)
		{
			T tmp = array[index1];
			array[index1] = array[index2];
			array[index2] = tmp;
		}

		public static void Shuffle<T>(T[] array)
		{
			for (int index = array.Length; 1 < index; index--)
			{
				Swap(array, MathTools.Random(index), index - 1);
			}
		}
	}
}
