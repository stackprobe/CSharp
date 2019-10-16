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
			int testCnt = 0;

			while (Console.KeyAvailable == false)
			{
				Console.WriteLine("testCnt: " + (++testCnt));
				Test01_Main();
			}
			Console.WriteLine("End");
		}

		private void Test01_Main()
		{
			Console.WriteLine("**** Test01_All ****");
			Test01_All(ConditionUtils.Make_Lite());

			Console.WriteLine("**** Test01_K1 ****");
			Test01_K1(ConditionUtils.Make());

			Console.WriteLine("**** Test01_K1 ****");
			Test01_K1(ConditionUtils.Make2());
		}

		private void Test01_All(Condition cond)
		{
			IKnapsack sk = new KnapsackSimple(cond);
			IKnapsack k1 = new Knapsack0001(cond);

			DateTime tm1 = DateTime.Now;
			int ret1 = sk.GetBestValue();
			DateTime tm2 = DateTime.Now;
			int ret2 = k1.GetBestValue();
			DateTime tm3 = DateTime.Now;

			Console.WriteLine((tm2 - tm1).TotalMilliseconds.ToString("F15"));
			Console.WriteLine((tm3 - tm2).TotalMilliseconds.ToString("F15"));

			Console.WriteLine(ret1);
			Console.WriteLine(ret2);

			if (ret1 != ret2)
				throw null; // bugged !!!
		}

		private void Test01_K1(Condition cond)
		{
			IKnapsack k1 = new Knapsack0001(cond);

			DateTime tm1 = DateTime.Now;
			int ret = k1.GetBestValue();
			DateTime tm2 = DateTime.Now;

			Console.WriteLine((tm2 - tm1).TotalMilliseconds.ToString("F15"));

			Console.WriteLine(ret);
		}
	}
}
