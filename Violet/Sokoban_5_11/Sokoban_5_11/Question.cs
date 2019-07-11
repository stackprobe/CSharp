using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Question
	{
		private Moment Initial;

		public Question(string[] lines)
		{
			Moment m = new Moment()
			{
				Map = new Map(lines[0].Length, lines.Length),
			};

			for (int x = 0; x < m.Map.W; x++)
			{
				for (int y = 0; y < m.Map.H; y++)
				{
					switch (lines[y][x])
					{
						case 'S':
							m.X = x;
							m.Y = y;
							break;

						case ' ':
							break;

						case '#':
							m.Map.Table[x][y].Wall = true;
							break;

						case '1':
							m.Map.Table[x][y].Box = true;
							break;

						case '2':
							m.Map.Table[x][y].Point = true;
							break;

						case '3':
							m.Map.Table[x][y].Box = true;
							m.Map.Table[x][y].Point = true;
							break;

						default:
							throw null;
					}
				}
			}

			this.Initial = m;
		}

		public static GeneralMap GeneralMap;

		private Queue<Moment> Tree = new Queue<Moment>();
		private KnownHashes KnownHashes = new KnownHashes();

		public void Solve()
		{
			this.Initial.UpdateReachable();
			this.Initial.Map.UpdateHash();

			GeneralMap = new GeneralMap(this.Initial.Map.W, this.Initial.Map.H);
			GeneralMap.Initialize(this.Initial);

			this.Tree.Enqueue(this.Initial);
			this.KnownHashes.Add(this.Initial.Map.Hash, Consts.PREV_NONE);

			for (int count = 0; 1 <= this.Tree.Count; count++)
			{
				Moment curr = this.Tree.Dequeue();

				if (count % 1000 == 0) // debug print
				{
					Console.WriteLine("" + count);
					curr.DebugPrint();
				}
				if (curr.Map.IsDead())
					continue;

				if (curr.Map.IsCompleted())
				{
					Solved(curr);
					return;
				}
				foreach (Moment next in curr.GetAllNext())
				{
					next.UpdateReachable();
					next.Map.UpdateHash();

					if (this.KnownHashes.Add(next.Map.Hash, curr.Map.Hash))
						this.Tree.Enqueue(next);
				}
			}
			throw new Exception("この問題は解けません。");
		}

		private void Solved(Moment goal)
		{
			string[] hashRoute = this.KnownHashes.GetRoute(goal.Map.Hash);
			List<Moment> route = new List<Moment>();

			this.Initial.UpdateReachable(); // 2bs
			this.Initial.Map.UpdateHash(); // 2bs

			route.Add(this.Initial);

			for (int index = 1; index < hashRoute.Length; index++)
			{
				Moment curr = route[route.Count - 1];
				Moment next = curr.GetAllNext().First(v =>
				{
					v.UpdateReachable();
					v.Map.UpdateHash();

					return v.Map.Hash == hashRoute[index];
				});

				route.Add(next);
			}
			this.Solved(route.ToArray());
		}

		private void Solved(Moment[] route)
		{
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
