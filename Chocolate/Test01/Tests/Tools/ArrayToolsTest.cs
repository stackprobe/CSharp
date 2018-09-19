using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class ArrayToolsTest
	{
		public void Test01()
		{
			Test01_a(new int[] { 1, 1, 1, 2, 2, 3, 5, 7, 7, 7, 7 }, new int[] { 1, 2, 3, 5, 7 });
			Test01_a(new int[] { 1, 2, 3, 4, 4, 4, 5, 6, 7 }, new int[] { 1, 2, 3, 4, 5, 6, 7 });
			Test01_a(new int[] { 1, 2, 1, 2, 1, 2 }, new int[] { 1, 2, 1, 2, 1, 2 }); // 勝手にソートはしない。
		}

		private void Test01_a(int[] testInput, int[] expectOutput)
		{
			int[] output = ArrayTools.Distinct(testInput, (a, b) => a - b).ToArray();

			if (ArrayTools.Comp(output, expectOutput, (a, b) => a - b) != 0)
				throw null;
		}
	}
}
