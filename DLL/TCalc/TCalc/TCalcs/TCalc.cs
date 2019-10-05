using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.TCalcs
{
	public class TCalc
	{
		private int Radix;
		private int Basement;

		public TCalc(int radix = 10, int basement = 30)
		{
			if (radix < 2 || IntTools.IMAX < radix)
				throw new ArgumentException("Bad radix: " + radix);

			if (basement < 0 || IntTools.IMAX < basement)
				throw new ArgumentException("Bad basement: " + basement);

			this.Radix = radix;
			this.Basement = basement;
		}

		public string Calc(string leftOperand, string operation, string rightOperand)
		{
			int divBasement = 0;

			if (operation == "/")
				divBasement = this.Basement;

			FatConverter conv = new FatConverter(this.Radix);

			conv.SetString(leftOperand);
			FatFloat a = conv.GetFloat();
			conv.SetString(rightOperand);
			conv.Exponent -= divBasement;
			FatFloat b = conv.GetFloat();

			FatFloat ans = this.CalcMain(a, operation, b, conv.Rdx);

			conv.SetFloat(ans);
			conv.Exponent -= divBasement;
			string answer = conv.GetString(this.Basement);

			return answer;
		}

		private FatFloat CalcMain(FatFloat a, string operation, FatFloat b, int radix)
		{
			if (operation == "+")
			{
				new FatFloatCalc(radix).Add(a, b);
				return a;
			}
			if (operation == "-")
			{
				new FatFloatCalc(radix).Sub(a, b);
				return a;
			}
			if (operation == "*")
			{
				return new FatFloatCalc(radix).Mul(a, b);
			}
			if (operation == "/")
			{
				return new FatFloatCalc(radix).Div(a, b);
			}
			throw new ArgumentException("Bad operator: " + operation);
		}
	}
}
