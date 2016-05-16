using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class CalcTest
	{
		private static Calc _calc = new Calc();

		public static void Test01()
		{
			for (int a = 0; a < 10; a++)
				for (int b = 0; b < 10; b++)
					Test01_b("" + a, "" + b);

			for (int a = -5; a <= 5; a++)
				for (int b = -5; b <= 5; b++)
					Test01_b("" + a, "" + b);
		}

		private static void Test01_b(string a, string b)
		{
			Test01_b2(a, '+', b);
			Test01_b2(a, '-', b);
			Test01_b2(a, '*', b);
			Test01_b2(a, '/', b);
			Test01_b2(a, '%', b);
		}

		private static void Test01_b2(string a, char operation, string b)
		{
			string ans;
			try
			{
				ans = _calc.Execute(a, operation, b);
			}
			catch (Exception e)
			{
				ans = e.Message;
			}
			DebugTools.WriteLog(a + " " + operation + " " + b + " = " + ans);
		}
	}
}
