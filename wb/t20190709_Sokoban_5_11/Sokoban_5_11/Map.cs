using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

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

		public bool IsOutOfRange(int x, int y)
		{
			return
				x < 0 || this.W <= x ||
				y < 0 || this.H <= y;
		}

		public void UpdateReachable(int startX, int startY)
		{
			foreach (Cell cell in this.GetAllCell())
			{
				cell.Reachable = false;
			}
			Queue<int> nextXYs = new Queue<int>();

			nextXYs.Enqueue(startX);
			nextXYs.Enqueue(startY);

			while (1 <= nextXYs.Count)
			{
				int x = nextXYs.Dequeue();
				int y = nextXYs.Dequeue();

				this.Table[x][y].Reachable = true;

				foreach (int[] axy in new int[][]
				{
					new int[] { -1,  0 },
					new int[] {  1,  0 },
					new int[] {  0, -1 },
					new int[] {  0,  1 },
				})
				{
					int ax = axy[0];
					int ay = axy[1];

					int xx = x + ax;
					int yy = y + ay;

					if (
						this.IsOutOfRange(xx, yy) == false &&
						this.Table[xx][yy].Reachable == false &&
						this.Table[xx][yy].Wall == false &&
						this.Table[xx][yy].Box == false
						)
					{
						nextXYs.Enqueue(xx);
						nextXYs.Enqueue(yy);
					}
				}
			}
		}

		public string Hash;

		public void UpdateHash()
		{
			this.Hash = BinTools.Hex.ToString(SecurityTools.GetMD5(Encoding.ASCII.GetBytes(string.Join(
				"",
				this.GetAllCell().Select(v => v.GetString())
				))));
		}

		public bool IsDead()
		{
			for (int x = 0; x < this.W; x++)
				for (int y = 0; y < this.H; y++)
					if (this.Table[x][y].Box && Question.GeneralMap.Table[x][y].DeadZone)
						return true;

			for (int x = 0; x < this.W - 1; x++)
			{
				for (int y = 0; y < this.H - 1; y++)
				{
					int b = 0;
					int w = 0;
					int p = 0;

					for (int ax = 0; ax < 2; ax++)
					{
						for (int ay = 0; ay < 2; ay++)
						{
							int xx = x + ax;
							int yy = y + ay;

							if (this.Table[xx][yy].Box)
								b++;

							if (this.Table[xx][yy].Wall)
								w++;

							if (this.Table[xx][yy].Point)
								p++;
						}
					}
					//if (4 < b + w) throw null; // test
					//if (b + w == 4 && b < p) throw null; // test

					if (1 <= b && b + w == 4 && b != p)
						return true;
				}
			}

			foreach (GeneralMap.DeadLine deadLine in Question.GeneralMap.DeadLines)
			{
				int b = 0;

				for (int x = deadLine.X; x <= deadLine.X2; x++)
					for (int y = deadLine.Y; y <= deadLine.Y2; y++)
						if (this.Table[x][y].Box)
							b++;

				if (deadLine.BoxMax < b)
					return true;
			}

			// TODO もっと厳密に、、

			return false;
		}

		public bool IsCompleted()
		{
			foreach (Cell cell in this.GetAllCell())
			{
				// TODO PointXYs

				if (cell.Point && cell.Box == false)
					return false;
			}
			return true;
		}

		public Map GetClone()
		{
			Map m = new Map(this.W, this.H);

			for (int x = 0; x < this.W; x++)
			{
				for (int y = 0; y < this.H; y++)
				{
					this.Table[x][y].CopyTo(m.Table[x][y]);
				}
			}
			return m;
		}
	}
}
