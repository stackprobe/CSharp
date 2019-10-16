using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public static class ConditionUtils
	{
		public static Condition Make_Lite()
		{
			return Make(
				(int)SecurityTools.CRandom.GetRandom((uint)30),
				10000,
				100,
				(int)SecurityTools.CRandom.GetRandom((uint)3000)
				);
		}

		public static Condition Make()
		{
			return Make(
				(int)SecurityTools.CRandom.GetRandom((uint)500),
				10000,
				100,
				(int)SecurityTools.CRandom.GetRandom((uint)50000)
				);
		}

		public static Condition Make2()
		{
			return Make(
				(int)SecurityTools.CRandom.GetRandom((uint)3000),
				10000,
				300,
				(int)SecurityTools.CRandom.GetRandom((uint)300)
				);
		}

		public static Condition Make(int itemCount, int valueLimit, int weightLimit, int capacity)
		{
			Condition cond = new Condition()
			{
				Items = new Item[itemCount],
				Capacity = capacity,
			};

			for (int index = 0; index < itemCount; index++)
			{
				cond.Items[index] = new Item()
				{
					Value = (int)SecurityTools.CRandom.GetRandom((uint)valueLimit),
					Weight = (int)SecurityTools.CRandom.GetRandom((uint)weightLimit),
				};
			}
			Console.WriteLine(string.Join(" ", cond.Items.Length, valueLimit, weightLimit, cond.Capacity)); // test
			return cond;
		}
	}
}
