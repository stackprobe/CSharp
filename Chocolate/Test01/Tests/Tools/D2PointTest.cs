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
			Console.WriteLine("D2PointTest");

			Test01_a();
			Test01_b();
		}

		private void Test01_a()
		{
			D2Point pt = new D2Point(2.0, 3.0);

			Console.WriteLine(pt.X + ", " + pt.Y);
			pt = pt * 5;
			Console.WriteLine(pt.X + ", " + pt.Y);
			pt = pt / 7;
			Console.WriteLine(pt.X + ", " + pt.Y);
		}

		private void Test01_b()
		{
			D2Point pt = new D2Point(2.0, 3.0);

			Console.WriteLine(pt.X + ", " + pt.Y);
			pt *= 5;
			Console.WriteLine(pt.X + ", " + pt.Y);
			pt /= 7;
			Console.WriteLine(pt.X + ", " + pt.Y);
		}
	}
}
