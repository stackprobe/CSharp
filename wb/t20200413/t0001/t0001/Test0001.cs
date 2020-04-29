using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Test0001
	{
		private Canvas Canvas;
		private int XNum;
		private int YNum;
		private int Piace_W;
		private int Piace_H;

		public Test0001(string imgFile, int xNum, int yNum)
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
		}

		public void Perform()
		{
			throw null; // TODO
		}
	}
}
