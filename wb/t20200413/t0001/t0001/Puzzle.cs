using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public static class Puzzle
	{
		public static Canvas Canvas;
		public static int XNum;
		public static int YNum;
		public static int Piace_W;
		public static int Piace_H;

		public static void Load(string imgFile, int xNum, int yNum)
		{
			Canvas = new Canvas(imgFile);

			if (
				xNum < 1 ||
				IntTools.IMAX < xNum ||
				Canvas.GetWidth() % xNum != 0
				)
				throw new Exception("Bad xNum: " + xNum);

			if (
				yNum < 1 ||
				IntTools.IMAX < yNum ||
				Canvas.GetHeight() % yNum != 0
				)
				throw new Exception("Bad yNum: " + yNum);

			XNum = xNum;
			YNum = yNum;
			Piace_W = Canvas.GetWidth() / XNum;
			Piace_H = Canvas.GetHeight() / YNum;
		}

		public static List<PuzzlePiace> Piaces = new List<PuzzlePiace>();

		private static void MakePiaces()
		{
			for (int x = 0; x < Puzzle.XNum; x++)
			{
				for (int y = 0; y < Puzzle.YNum; y++)
				{
					Piaces.Add(new PuzzlePiace()
					{
						Canvas = Canvas.Copy(x * Piace_W, y * Piace_H, Piace_W, Piace_H),
					});
				}
			}

			PuzzlePiaceSide.IdentCount = 0;

			foreach (PuzzlePiace piace in Piaces)
			{
				piace.Side_2 = new PuzzlePiaceSide(Enumerable.Range(0, Piace_W).Select(v => piace.Canvas.Get(v, Piace_H - 1)));
				piace.Side_4 = new PuzzlePiaceSide(Enumerable.Range(0, Piace_H).Select(v => piace.Canvas.Get(0, v)));
				piace.Side_6 = new PuzzlePiaceSide(Enumerable.Range(0, Piace_H).Select(v => piace.Canvas.Get(Piace_W - 1, v)));
				piace.Side_8 = new PuzzlePiaceSide(Enumerable.Range(0, Piace_W).Select(v => piace.Canvas.Get(v, 0)));
			}
		}
	}
}
