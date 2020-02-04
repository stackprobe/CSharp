using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Question
	{
		private Map InitialMap;
		private int InitialX;
		private int InitialY;

		public Question(string[] lines)
		{
			Map m = new Map(lines[0].Length, lines.Length);

			for (int x = 0; x < m.W; x++)
			{
				for (int y = 0; y < m.H; y++)
				{
					char chr = lines[y][x];
					Cell.State_e state;

					switch (chr)
					{
						case 'S': state = Cell.State_e.EMPTY;
							this.InitialX = x;
							this.InitialY = y;
							break;

						case ' ': state = Cell.State_e.EMPTY; break;
						case '#': state = Cell.State_e.WALL; break;
						case '1': state = Cell.State_e.BOX; break;
						case '2': state = Cell.State_e.POINT; break;
						case '3': state = Cell.State_e.POINT_BOX; break;

						default:
							throw null;
					}
					m.Table[x][y].State = state;
				}
			}
			this.InitialMap = m;
		}

		private List<Moment> Tree = new List<Moment>();

		public void Solve()
		{
			this.Tree.Add(new Moment()
			{
				Prev = null,
				Map = this.InitialMap,
				X = this.InitialX,
				Y = this.InitialY,
			});

			for (int index = 0; index < this.Tree.Count; index++)
			{
				Moment curr = this.Tree[index];

				if (index % 1000 == 0) // debug print
				{
					Console.WriteLine("" + index);
					curr.DebugPrint();
				}

				if (curr.IsCompleted())
				{
					this.Solved(curr);
					break;
				}
				this.Tree.AddRange(curr.Next().Where(v => this.Tree.Any(w => w.IsSame(v)) == false && v.Map.HasDeadBox() == false));
			}
		}

		private void Solved(Moment goal)
		{
			Moment[] route = goal.GetRoute();

			// debug print
			{
				Console.WriteLine("Answer...");

				foreach (Moment moment in route)
				{
					moment.DebugPrint();
				}
			}

			ShowAnswer.Perform(route);
		}
	}
}
