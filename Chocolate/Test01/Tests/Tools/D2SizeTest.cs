using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class D2SizeTest
	{
		public void Test01()
		{
			Console.WriteLine("D2SizeTest");

			Test01_a();
			Test01_b();
		}

		private void Test01_a()
		{
			D2Size sz = new D2Size(2.0, 3.0);

			Console.WriteLine(sz.W + ", " + sz.H);
			sz = sz * 5;
			Console.WriteLine(sz.W + ", " + sz.H);
			sz = sz / 7;
			Console.WriteLine(sz.W + ", " + sz.H);
		}

		private void Test01_b()
		{
			D2Size sz = new D2Size(2.0, 3.0);

			Console.WriteLine(sz.W + ", " + sz.H);
			sz *= 5;
			Console.WriteLine(sz.W + ", " + sz.H);
			sz /= 7;
			Console.WriteLine(sz.W + ", " + sz.H);
		}
	}
}
