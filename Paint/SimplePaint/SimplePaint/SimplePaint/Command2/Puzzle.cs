using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;

namespace Charlotte.Command2
{
	public class Puzzle
	{
		private int XNum;
		private int YNum;
		private int Piece_W;
		private int Piece_H;

		private static string LastInputLine = "";

		public Puzzle(Image image)
		{
			string line = InputStringDlgTools.Show("Input xNum, yNum", "Input => xNum:yNum (e.g. 20:30)", true, LastInputLine);

			if (line == null)
				throw new Exception("キャンセル");

			LastInputLine = line;

			string[] tokens = line.Split(':');
			int xNum = int.Parse(tokens[0]);
			int yNum = int.Parse(tokens[1]);

			int pW = image.Width / xNum;
			int pH = image.Height / yNum;

			if (pW * xNum != image.Width)
				throw new Exception("xNum Error");

			if (pH * yNum != image.Height)
				throw new Exception("yNum Error");

			this.XNum = xNum;
			this.YNum = yNum;
			this.Piece_W = pW;
			this.Piece_H = pH;
		}

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
				BusyDlgTools.Show("Puzzle", "Puzzle 処理中...", () =>
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
				},
				true
				);
			}
			return image;
		}
	}
}
