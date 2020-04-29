using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class PzPiaceSidePair
	{
		public double Difference;

		public PzPiaceSidePair(PzPiaceSide a, PzPiaceSide b)
		{
			this.Difference =
				GetDifference(a.R, b.R) +
				GetDifference(a.G, b.G) +
				GetDifference(a.B, b.B);
		}

		private static double GetDifference(double[] a, double[] b)
		{
			if (a.Length != b.Length)
				throw null; // bugged !!!

			double ret = 0.0;

			for (int index = 0; index < a.Length; index++)
			{
				double v = a[index] - b[index];

				v *= v;
				v *= v;
				v *= v;

				ret += v;
			}
			return ret;
		}

		public static string GetIdent(PzPiaceSide a, PzPiaceSide b)
		{
			return Math.Min(a.Ident, b.Ident) + "_" + Math.Max(a.Ident, b.Ident);
		}
	}
}
