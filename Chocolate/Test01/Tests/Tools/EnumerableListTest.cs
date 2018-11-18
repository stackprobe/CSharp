using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EnumerableListTest
	{
		public void Test01()
		{
			string[] oneToThree = StringTools.Tokenize("1:2:3", ":");

			EnumerableList<string> oneToThree_x3 = new EnumerableList<string>()
				.AddOne("{")
				.Add(oneToThree)
				.Add(oneToThree)
				.Add(oneToThree)
				.AddOne("}");

			foreach (string s in new EnumerableList<string>()
				.AddOne("A")
				.Add(oneToThree)
				.AddOne("B")
				.Add(oneToThree_x3)
				.AddOne("C")
				)
				Console.Write(" " + s);

			Console.WriteLine("");
		}
	}
}
