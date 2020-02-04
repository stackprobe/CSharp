using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Moment
	{
		public Moment Prev;
		public Map Map;
		public int X;
		public int Y;

		// <---- prm

		public bool IsCompleted()
		{
			return this.Map.IsCompleted();
		}

		public IEnumerable<Moment> Next()
		{
			foreach (Cell cell in this.Map.GetAllCell())
				cell.Checked = false;

			Queue<int[]> nextXYs = new Queue<int[]>();

			nextXYs.Enqueue(new int[]
			{
				this.X,
				this.Y,
			});

			while (1 <= nextXYs.Count)
			{
				int[] xy = nextXYs.Dequeue();
				int x = xy[0];
				int y = xy[1];
				Cell cell = this.Map.Table[x][y];

				cell.Checked = true;

				foreach (int[] sxy in new int[][]
				{
					new int[] { -1,  0 },
					new int[] {  1,  0 },
					new int[] {  0, -1 },
					new int[] {  0,  1 },
				})
				{
					int sx = sxy[0];
					int sy = sxy[1];

					if (this.Map.IsBox(x + sx, y + sy) && this.Map.IsFill(x + sx * 2, y + sy * 2) == false)
					{
						Map m = this.Map.GetClone();

						{
							int xx = x + sx;
							int yy = y + sy;

							m.Table[xx][yy].State = m.Table[xx][yy].Point ? Cell.State_e.POINT : Cell.State_e.EMPTY;
						}

						{
							int xx = x + sx * 2;
							int yy = y + sy * 2;

							m.Table[xx][yy].State = m.Table[xx][yy].Point ? Cell.State_e.POINT_BOX : Cell.State_e.BOX;
						}

						yield return new Moment()
						{
							Prev = this,
							Map = m,
							X = x + sx,
							Y = y + sy,
						};
					}
					else if (this.Map.IsFill(x + sx, y + sy) == false && this.Map.Table[x + sx][y + sy].Checked == false)
					{
						nextXYs.Enqueue(new int[]
					{
						x + sx,
						y + sy,
					});
					}
				}
			}
		}

		public bool IsSame(Moment other)
		{
			for (int x = 0; x < this.Map.W; x++)
				for (int y = 0; y < this.Map.H; y++)
					if (this.Map.Table[x][y].State != other.Map.Table[x][y].State)
						return false;

			return this.IsMovable(this.X, this.Y, other.X, other.Y);
		}

		private bool IsMovable(int startX, int startY, int goalX, int goalY)
		{
			foreach (Cell cell in this.Map.GetAllCell())
				cell.Reached = false;

			Queue<int[]> nextXYs = new Queue<int[]>();

			nextXYs.Enqueue(new int[]
			{
				startX,
				startY,
			});

			while (1 <= nextXYs.Count)
			{
				int[] xy = nextXYs.Dequeue();
				int x = xy[0];
				int y = xy[1];

				if (x == goalX && y == goalY)
					return true;

				Cell cell = this.Map.Table[x][y];

				cell.Reached = true;

				foreach (int[] sxy in new int[][]
				{
					new int[] { -1,  0 },
					new int[] {  1,  0 },
					new int[] {  0, -1 },
					new int[] {  0,  1 },
				})
				{
					int sx = sxy[0];
					int sy = sxy[1];

					int xx = x + sx;
					int yy = y + sy;

					if (this.Map.IsFill(xx, yy) == false && this.Map.Table[xx][yy].Reached == false)
					{
						nextXYs.Enqueue(new int[]
							{
								xx,
								yy,
							});
					}
				}
			}
			return false;
		}

		public Moment[] GetRoute()
		{
			List<Moment> dest = new List<Moment>();

			this.CollectRoute(dest);
			dest.Reverse();
			return dest.ToArray();
		}

		private void CollectRoute(List<Moment> dest)
		{
			dest.Add(this);

			if (this.Prev != null)
				this.Prev.CollectRoute(dest);
		}

		public void DebugPrint()
		{
			Console.WriteLine("====");

			for (int y = 0; y < this.Map.H; y++)
			{
				for (int x = 0; x < this.Map.W; x++)
				{
					bool katasukeIsHere = x == this.X && y == this.Y;
					string ptn;

					switch (this.Map.Table[x][y].State)
					{
						case Cell.State_e.EMPTY: ptn = katasukeIsHere ? "○" : "　"; break;
						case Cell.State_e.WALL: ptn = "壁"; break;
						case Cell.State_e.BOX: ptn = "□"; break;
						case Cell.State_e.POINT: ptn = katasukeIsHere ? "●" : "・"; break;
						case Cell.State_e.POINT_BOX: ptn = "■"; break;

						default:
							throw null;
					}
					Console.Write(ptn);
				}
				Console.WriteLine("");
			}
			Console.WriteLine("====");
		}
	}
}
