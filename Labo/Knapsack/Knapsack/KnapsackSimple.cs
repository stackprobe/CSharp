using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class KnapsackSimple : IKnapsack
	{
		private Condition Cond;

		public KnapsackSimple(Condition cond)
		{
			this.Cond = cond;
		}

		private int BestTotalValue;
		private int TotalValue;
		private int TotalWeight;

		public int GetBestValue()
		{
			this.BestTotalValue = 0;
			this.TotalValue = 0;
			this.TotalWeight = 0;

			bool[] chooseds = new bool[this.Cond.Items.Length];
			bool ahead = true;
			int index = 0;

			for (; ; )
			{
				if (ahead)
				{
					if (this.Check() == false)
					{
						ahead = false;
					}
					else if (index < this.Cond.Items.Length)
					{
						//chooseds[index] = false;
					}
					else
					{
						this.End();
						ahead = false;
					}
				}
				else
				{
					if (chooseds[index] == false)
					{
						TotalValue += this.Cond.Items[index].Value;
						TotalWeight += this.Cond.Items[index].Weight;

						chooseds[index] = true;
						ahead = true;
					}
					else
					{
						TotalValue -= this.Cond.Items[index].Value;
						TotalWeight -= this.Cond.Items[index].Weight;

						chooseds[index] = false;
					}
				}

				if (ahead)
				{
					index++;
				}
				else
				{
					if (index <= 0)
						break;

					index--;
				}
			}
			return this.BestTotalValue;
		}

		private bool Check()
		{
			return this.TotalWeight <= this.Cond.Capacity;
		}

		private void End()
		{
			this.BestTotalValue = Math.Max(this.BestTotalValue, this.TotalValue);
		}
	}
}
