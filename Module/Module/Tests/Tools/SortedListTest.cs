﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class SortedListTest
	{
		public void test01()
		{
			test01(new int[] { 1, 2, 3 }, 2, 1, 1);
			test01(new int[] { 1, 2, 2, 3 }, 2, 1, 2);
			test01(new int[] { 1, 2, 2, 2, 3 }, 2, 1, 3);

			test01(new int[] { 1, 3 }, 2, 1, 0);
			test01(new int[] { 1 }, 2, 1, 0);
			test01(new int[] { 3 }, 2, 0, -1);
			test01(new int[] { }, 2, 0, -1);

			test01(new int[] { 1, 1, 1 }, 2, 3, 2);
			test01(new int[] { 2, 2, 2 }, 2, 0, 2);
			test01(new int[] { 3, 3, 3 }, 2, 0, -1);
		}

		private void test01(int[] arr, int target, int expectedLeftIndex, int expectedRightIndex)
		{
			SortedList<int> list = new SortedList<int>(IntTools.comp);

			foreach (int element in arr)
				list.add(element);

			if (list.leftIndexOf(target) != expectedLeftIndex)
				throw null;

			if (list.rightIndexOf(target) != expectedRightIndex)
				throw null;
		}
	}
}
