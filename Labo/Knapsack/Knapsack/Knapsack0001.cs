using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Knapsack0001 : IKnapsack
	{
		private Condition Cond;

		public Knapsack0001(Condition cond)
		{
			this.Cond = cond;
		}

		public int GetBestValue()
		{
			int ret = 0;
			int[][] item_weight2Value = new int[this.Cond.Items.Length][];

			for (int i = 0; i < this.Cond.Items.Length; i++)
			{
				item_weight2Value[i] = new int[this.Cond.Capacity + 1];

				int w = this.Cond.Items[i].Weight;

				if (w <= this.Cond.Capacity)
				{
					int v = this.Cond.Items[i].Value;

					ret = Math.Max(ret, v);
					item_weight2Value[i][w] = v;
				}
			}
			for (int i = 0; i < this.Cond.Items.Length; i++)
			{
				for (int w = 0; w <= this.Cond.Capacity; w++)
				{
					int v = item_weight2Value[i][w];

					if (v != 0) // .Value == 0 のアイテムだけの組み合わせは無視して良い。
					{
						for (int ii = i + 1; ii < this.Cond.Items.Length; ii++)
						{
							int ww = w + this.Cond.Items[ii].Weight;

							if (ww <= this.Cond.Capacity)
							{
								int vv = v + this.Cond.Items[ii].Value;

								if (item_weight2Value[ii][ww] < vv)
								{
									ret = Math.Max(ret, vv);
									item_weight2Value[ii][ww] = vv;
								}
							}
						}
					}
				}
			}
			return ret;
		}
	}
}
