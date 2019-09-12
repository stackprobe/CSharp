using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Utils2;

namespace Charlotte.Tests.Utils2
{
	public class ArrayUtilsTest
	{
		public void Test01()
		{
			Test01_a(new int[] { 1, 1, 1, 2, 2, 3, 5, 7, 7, 7, 7 }, new int[] { 1, 2, 3, 5, 7 });
			Test01_a(new int[] { 1, 2, 3, 4, 4, 4, 5, 6, 7 }, new int[] { 1, 2, 3, 4, 5, 6, 7 });
			Test01_a(new int[] { 1, 2, 1, 2, 1, 2 }, new int[] { 1, 2 }); // ソートする。
		}

		private void Test01_a(int[] testInput, int[] expectOutput)
		{
			List<int> tmp = new List<int>(testInput);
			ArrayUtils.Distinct(tmp, (a, b) => a - b);
			int[] output = tmp.ToArray();

			if (ArrayTools.Comp(output, expectOutput, (a, b) => a - b) != 0)
				throw null;

			Console.WriteLine("Test01_a() OK!");
		}
	}
}
