using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public struct D2Size
	{
		public double W;
		public double H;

		public D2Size(double w, double h)
		{
			this.W = w;
			this.H = h;
		}

		public static D2Size operator *(D2Size a, double b)
		{
			return new D2Size(a.W * b, a.H * b);
		}

		public static D2Size operator /(D2Size a, double b)
		{
			return new D2Size(a.W / b, a.H / b);
		}
	}
}
