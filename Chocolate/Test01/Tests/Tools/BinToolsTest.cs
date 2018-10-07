using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class BinToolsTest
	{
		public void Test01()
		{
			for (int value = -10000; value <= 10000; value++)
			{
				Test01a(value);
			}
			for (int value = -2100000000; value <= 2100000000; value += 10000)
			{
				Test01a(value);
			}
			Test01a(int.MinValue);
			Test01a(int.MaxValue);

			Console.WriteLine("OK!");
		}

		private void Test01a(int value)
		{
			int ret = BinTools.ToInt(BinTools.ToBytes(value));

			if (ret != value)
				throw null;
		}
	}
}
