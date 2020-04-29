using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	public static class SolvePuzzle
	{
		public static void Perform()
		{
			for (int origPiaceIndex = 0; origPiaceIndex < Puzzle.Piaces.Count; origPiaceIndex++)
			{
				Perform_OrigPiaceIndex(origPiaceIndex);
			}
		}

		private static List<PzPiace> SrcPiaces;
		private static AutoTable<PzPiace> DestTable;

		private static void Perform_OrigPiaceIndex(int origPiaceIndex)
		{
			SrcPiaces = new List<PzPiace>();
			SrcPiaces.AddRange(Puzzle.Piaces);
			DestTable = new AutoTable<PzPiace>();
			DestTable.W = Puzzle.XNum * 2 + 1;
			DestTable.H = Puzzle.YNum * 2 + 1;

			DestTable[Puzzle.XNum, Puzzle.YNum] = ExtraTools.FastDesertElement(SrcPiaces, origPiaceIndex);

			while (1 <= SrcPiaces.Count)
			{
				JoinPiaceOne();
			}
			WriteDestTable(origPiaceIndex);
		}

		private static List<I2Point> DestPoints;
		private static int DPJoinNum;

		private static int BestSrcIndex;
		private static I2Point BestDestPoint;
		private static double BestDifference;

		private static void JoinPiaceOne()
		{
			DestPoints = new List<I2Point>();
			DPJoinNum = 0;

			for (int x = 1; x + 1 < DestTable.W; x++)
			{
				for (int y = 1; y + 1 < DestTable.H; y++)
				{
					if (DestTable[x, y] == null)
					{
						int joinNum = GetJoinNum(x, y);

						if (DPJoinNum < joinNum)
						{
							DestPoints.Clear();
							DPJoinNum = joinNum;
						}
						if (DPJoinNum == joinNum)
						{
							DestPoints.Add(new I2Point(x, y));
						}
					}
				}
			}
			BestSrcIndex = -1;
			BestDestPoint = new I2Point(-1, -1); // as null
			BestDifference = double.MaxValue;

			for (int index = 0; index < SrcPiaces.Count; index++)
			{
				foreach (I2Point pt in DestPoints)
				{
					double d = GetDifference(SrcPiaces[index], pt.X, pt.Y);

					if (d < BestDifference)
					{
						BestSrcIndex = index;
						BestDestPoint = pt;
						BestDifference = d;
					}
				}
			}
			if (BestSrcIndex == -1)
				throw null; // souteigai !!!

			DestTable[BestDestPoint.X, BestDestPoint.Y] = ExtraTools.FastDesertElement(SrcPiaces, BestSrcIndex);
		}

		private static int GetJoinNum(int x, int y)
		{
			return new PzPiace[]
			{
				DestTable[x - 1, y],
				DestTable[x + 1, y],
				DestTable[x, y - 1],
				DestTable[x, y + 1],
			}
			.Where(v => v != null).Count();
		}

		private static double GetDifference(PzPiace piace, int x, int y)
		{
			return
				GetDifference(piace, x + 1, y, (a, b) => PzPiaceSidePairCache.GetPair(a.Side_6, b.Side_4)) +
				GetDifference(piace, x - 1, y, (a, b) => PzPiaceSidePairCache.GetPair(a.Side_4, b.Side_6)) +
				GetDifference(piace, x, y + 1, (a, b) => PzPiaceSidePairCache.GetPair(a.Side_2, b.Side_8)) +
				GetDifference(piace, x, y - 1, (a, b) => PzPiaceSidePairCache.GetPair(a.Side_8, b.Side_2));
		}

		private static double GetDifference(PzPiace piace, int x, int y, Func<PzPiace, PzPiace, PzPiaceSidePair> getPair)
		{
			return DestTable[x, y] == null ? 0.0 : getPair(piace, DestTable[x, y]).Difference;
		}

		private static void WriteDestTable(int origPiaceIndex)
		{
			string wFile = string.Format(@"C:\temp\Pz_{0:D3}.png", origPiaceIndex);

			// ---- make canvas ----

			int x1 = IntTools.IMAX;
			int y1 = IntTools.IMAX;
			int x2 = -1;
			int y2 = -1;

			for (int x = 0; x < DestTable.W; x++)
			{
				for (int y = 0; y < DestTable.H; y++)
				{
					if (DestTable[x, y] != null)
					{
						x1 = Math.Min(x1, x);
						y1 = Math.Min(y1, y);
						x2 = Math.Max(x2, x);
						y2 = Math.Max(y2, y);
					}
				}
			}
			int w = x2 - x1 + 1;
			int h = y2 - y1 + 1;

			Canvas canvas = new Canvas(w * Puzzle.Piace_W, h * Puzzle.Piace_H);

			canvas.Fill(Color.Azure);

			for (int x = x1; x <= x2; x++)
			{
				for (int y = y1; y <= y2; y++)
				{
					if (DestTable[x, y] != null)
					{
						canvas.Paste(DestTable[x, y].Canvas, (x - x1) * Puzzle.Piace_W, (y - y1) * Puzzle.Piace_H);
					}
				}
			}

			// ----

			canvas.Save(wFile);
		}
	}
}
