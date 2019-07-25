using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte.Command2
{
	public class Puzzle
	{
		public int XNum;
		public int YNum;
		public int Piece_W;
		public int Piece_H;

		// <---- prm

		private int[] LastXY = null;

		public Image Routine(Image image, int x, int y)
		{
			x /= this.Piece_W;
			x *= this.Piece_W;
			y /= this.Piece_H;
			y *= this.Piece_H;

			if (this.LastXY == null)
			{
				this.LastXY = new int[] { x, y };
			}
			else
			{
				int lastX = this.LastXY[0];
				int lastY = this.LastXY[1];

				this.LastXY = null;

				Canvas c = new Canvas(image);
				Canvas piece1 = c.Copy(lastX, lastY, this.Piece_W, this.Piece_H);
				Canvas piece2 = c.Copy(x, y, this.Piece_W, this.Piece_H);

				c.Paste(piece2, lastX, lastY);
				c.Paste(piece1, x, y);

				image = c.GetImage();
			}
			return image;
		}
	}
}
