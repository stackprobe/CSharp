using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Tools
{
	public struct I4Color
	{
		/// <summary>
		/// 赤 0 ～ 255
		/// </summary>
		public int R;

		/// <summary>
		/// 緑 0 ～ 255
		/// </summary>
		public int G;

		/// <summary>
		/// 青 0 ～ 255
		/// </summary>
		public int B;

		/// <summary>
		/// アルファ値 0 ～ 255
		/// </summary>
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

		public Color ToColor()
		{
			return Color.FromArgb(this.A, this.R, this.G, this.B);
		}

		public static I4Color FromColor(Color color)
		{
			return new I4Color(color.R, color.G, color.B, color.A);
		}
	}
}
