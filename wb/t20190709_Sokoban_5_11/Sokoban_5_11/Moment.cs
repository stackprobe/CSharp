using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Moment
	{
		//public Moment Prev; // old
		public Map Map;
		public int X;
		public int Y;

		public void UpdateReachable()
		{
			this.Map.UpdateReachable(this.X, this.Y);
		}

		public IEnumerable<Moment> GetAllNext()
		{
			for (int x = 0; x < this.Map.W; x++)
			{
				for (int y = 0; y < this.Map.H; y++)
				{
					if (this.Map.Table[x][y].Reachable)
					{
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

							int x2 = x + ax * 2;
							int y2 = y + ay * 2;

							if (
								this.Map.IsOutOfRange(xx, yy) ||
								this.Map.Table[xx][yy].Box == false
								)
								continue;

							if (
								this.Map.IsOutOfRange(x2, y2) ||
								this.Map.Table[x2][y2].Wall ||
								this.Map.Table[x2][y2].Box
								)
								continue;

							Map m = this.Map.GetClone();

							m.Table[xx][yy].Box = false;
							m.Table[x2][y2].Box = true;

							yield return new Moment()
							{
								//Prev = this, // old
								Map = m,
								X = xx,
								Y = yy,
							};
						}
					}
				}
			}
		}

		public void DebugPrint()
		{
			Console.WriteLine("====");

			for (int y = 0; y < this.Map.H; y++)
			{
				for (int x = 0; x < this.Map.W; x++)
				{
					Console.Write(this.Map.Table[x][y].GetString());
				}
				Console.WriteLine("");
			}
			Console.WriteLine("====");
		}

		public bool IsWall(int x, int y)
		{
			return this.Map.IsOutOfRange(x, y) || this.Map.Table[x][y].Wall;
		}
	}
}
