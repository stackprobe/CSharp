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
			for (int c = 0; c < 10; c++)
			{
				for (int d = 0; d < 10; d++)
				{
					for (int e = 0; e < 10; e++)
					{
						Test01_b(d, e);
					}
				}
			}
			for (int c = 0; c < 1000; c++)
			{
				Test01_b(SecurityTools.CRandom.GetRange(0, 1000), 0);
			}
			for (int c = 0; c < 1000; c++)
			{
				Test01_b(SecurityTools.CRandom.GetRange(0, 1000), SecurityTools.CRandom.GetRange(0, 1000));
			}
		}

		private void Test01_b(int count, int cpCount)
		{
			string[] a = MakeRandStrings(count, cpCount);
			string[] b = new string[count];

			Array.Copy(a, b, count);

			Sort_A(a);
			Sort_B(b);

			Test01_Check(a, b);
		}

		private string[] MakeRandStrings(int count, int cpCount)
		{
			string[] dest = new string[count];

			for (int index = 0; index < count; index++)
			{
				dest[index] = SecurityTools.MakePassword(StringTools.DECIMAL, SecurityTools.CRandom.GetRange(10, 100));
			}
			if (2 <= count)
			{
				for (int index = 0; index < cpCount; index++)
				{
					int a = SecurityTools.CRandom.GetRange(0, count - 1);
					int b = SecurityTools.CRandom.GetRange(0, count - 1);

					dest[a] = dest[b];
				}
			}
			return dest;
		}

		private void Sort_A(string[] lines)
		{
			Array.Sort<string>(lines, StringTools.Comp);
		}

		private void Sort_B(string[] lines)
		{
			ArrayTools.Sort(
				lines.Length,
				(int a, int b) => StringTools.Comp(lines[a], lines[b]),
				(int a, int b) => ArrayTools.Swap(lines, a, b)
				);
		}

		private void Test01_Check(string[] a, string[] b)
		{
			if (ArrayTools.Comp(a, b, StringTools.Comp) != 0)
			{
				throw null;
			}
		}
	}
}
