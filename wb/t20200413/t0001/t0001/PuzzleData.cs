using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	public class PuzzleData
	{
		public Canvas Canvas;
		public int XNum;
		public int YNum;
		public int Piace_W;
		public int Piace_H;

		public PuzzleData(string imgFile, int xNum, int yNum)
		{
			this.Canvas = new Canvas(imgFile);

			if (
				xNum < 1 ||
				IntTools.IMAX < xNum ||
				this.Canvas.GetWidth() % xNum != 0
				)
				throw new Exception("Bad xNum: " + xNum);

			if (
				yNum < 1 ||
				IntTools.IMAX < yNum ||
				this.Canvas.GetHeight() % yNum != 0
				)
				throw new Exception("Bad yNum: " + yNum);

			this.XNum = xNum;
			this.YNum = yNum;
			this.Piace_W = this.Canvas.GetWidth() / this.XNum;
			this.Piace_H = this.Canvas.GetHeight() / this.YNum;

			this.MakePiaces();
		}

		public class PiaceSidePairInfo
		{
			public string Ident; // GetIdent()
			public double Difference;

			public static string GetIdent(PiaceSideInfo a, PiaceSideInfo b)
			{
				return Math.Min(a.Ident, b.Ident) + "_" + Math.Max(a.Ident, b.Ident);
			}
		}

		public class PiaceSideInfo
		{
			private static int IdentCount = 0;

			public int Ident;
			public double[] R;
			public double[] G;
			public double[] B;

			public PiaceSideInfo(IEnumerable<Color> colorEnum)
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

		public class PiaceInfo
		{
			public Canvas Canvas;
			public PiaceSideInfo Side_2;
			public PiaceSideInfo Side_4;
			public PiaceSideInfo Side_6;
			public PiaceSideInfo Side_8;
		}

		public List<PiaceInfo> Piaces = new List<PiaceInfo>();

		private void MakePiaces()
		{
			for (int x = 0; x < this.XNum; x++)
			{
				for (int y = 0; y < this.YNum; y++)
				{
					this.Piaces.Add(new PiaceInfo()
					{
						Canvas = this.Canvas.Copy(x * this.Piace_W, y * this.Piace_H, this.Piace_W, this.Piace_H),
					});
				}
			}
			foreach (PiaceInfo piace in this.Piaces)
			{
				piace.Side_2 = new PiaceSideInfo(Enumerable.Range(0, this.Piace_W).Select(v => piace.Canvas.Get(v, this.Piace_H - 1)));
				piace.Side_4 = new PiaceSideInfo(Enumerable.Range(0, this.Piace_H).Select(v => piace.Canvas.Get(0, v)));
				piace.Side_6 = new PiaceSideInfo(Enumerable.Range(0, this.Piace_H).Select(v => piace.Canvas.Get(this.Piace_W - 1, v)));
				piace.Side_8 = new PiaceSideInfo(Enumerable.Range(0, this.Piace_W).Select(v => piace.Canvas.Get(v, 0)));
			}
		}
	}
}
