using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.TCalcs;

namespace Charlotte.Tests.TCalcs
{
	public class TCalcTest
	{
		public void Test01()
		{
			Console.WriteLine(new TCalc().Calc("1", "+", "1"));
		}
	}
}
