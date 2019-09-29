using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class RandomUnitTest
	{
		public void Test01()
		{
			for (int c = 0; c < 1000; c++)
			{
				Console.WriteLine(string.Join(", ",
					SecurityTools.CRandom.GetUInt16().ToString("x8"),
					SecurityTools.CRandom.GetUInt24().ToString("x8"),
					SecurityTools.CRandom.GetUInt().ToString("x8"),
					SecurityTools.CRandom.GetUInt64().ToString("x16")
					));
			}
		}

		public void Test02()
		{
			for (int c = 0; c < 1000; c++)
			{
				Console.WriteLine(string.Join(", ",
					SecurityTools.CRandom.GetRandom64(1),
					SecurityTools.CRandom.GetRandom64(2),
					SecurityTools.CRandom.GetRandom64(3),
					SecurityTools.CRandom.GetRandom64(4)
					));
			}
		}
	}
}
