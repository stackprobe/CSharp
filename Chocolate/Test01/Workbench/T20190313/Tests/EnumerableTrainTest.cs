using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Workbench.T20190313.Tests
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

		public void Test02()
		{
			string[] strs = "A:BB:CCC".Split(':');

			EnumerableTrain<string> strtbl = new EnumerableTrain<string>();

			strtbl.Add(strs);
			strtbl.Add(strs);
			strtbl.Add(strs);

			Console.WriteLine("*1");
			foreach (string str in strtbl)
				Console.WriteLine(str);

			Console.WriteLine("*2");
			foreach (string str in strtbl.Iterate())
				Console.WriteLine(str);
		}
	}
}
