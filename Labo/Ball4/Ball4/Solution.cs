using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class Solution
	{
		public Moment Initial = new Moment();

		public Solution(string[] lines)
		{
			for (int x = 0; x < Consts.TABLE_W; x++)
			{
				for (int y = 0; y < Consts.TABLE_H; y++)
				{
					this.Initial.Table[x][y] = int.Parse(lines[y].Substring(x, 1));
				}
			}
			this.Initial.LastTurn = Consts.CELL_ENEMY;
		}

		public void Perform()
		{
			this.Solve();
			this.Solved();
		}

		private void Solve()
		{
			Dictionary<string, Moment> knowns = DictionaryTools.Create<Moment>();
			Queue<Moment> q = new Queue<Moment>();

			this.Initial.UpdateHash();
			knowns.Add(this.Initial.Hash, this.Initial);
			q.Enqueue(this.Initial);

			for (int count = 0; 1 <= q.Count; count++)
			{
				Moment curr = q.Dequeue();

				if (count % 10000 == 0)
				{
					Console.WriteLine(count + " " + curr.GetRoute().Length);
					this.Initial.DebugPrint("", 1);
				}
				curr.Check();

				if (curr.Win || curr.Lose)
					continue;

				foreach (Moment f_next in curr.GetAllNext())
				{
					Moment next = f_next;

					next.UpdateHash();

					if (knowns.ContainsKey(next.Hash))
					{
						next = knowns[next.Hash];
					}
					else
					{
						knowns.Add(next.Hash, next);
						q.Enqueue(next);
					}
					next.Prev = curr;
					curr.Nexts.Add(next);
				}
			}
		}

		private void Solved()
		{
			this.Initial.DebugPrint("", 3);
		}
	}
}
