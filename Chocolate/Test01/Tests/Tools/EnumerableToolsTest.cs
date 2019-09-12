using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EnumerableToolsTest
	{
		public void Test01()
		{
			Func<int> seq = EnumerableTools.Supplier(this.Test01_Ret123());

			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
			Console.WriteLine(seq());
		}

		private IEnumerable<int> Test01_Ret123()
		{
			yield return 1;
			yield return 2;
			yield return 3;
		}
	}
}
