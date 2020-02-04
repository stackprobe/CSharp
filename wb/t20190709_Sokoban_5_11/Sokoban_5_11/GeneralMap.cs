using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class GeneralMap
	{
		public GeneralCell[][] Table;

		public GeneralMap(int w, int h)
		{
			this.Table = new GeneralCell[w][];

			for (int x = 0; x < w; x++)
			{
				this.Table[x] = new GeneralCell[h];

				for (int y = 0; y < h; y++)
				{
					this.Table[x][y] = new GeneralCell();
				}
			}
		}

		public int W
		{
			get
			{
				return this.Table.Length;
			}
		}

		public int H
		{
			get
			{
				return this.Table[0].Length;
			}
		}

		public void Initialize(Moment curr)
		{
			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					if (curr.IsWall(x, y) == false && curr.Map.Table[x][y].Point == false)
					{
						if (
							(curr.IsWall(x - 1, y) && curr.IsWall(x, y - 1)) ||
							(curr.IsWall(x + 1, y) && curr.IsWall(x, y - 1)) ||
							(curr.IsWall(x - 1, y) && curr.IsWall(x, y + 1)) ||
							(curr.IsWall(x + 1, y) && curr.IsWall(x, y + 1))
							)
							this.Table[x][y].DeadZone = true;
					}
				}
			}
			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					for (int x2 = x + 1; x2 < this.W; x2++)
					{
						this.CheckDeadLine(curr, x, y, x2, y);
					}
					for (int y2 = y + 1; y2 < this.H; y2++)
					{
						this.CheckDeadLine(curr, x, y, x, y2);
					}
				}
			}

			// もっと詳細に、、
		}

		private void CheckDeadLine(Moment curr, int x, int y, int x2, int y2)
		{
			if (x == x2)
			{
				if (
					curr.IsWall(x, y - 1) &&
					curr.IsWall(x, y2 + 1) &&
					IntTools.Sequence(y, y2 - y + 1).Any(yy => curr.IsWall(x, yy)) == false &&
					(
						IntTools.Sequence(y, y2 - y + 1).Any(yy => curr.IsWall(x - 1, yy) == false) == false ||
						IntTools.Sequence(y, y2 - y + 1).Any(yy => curr.IsWall(x + 1, yy) == false) == false
					)
					)
					this.DeadLines.Add(new DeadLine()
					{
						X = x,
						Y = y,
						X2 = x,
						Y2 = y2,
						BoxMax = IntTools.Sequence(y, y2 - y + 1).Where(yy => curr.Map.Table[x][yy].Point).Count(),
					});
			}
			else // y == y2
			{
				if (
					curr.IsWall(x - 1, y) &&
					curr.IsWall(x2 + 1, y) &&
					IntTools.Sequence(x, x2 - x + 1).Any(xx => curr.IsWall(xx, y)) == false &&
					(
						IntTools.Sequence(x, x2 - x + 1).Any(xx => curr.IsWall(xx, y - 1) == false) == false ||
						IntTools.Sequence(x, x2 - x + 1).Any(xx => curr.IsWall(xx, y + 1) == false) == false
					)
					)
					this.DeadLines.Add(new DeadLine()
					{
						X = x,
						Y = y,
						X2 = x2,
						Y2 = y,
						BoxMax = IntTools.Sequence(x, x2 - x + 1).Where(xx => curr.Map.Table[xx][y].Point).Count(),
					});
			}
		}

		public class DeadLine
		{
			public int X;
			public int Y;
			public int X2;
			public int Y2;
			public int BoxMax;
		}

		public List<DeadLine> DeadLines = new List<DeadLine>();
	}
}
