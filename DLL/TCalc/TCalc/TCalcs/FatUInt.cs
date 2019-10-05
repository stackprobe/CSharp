using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatUInt
	{
		public int[] Figures;
		public bool Remained = false;

		public FatUInt()
			: this(new int[0])
		{ }

		public FatUInt(int[] figures)
		{
			this.Figures = figures;
		}

		public int Get(int index)
		{
			return index < this.Figures.Length ? this.Figures[index] : 0;
		}

		public void Resize(int size)
		{
			this.Figures = TCalcArrays.CopyOf(this.Figures, size);
		}

		public void Inverse(int fill)
		{
			for (int index = 0; index < this.Figures.Length; index++)
			{
				this.Figures[index] = fill - this.Figures[index];
			}
		}

		public void Add(int index, long value, int radix)
		{
			while (0 < value)
			{
				value += this.Figures[index];
				this.Figures[index] = (int)(value % radix);
				value /= radix;
				index++;
			}
		}
	}
}
