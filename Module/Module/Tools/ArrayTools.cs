using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ArrayTools
	{
		public static bool Contains<T>(T[] arr, T target, Comparison<T> comp)
		{
			return IndexOf<T>(arr, target, comp) != -1;
		}

		public static int IndexOf<T>(T[] arr, T target, Comparison<T> comp)
		{
			for (int index = 0; index < arr.Length; index++)
				if (comp(arr[index], target) == 0)
					return index;

			return -1;
		}
	}
}
