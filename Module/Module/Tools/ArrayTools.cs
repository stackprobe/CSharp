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

			foreach(T[] part in src)
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
	}
}
