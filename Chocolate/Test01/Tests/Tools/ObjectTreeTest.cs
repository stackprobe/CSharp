using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class ObjectTreeTest
	{
		public void Test01()
		{
			Test01_a(ObjectTree.Conv(new int[] { 1, 2, 3 }));
			Test01_a(ObjectTree.Conv(new List<int>(new int[] { 4, 5, 6 })));
			Test01_a(ObjectTree.Conv(Test01_Get789()));

			{
				Dictionary<string, int> dict = DictionaryTools.Create<int>();

				dict.Add("A", 1);
				dict.Add("BB", 22);
				dict.Add("CCC", 333);

				Test01_a(ObjectTree.Conv(dict));
			}

			{
				Dictionary<string, string> dict = DictionaryTools.Create<string>();

				dict.Add("a", "s1");
				dict.Add("bb", "s22");
				dict.Add("ccc", "s333");

				Test01_a(ObjectTree.Conv(dict));
			}
		}

		private void Test01_a(object src)
		{
			Console.WriteLine(JsonTools.Encode(src));
		}

		private IEnumerable<int> Test01_Get789()
		{
			for (int c = 7; c <= 9; c++)
			{
				yield return c;
			}
		}
	}
}
