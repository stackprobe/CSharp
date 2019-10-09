using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatUFloatCalc
	{
		public int Radix;

		public FatUFloatCalc(int radix)
		{
			this.Radix = radix;
		}

		public void Add(FatUFloat a, FatUFloat b)
		{
			a.Normalize();
			b.Normalize();

			a.Sync(b);
			b.Sync(a);

			new FatUIntCalc(Radix).Add(a.Inner, b.Inner);

			a.Normalize();
		}

		public int Sub(FatUFloat a, FatUFloat b)
		{
			a.Normalize();
			b.Normalize();

			a.Sync(b);
			b.Sync(a);

			int sign = new FatUIntCalc(Radix).Sub(a.Inner, b.Inner);

			a.Normalize();

			return sign;
		}

		public FatUFloat Mul(FatUFloat a, FatUFloat b)
		{
			a.Normalize();
			b.Normalize();

			FatUFloat answer = new FatUFloat(new FatUIntCalc(Radix).Mul(a.Inner, b.Inner), a.Exponent + b.Exponent);

			answer.Normalize();

			return answer;
		}

		public FatUFloat Div(FatUFloat a, FatUFloat b)
		{
			a.Normalize();
			b.Normalize();

			a.Sync(b);
			b.Sync(a);

			FatUFloat answer = new FatUFloat(new FatUIntCalc(Radix).Div(a.Inner, b.Inner), 0);

			answer.Normalize();

			return answer;
		}
	}
}
