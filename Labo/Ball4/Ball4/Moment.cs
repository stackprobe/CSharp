using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Moment
	{
		public int[][] Table;
		public int LastTurn;

		public Moment()
		{
			// ---- init ----

			this.Table = new int[Consts.TABLE_W][];

			for (int x = 0; x < Consts.TABLE_W; x++)
			{
				this.Table[x] = new int[Consts.TABLE_H];

				for (int y = 0; y < Consts.TABLE_H; y++)
				{
					this.Table[x][y] = Consts.CELL_NONE;
				}
			}
			this.LastTurn = Consts.CELL_NONE;

			// ----
		}

		public string Hash;

		public void UpdateHash()
		{
			StringBuilder buff = new StringBuilder();

			for (int x = 0; x < Consts.TABLE_W; x++)
			{
				for (int y = 0; y < Consts.TABLE_H; y++)
				{
					buff.Append("" + this.Table[x][y]);
				}
			}
			this.Hash = buff.ToString();
		}

		public void CopyTableTo(int[][] dest)
		{
			for (int x = 0; x < Consts.TABLE_W; x++)
			{
				for (int y = 0; y < Consts.TABLE_H; y++)
				{
					dest[x][y] = this.Table[x][y];
				}
			}
		}

		public Moment Prev = null;
		public List<Moment> Nexts = new List<Moment>();

		public IEnumerable<Moment> GetAllNext()
		{
			int turn = 3 - this.LastTurn;

			for (int x = 0; x < Consts.TABLE_W; x++)
			{
				for (int y = Consts.TABLE_H - 1; 0 <= y; y--)
				{
					if (this.Table[x][y] == Consts.CELL_NONE)
					{
						Moment next = new Moment();

						this.CopyTableTo(next.Table);
						next.Table[x][y] = turn;
						next.LastTurn = turn;

						yield return next;

						break;
					}
				}
			}
		}

		public bool Win = false;
		public bool Lose = false;

		public void Check()
		{
			if (this.IsFinished())
			{
				if (this.LastTurn == Consts.CELL_PLAYER)
					this.Win = true;
				else // CELL_ENEMY
					this.Lose = true;

				//Console.WriteLine("Finished: " + this.Win);

				this.RecheckPrev();
			}
		}

		private void RecheckPrev()
		{
			if (this.Prev != null)
				this.Prev.Recheck();
		}

		private void Recheck()
		{
			if (this.Win || this.Lose) // ? 既に確定
				return;

			if (this.Nexts.Count == 0) // ? 引き分け
				return;

			if (this.LastTurn == Consts.CELL_PLAYER) // -> enemy turn
			{
				foreach (Moment next in this.Nexts)
				{
					if (next.Lose)
					{
						this.Lose = true;
						this.RecheckPrev();
						return;
					}
				}
				foreach (Moment next in this.Nexts)
				{
					if (next.Win == false)
						return;
				}
				this.Win = true;
				this.RecheckPrev();
			}
			else // CELL_ENEMY -> player turn
			{
				foreach (Moment next in this.Nexts)
				{
					if (next.Win)
					{
						this.Win = true;
						this.RecheckPrev();
						return;
					}
				}
				foreach (Moment next in this.Nexts)
				{
					if (next.Lose == false)
						return;
				}
				this.Lose = true;
				this.RecheckPrev();
			}
		}

		private bool IsFinished()
		{
			for (int x = 0; x < Consts.TABLE_W; x++)
			{
				for (int y = 0; y + 4 <= Consts.TABLE_H; y++)
				{
					if (
						this.Table[x][y + 0] == this.LastTurn &&
						this.Table[x][y + 1] == this.LastTurn &&
						this.Table[x][y + 2] == this.LastTurn &&
						this.Table[x][y + 3] == this.LastTurn
						)
						return true;
				}
			}
			for (int x = 0; x + 4 <= Consts.TABLE_W; x++)
			{
				for (int y = 0; y < Consts.TABLE_H; y++)
				{
					if (
						this.Table[x + 0][y] == this.LastTurn &&
						this.Table[x + 1][y] == this.LastTurn &&
						this.Table[x + 2][y] == this.LastTurn &&
						this.Table[x + 3][y] == this.LastTurn
						)
						return true;
				}
			}
			for (int x = 0; x + 4 <= Consts.TABLE_W; x++)
			{
				for (int y = 0; y + 4 <= Consts.TABLE_H; y++)
				{
					if (
						this.Table[x + 0][y + 0] == this.LastTurn &&
						this.Table[x + 1][y + 1] == this.LastTurn &&
						this.Table[x + 2][y + 2] == this.LastTurn &&
						this.Table[x + 3][y + 3] == this.LastTurn
						)
						return true;

					if (
						this.Table[x + 0][y + 3] == this.LastTurn &&
						this.Table[x + 1][y + 2] == this.LastTurn &&
						this.Table[x + 2][y + 1] == this.LastTurn &&
						this.Table[x + 3][y + 0] == this.LastTurn
						)
						return true;
				}
			}
			return false;
		}

		public void DebugPrint(string indent, int depth)
		{
			if (depth == 0)
				return;

			foreach (Moment next in this.Nexts)
			{
				Console.WriteLine(indent + next.Win + " " + next.Lose);

				next.DebugPrint(indent + "\t", depth - 1);
			}
		}

		public Moment[] GetRoute()
		{
			List<Moment> dest = new List<Moment>();

			dest.Add(this);

			while (dest[dest.Count - 1].Prev != null)
				dest.Add(dest[dest.Count - 1].Prev);

			dest.Reverse();
			return dest.ToArray();
		}
	}
}
