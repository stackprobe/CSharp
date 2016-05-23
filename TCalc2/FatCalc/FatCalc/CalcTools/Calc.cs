using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.CalcTools
{
	public class Calc
	{
		public const UInt64 DEF_RADIX = 10;
		public const int DEF_BASEMENT = 30;
		public const int DEF_BRACKET_MIN = 36;

		private UInt64 _radix; // 2 ～ 2^64-1
		private int _basement; // 0 ～ IMAX
		private int _bracketMin; // 0 ～ 36

		public UInt64 Radix
		{
			get
			{
				return _radix;
			}
			set
			{
				if (value < 2) throw new ArgumentOutOfRangeException();
				_radix = value;
			}
		}

		public int Basement
		{
			get
			{
				return _basement;
			}
			set
			{
				if (value < 0 || IntTools.IMAX < value) throw new ArgumentOutOfRangeException();
				_basement = value;
			}
		}

		public int BracketMin
		{
			get
			{
				return _bracketMin;
			}
			set
			{
				if (value < 0 || 36 < value) throw new ArgumentOutOfRangeException();
				_bracketMin = value;
			}
		}

		public Calc(UInt64 radix = DEF_RADIX, int basement = DEF_BASEMENT, int bracketMin = DEF_BRACKET_MIN)
		{
			if (radix < 2) throw new ArgumentOutOfRangeException();
			if (basement < 0 || IntTools.IMAX < basement) throw new ArgumentOutOfRangeException();
			if (bracketMin < 0 || 36 < bracketMin) throw new ArgumentOutOfRangeException();

			_radix = radix;
			_basement = basement;
			_bracketMin = bracketMin;
		}

		public string Execute(string leftOperand, char operation, string rightOperand)
		{
			if (leftOperand == null) throw new ArgumentNullException();
			if (rightOperand == null) throw new ArgumentNullException();

			FatFloat a = FatValue.GetFatFloat(leftOperand, _radix);
			FatFloat b = FatValue.GetFatFloat(rightOperand, _radix);
			FatFloat ans;

			switch (operation)
			{
				case '+': ans = FatFloat.Add(a, b); break;
				case '-': ans = FatFloat.Red(a, b); break;
				case '*': ans = FatFloat.Mul(a, b); break;
				case '/': ans = FatFloat.Div(a, b, _basement); break;
				case '%': ans = FatFloat.Mod(a, b, _basement); break;

				default:
					throw new ArgumentException();
			}
			string ret = FatValue.GetString(ans, _bracketMin);
			return ret;
		}

		public string Power(string operand, int exponent)
		{
			if (operand == null) throw new ArgumentNullException();
			if (exponent < 0 || IntTools.IMAX < exponent) throw new ArgumentOutOfRangeException();

			FatFloat a = FatValue.GetFatFloat(operand, _radix);
			FatFloat ans = FatFloat.Power(a, exponent);
			string ret = FatValue.GetString(ans, _bracketMin);
			return ret;
		}

		public string Root(string operand, int exponent)
		{
			if (operand == null) throw new ArgumentNullException();
			if (exponent < 1 || IntTools.IMAX < exponent) throw new ArgumentOutOfRangeException();

			FatFloat a = FatValue.GetFatFloat(operand, _radix);
			FatFloat ans = FatFloat.Root(a, exponent, _basement);
			string ret = FatValue.GetString(ans, _bracketMin);
			return ret;
		}

		public string ChangeRadix(string operand, UInt64 radixNew)
		{
			if (operand == null) throw new ArgumentNullException();
			if (radixNew < 2) throw new ArgumentOutOfRangeException();

			FatFloat a = FatValue.GetFatFloat(operand, _radix);
			FatFloat ans = a.ChangeRadix(radixNew, _basement);
			string ret = FatValue.GetString(ans, _bracketMin);
			return ret;
		}

		public string Normalize(string operand)
		{
			if (operand == null) throw new ArgumentNullException();

			FatValue mid = new FatValue();
			mid.SetString(operand, _radix);
			string ret = mid.GetString(_bracketMin);
			return ret;
		}
	}
}
