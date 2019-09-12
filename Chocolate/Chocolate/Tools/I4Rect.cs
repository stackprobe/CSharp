using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public struct I4Rect
	{
		public int L;
		public int T;
		public int W;
		public int H;

		public I4Rect(int l, int t, int w, int h)
		{
			this.L = l;
			this.T = t;
			this.W = w;
			this.H = h;
		}

		public static I4Rect LTRB(int l, int t, int r, int b)
		{
			return new I4Rect(l, t, r - l, b - t);
		}

		public int R
		{
			get
			{
				return this.L + this.W;
			}
		}

		public int B
		{
			get
			{
				return this.T + this.H;
			}
		}
	}
}
