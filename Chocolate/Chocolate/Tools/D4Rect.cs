using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class D4Rect
	{
		public double L;
		public double T;
		public double W;
		public double H;

		public D4Rect(double l, double t, double w, double h)
		{
			this.L = l;
			this.T = t;
			this.W = w;
			this.H = h;
		}

		public static D4Rect LTRB(double l, double t, double r, double b)
		{
			return new D4Rect(l, t, r - l, b - t);
		}

		public double R
		{
			get
			{
				return this.L + this.W;
			}
		}

		public double B
		{
			get
			{
				return this.T + this.H;
			}
		}
	}
}
