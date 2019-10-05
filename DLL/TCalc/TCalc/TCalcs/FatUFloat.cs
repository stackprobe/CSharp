using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatUFloat
	{
		public FatUInt Inner;
		public int Exponent;

		public FatUFloat(FatUInt inner, int exponent)
		{
			this.Inner = inner;
			this.Exponent = exponent;
		}

		public void normalize()
		{
			int start;

			for (start = 0; start < this.Inner.Figures.Length && this.Inner.Figures[start] == 0; start++)
			{ }

			if (start < this.Inner.Figures.Length)
			{
				int end;

				for (end = this.Inner.Figures.Length; this.Inner.Figures[end - 1] == 0; end--)
				{ }

				if (0 < start || end < this.Inner.Figures.Length)
				{
					this.Inner.Figures = TCalcArrays.CopyOfRange(this.Inner.Figures, start, end);
					this.Exponent += start;
				}
			}
			else
			{
				if (this.Inner.Figures.Length != 0)
					this.Inner.Figures = new int[0];

				this.Exponent = 0;
			}
		}

		public void sync(FatUFloat another)
		{
			int count = this.Exponent - another.Exponent;

			if (0 < count)
			{
				this.Inner.Figures = TCalcArrays.Shift(this.Inner.Figures, count);
				this.Exponent -= count;
			}
		}
	}
}
