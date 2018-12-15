using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EnumerableTrainTest
	{
		public void Test01()
		{
			string[] oneToThree = StringTools.Tokenize("1:2:3", ":");

			EnumerableTrain<string> oneToThree_x3 = new EnumerableTrain<string>()
				.AddOne("{")
				.Add(oneToThree)
				.Add(oneToThree)
				.Add(oneToThree)
				.AddOne("}");

			foreach (string s in new EnumerableTrain<string>()
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
