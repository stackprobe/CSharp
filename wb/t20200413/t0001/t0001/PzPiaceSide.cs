using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class PzPiaceSide
	{
		public static int IdentCount = 0;

		public int Ident;
		public double[] R;
		public double[] G;
		public double[] B;

		public PzPiaceSide(IEnumerable<Color> colorEnum)
		{
			this.Ident = IdentCount++;

			Color[] colors = colorEnum.ToArray();

			this.R = new double[colors.Length];
			this.G = new double[colors.Length];
			this.B = new double[colors.Length];

			for (int index = 0; index < colors.Length; index++)
			{
				this.R[index] = colors[index].R / 255.0;
				this.G[index] = colors[index].G / 255.0;
				this.B[index] = colors[index].B / 255.0;
			}
			Bokashi(ref this.R);
			Bokashi(ref this.G);
			Bokashi(ref this.B);
		}

		private static void Bokashi(ref double[] p)
		{
			int pLen = p.Length;
			double[] dest = new double[pLen];

			dest[0] = (p[0] + p[1]) / 2.0;

			for (int index = 1; index + 1 < pLen; index++)
				dest[index] = (p[index - 1] + p[index] + p[index + 1]) / 3.0;

			dest[pLen - 1] = (p[pLen - 2] + p[pLen - 1]) / 2.0;
			p = dest;
		}
	}
}
