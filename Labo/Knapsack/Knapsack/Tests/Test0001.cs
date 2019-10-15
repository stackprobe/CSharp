using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tests
{
	public class Test0001
	{
		public void Test01()
		{
			while (Console.KeyAvailable == false)
			{
				Test01_Main();
			}
			Console.WriteLine("End");
		}

		private void Test01_Main()
		{
			Test01_A();
			Test01_B(ConditionUtils.Make());
			Test01_B(ConditionUtils.Make2());
		}

		private void Test01_A()
		{
			Condition cond = ConditionUtils.Make_Lite();

			Console.WriteLine(cond.Items.Length + " " + cond.Capacity);

			IKnapsack k1 = new Knapsack0001(cond);
			IKnapsack sk = new KnapsackSimple(cond);

			DateTime tm1 = DateTime.Now;
			int ret1 = k1.GetBestValue();
			DateTime tm2 = DateTime.Now;
			int ret2 = sk.GetBestValue();
			DateTime tm3 = DateTime.Now;

			Console.WriteLine((tm2 - tm1).TotalMilliseconds.ToString("F15"));
			Console.WriteLine((tm3 - tm2).TotalMilliseconds.ToString("F15"));

			Console.WriteLine(ret1);
			Console.WriteLine(ret2);

			if (ret1 != ret2)
				throw null; // bugged !!!
		}

		private void Test01_B(Condition cond)
		{
			Console.WriteLine(cond.Items.Length + " " + cond.Capacity);

			IKnapsack k1 = new Knapsack0001(cond);

			DateTime tm1 = DateTime.Now;
			int ret = k1.GetBestValue();
			DateTime tm2 = DateTime.Now;

			Console.WriteLine((tm2 - tm1).TotalMilliseconds.ToString("F15"));

			Console.WriteLine(ret);
		}
	}
}
