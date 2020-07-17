using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte.Tools
{
	public struct I3Color
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

		public I3Color(int r, int g, int b)
		{
			this.R = r;
			this.G = g;
			this.B = b;
		}

		public override string ToString()
		{
			return string.Format("{0:x2}{1:x2}{2:x2}", this.R, this.G, this.B);
		}

		public Color ToColor()
		{
			return Color.FromArgb(this.R, this.G, this.B);
		}

		public static I3Color FromColor(Color color)
		{
			return new I3Color(color.R, color.G, color.B);
		}
	}
}
