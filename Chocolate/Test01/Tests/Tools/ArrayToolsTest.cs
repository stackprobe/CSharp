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
			string[] b2 = new string[count];
			string[] c = new string[count];

			Array.Copy(a, b, count);
			Array.Copy(a, b2, count);
			Array.Copy(a, c, count);

			Sort_A(a);
			Sort_B(b);
			Sort_B2(b2);
			Sort_C(c);

			Test01_Check(a, b);
			Test01_Check(a, b2);
			Test01_Check(a, c);

			//Test01_Check(b, b2);
			//Test01_Check(b, c);
			//Test01_Check(b2, c);
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

		private void Sort_B2(string[] lines)
		{
			ArrayTools.Annex.Sort2(
				lines.Length,
				(int a, int b) => StringTools.Comp(lines[a], lines[b]),
				(int a, int b) => ArrayTools.Swap(lines, a, b)
				);
		}

		private void Sort_C(string[] lines)
		{
			string store = null;

			ArrayTools.Annex.Sort_Retractable(
				lines.Length,
				(int a, int b) => StringTools.Comp(lines[a], lines[b]),
				(int a, int b) =>
				{
					if (a == -1)
					{
						if (b == -1) throw null;
						if (store == null) throw null; // 元
						if (lines[b] != null) throw null; // 先

						lines[b] = store;
						store = null;
					}
					else if (b == -1)
					{
						//if (a == -1) throw null;
						if (lines[a] == null) throw null; // 元
						if (store != null) throw null; // 先

						store = lines[a];
						lines[a] = null;
					}
					else
					{
						if (lines[a] == null) throw null; // 元
						if (lines[b] != null) throw null; // 先

						lines[b] = lines[a];
						lines[a] = null;
					}
				}
				);

			if (store != null) throw null;
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
