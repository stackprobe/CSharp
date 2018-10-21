using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class SyncListTest
	{
		public void Test01()
		{
			Test01a(2, 1, 2, 3);
			Test01a(0, 1, 2, 3);
			Test01a(1, 1, 1, 1);
			Test01a(1, 1);
			Test01a(0);
		}

		private void Test01a(params int[] values)
		{
			int c = 0;
			int trgval = values[c++];

			SyncList<int> list = new SyncList<int>();

			while (c < values.Length)
				list.Add(values[c++]);

			list.RemoveAll(value => value == trgval);

			Console.WriteLine("[" + string.Join(", ", list.ToArray().Select(value => "" + value)) + "]");
		}
	}
}
