using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.TCalcs
{
	public class FatUIntDiv
	{
		public int[] Figures;
		public int Start;
		public int End;

		public FatUIntDiv(FatUInt a)
			: this(a.Figures)
		{ }

		public FatUIntDiv(int[] figures)
		{
			this.Figures = figures;
			this.Start = 0;
			this.End = figures.Length;

			this.Trim();
		}

		public void Trim()
		{
			while (0 < this.End && this.Figures[this.End - 1] == 0)
			{
				this.End--;
			}
		}

		public int Get(int index)
		{
			return index < this.Figures.Length ? this.Figures[index] : 0;
		}

		public FatUInt GetUInt()
		{
			return new FatUInt(this.Figures.Length == this.End ? this.Figures : TCalcArrays.CopyOf(this.Figures, this.End));
		}
	}
}
