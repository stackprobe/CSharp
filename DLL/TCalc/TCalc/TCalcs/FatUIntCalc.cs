using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatUIntCalc
	{
		public int Radix;

		public FatUIntCalc(int radix)
		{
			this.Radix = radix;
		}

		public void Add(FatUInt a, FatUInt b)
		{
			a.Resize(Math.Max(a.Figures.Length, b.Figures.Length) + 1);

			int carry = 0;

			for (int index = 0; index < b.Figures.Length || carry == 1; index++)
			{
				int value = a.Figures[index] + b.Get(index) + carry;
				carry = value / this.Radix;
				a.Figures[index] = value % Radix;
			}
		}

		public int Sub(FatUInt a, FatUInt b)
		{
			a.Resize(Math.Max(a.Figures.Length, b.Figures.Length) + 1);

			int carry = 0;

			for (int index = 0; (index < b.Figures.Length || carry == -1) && index < a.Figures.Length; index++)
			{
				int value = a.Figures[index] - b.Get(index) + carry + Radix;
				carry = value / this.Radix - 1;
				a.Figures[index] = value % Radix;
			}
			if (carry == -1)
			{
				a.Inverse(this.Radix - 1);
				a.Add(0, 1L, this.Radix);
			}
			return carry * 2 + 1;
		}

		public FatUInt Mul(FatUInt a, FatUInt b)
		{
			FatUInt answer = new FatUInt();

			answer.Resize(a.Figures.Length + b.Figures.Length);

			for (int index = 0; index < a.Figures.Length; index++)
			{
				for (int ndx = 0; ndx < b.Figures.Length; ndx++)
				{
					answer.Add(index + ndx, (long)a.Figures[index] * b.Figures[ndx], this.Radix);
				}
			}
			return answer;
		}

		public FatUInt Div(FatUInt a, FatUInt b)
		{
			return new FatUIntCalcDiv(this).Div(new FatUIntDiv(a), new FatUIntDiv(b));
		}
	}
}
