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

		private int MaxTotalValue;
		private int TotalValue;
		private int TotalWeight;

		public int GetBestValue()
		{
			this.MaxTotalValue = 0;
			this.TotalValue = 0;
			this.TotalWeight = 0;

			bool[] chooseds = new bool[this.Cond.Items.Length];
			bool forward = true;
			int index = 0;

			for (; ; )
			{
				if (forward)
				{
					if (this.Check() == false)
					{
						forward = false;
					}
					else if (this.Cond.Items.Length <= index)
					{
						forward = false;
						this.End();
					}
				}
				else
				{
					if (chooseds[index] == false)
					{
						TotalValue += this.Cond.Items[index].Value;
						TotalWeight += this.Cond.Items[index].Weight;

						chooseds[index] = true;
						forward = true;
					}
					else
					{
						TotalValue -= this.Cond.Items[index].Value;
						TotalWeight -= this.Cond.Items[index].Weight;

						chooseds[index] = false;
					}
				}

				if (forward)
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
			return this.MaxTotalValue;
		}

		private bool Check()
		{
			return this.TotalWeight <= this.Cond.Capacity;
		}

		private void End()
		{
			this.MaxTotalValue = Math.Max(this.MaxTotalValue, this.TotalValue);
		}
	}
}
