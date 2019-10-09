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
			Test01_b("1", "+", "1");
			Test01_b("1", "-", "1");
			Test01_b("1", "-", "2");
			Test01_b("1", "/", "2");
			Test01_b("1", "/", "3");
			Test01_b("3.1622776601", "*", "3.1622776601");
			Test01_b("3.1622776602", "*", "3.1622776602");
			Test01_b("3.1622776601", "/", "3.1622776601");
			Test01_b("1.25", "+", "1.75");
			Test01_b("6.33", "-", "3.33");
			Test01_b("1.25", "*", "2.40");
			Test01_b("9.63", "/", "3.21");
		}

		private void Test01_b(string leftOperand, string operation, string rightOperand)
		{
			Console.WriteLine(leftOperand + " " + operation + " " + rightOperand + " = " + new TCalc().Calc(leftOperand, operation, rightOperand));
		}
	}
}
