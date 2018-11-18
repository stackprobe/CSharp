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
				.Add(new string[] { "{" })
				.Add(oneToThree)
				.Add(oneToThree)
				.Add(oneToThree)
				.Add(new string[] { "}" });

			foreach (string s in new EnumerableList<string>()
				.Add(new string[] { "A" })
				.Add(oneToThree)
				.Add(new string[] { "B" })
				.Add(oneToThree_x3)
				.Add(new string[] { "C" })
				)
				Console.Write(" " + s);

			Console.WriteLine("");
		}
	}
}
