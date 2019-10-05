using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatUIntCalcDiv
	{
		public FatUIntCalc Owner;

		public FatUIntCalcDiv(FatUIntCalc owner)
		{
			this.Owner = owner;
		}

		public FatUInt Div(FatUIntDiv a, FatUIntDiv b)
		{
			if (b.End == 0)
			{
				throw new Exception("Zero divide");
			}

			FatUInt answer = new FatUInt(new int[a.End]);

			{
				int numer = this.Owner.Radix;
				int denom = b.Figures[b.End - 1] + 1;
				int d = numer / denom;

				FatUInt dd = new FatUInt(new int[] { d });

				a = new FatUIntDiv(this.Owner.Mul(a.GetUInt(), dd));
				b = new FatUIntDiv(this.Owner.Mul(b.GetUInt(), dd));
			}

			while (b.Figures[b.Start] == 0)
			{
				b.Start++;
			}

			for (; ; )
			{
				if (a.End < b.End)
				{
					break;
				}

				if (a.End == b.End)
				{
					int numer = a.Figures[a.End - 1];
					int denom = b.Figures[b.End - 1] + 1;
					int d = numer / denom;

					if (d == 0)
						break;

					answer.Add(0, (long)d, this.Owner.Radix);

					this.Sub(a, 0, this.Mul(b, d));
				}
				else
				{
					long numer = a.Figures[a.End - 2] + (long)this.Owner.Radix * a.Figures[a.End - 1];
					long denom = b.Figures[b.End - 1] + 1L;
					long d = numer / denom;
					int di = a.End - b.End - 1;

					answer.Add(di, (long)d, this.Owner.Radix);

					this.Sub(a, di, this.Mul(b, (int)(d % this.Owner.Radix), (int)(d / this.Owner.Radix)));
				}
				a.Trim();
			}
			if (a.End != 0)
			{
				FatUInt ta = a.GetUInt();

				if (this.Owner.Sub(ta, b.GetUInt()) == 1)
				{
					answer.Add(0, 1L, this.Owner.Radix);
					a = new FatUIntDiv(ta);
					answer.Remained = a.End != 0;
				}
				else
					answer.Remained = true;
			}
			return answer;
		}

		private void Sub(FatUIntDiv a, int offset, FatUIntDiv b)
		{
			int carry = 0;

			for (int index = 0; index < b.End || carry == -1; index++)
			{
				int value = a.Figures[offset + index] - b.Get(index) + carry + this.Owner.Radix;
				carry = value / this.Owner.Radix - 1;
				a.Figures[offset + index] = value % this.Owner.Radix;
			}
		}

		private FatUIntDiv Mul(FatUIntDiv a, int b)
		{
			FatUInt answer = new FatUInt(new int[a.End + 1]);

			for (int index = a.Start; index < a.End; index++)
			{
				answer.Add(index, (long)a.Figures[index] * b, this.Owner.Radix);
			}
			return new FatUIntDiv(answer);
		}

		private FatUIntDiv Mul(FatUIntDiv a, int b, int b2)
		{
			FatUInt answer = new FatUInt(new int[a.End + 2]);

			for (int index = a.Start; index < a.End; index++)
			{
				answer.Add(index + 0, (long)a.Figures[index] * b, this.Owner.Radix);
				answer.Add(index + 1, (long)a.Figures[index] * b2, this.Owner.Radix);
			}
			return new FatUIntDiv(answer);
		}
	}
}
