using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatFloat
	{
		public FatUFloat Inner;
		public int Sign;

		public FatFloat(FatUFloat inner, int sign)
		{
			this.Inner = inner;
			this.Sign = sign;
		}

		public void Normalize()
		{
			this.Inner.normalize();

			if (this.Inner.Inner.Figures.Length == 0)
			{
				this.Sign = 1;
			}
		}
	}
}
