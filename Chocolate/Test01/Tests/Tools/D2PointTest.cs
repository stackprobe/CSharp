using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class D2PointTest
	{
		public void Test01()
		{
			D2Point pt = new D2Point(2.0, 3.0);

			pt = pt * 5;

			Console.WriteLine(pt.X + ", " + pt.Y);

			pt = pt / 7;

			Console.WriteLine(pt.X + ", " + pt.Y);
		}
	}
}
