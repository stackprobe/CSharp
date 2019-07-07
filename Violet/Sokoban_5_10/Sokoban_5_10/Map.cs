using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Map
	{
		public Cell[][] Table;

		public Map(int w, int h)
		{
			this.Table = new Cell[w][];

			for (int x = 0; x < w; x++)
			{
				this.Table[x] = new Cell[h];

				for (int y = 0; y < h; y++)
				{
					this.Table[x][y] = new Cell();
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

		public IEnumerable<Cell> GetAllCell()
		{
			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					yield return this.Table[x][y];
				}
			}
		}

		public bool IsCompleted()
		{
			return this.GetAllCell().Any(v => v.State == Cell.State_e.POINT) == false;
		}

		public bool IsOutOfRange(int x, int y)
		{
			return
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y;
		}

		public bool IsBox(int x, int y)
		{
			return IsOutOfRange(x, y) == false && this.Table[x][y].Box;
		}

		public bool IsFill(int x, int y)
		{
			return IsOutOfRange(x, y) || this.Table[x][y].Fill;
		}

		public bool IsWall(int x, int y)
		{
			return IsOutOfRange(x, y) || this.Table[x][y].State == Cell.State_e.WALL;
		}

		public Map GetClone()
		{
			Map m = new Map(this.W, this.H);

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					m.Table[x][y].State = this.Table[x][y].State;
				}
			}
			return m;
		}

		public bool HasDeadBox()
		{
			for (int x = 0; x < this.W; x++)
				for (int y = 0; y < this.H; y++)
					if (this.Table[x][y].State == Cell.State_e.BOX)
						if (this.IsDeadBox(x, y))
							return true;

			return false;
		}

		private bool IsDeadBox(int x, int y)
		{
			if (this.IsWall(x - 1, y) && this.IsWall(x, y - 1))
				return true;

			if (this.IsWall(x + 1, y) && this.IsWall(x, y - 1))
				return true;

			if (this.IsWall(x - 1, y) && this.IsWall(x, y + 1))
				return true;

			if (this.IsWall(x + 1, y) && this.IsWall(x, y + 1))
				return true;

			return false;
		}
	}
}
