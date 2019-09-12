using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests
{
	public class Test0002
	{
		private IEnumerable<int> Test01_Ret123()
		{
			yield return 1;
			yield return 2;
			yield return 3;
		}

		public void Test01()
		{
			Func<int> reader = EnumerableTools.Supplier(Test01_Ret123());

			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine(reader());
			}
		}
	}
}
