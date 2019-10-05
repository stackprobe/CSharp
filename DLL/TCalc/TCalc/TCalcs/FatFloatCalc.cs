using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatFloatCalc
	{
		public int Radix;

		public FatFloatCalc(int radix)
		{
			this.Radix = radix;
		}

		public void Add(FatFloat a, FatFloat b)
		{
			a.Normalize();
			b.Normalize();

			if (a.Sign == b.Sign)
			{
				new FatUFloatCalc(this.Radix).Add(a.Inner, b.Inner);
			}
			else
			{
				a.Sign *= new FatUFloatCalc(this.Radix).Sub(a.Inner, b.Inner);
			}
		}

		public void Sub(FatFloat a, FatFloat b)
		{
			a.Normalize();
			b.Normalize();

			b.Sign *= -1;

			Add(a, b);
		}

		public FatFloat Mul(FatFloat a, FatFloat b)
		{
			a.Normalize();
			b.Normalize();

			return new FatFloat(new FatUFloatCalc(this.Radix).Mul(a.Inner, b.Inner), a.Sign * b.Sign);
		}

		public FatFloat Div(FatFloat a, FatFloat b)
		{
			a.Normalize();
			b.Normalize();

			return new FatFloat(new FatUFloatCalc(this.Radix).Div(a.Inner, b.Inner), a.Sign * b.Sign);
		}
	}
}
