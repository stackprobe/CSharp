using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Tools
{
	/// <summary>
	/// アルファ値を含む色を表す。
	/// 各色は 0 ～ 255 を想定する。
	/// </summary>
	public struct I4Color
	{
		public int R;
		public int G;
		public int B;
		public int A;

		public I4Color(int r, int g, int b, int a)
		{
			this.R = r;
			this.G = g;
			this.B = b;
			this.A = a;
		}

		public override string ToString()
		{
			return string.Format("{0:x2}{1:x2}{2:x2}{3:x2}", this.R, this.G, this.B, this.A);
		}

		public I3Color WithoutAlpha()
		{
			return new I3Color(this.R, this.G, this.B);
		}
	}
}
