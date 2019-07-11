using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

			// もっと詳細に、、
		}
	}
}
