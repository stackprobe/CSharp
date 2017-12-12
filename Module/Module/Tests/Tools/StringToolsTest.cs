using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class StringToolsTest
	{
		public void test01()
		{
			string[] lines = new string[] { "DDD", "CCC", "BBB", "AAA" };

			Array.Sort(lines, 1, 2, new StringTools.IComp());

			if (
				lines[0] != "DDD" ||
				lines[1] != "BBB" ||
				lines[2] != "CCC" ||
				lines[3] != "AAA"
				)
				throw null;
		}
	}
}
