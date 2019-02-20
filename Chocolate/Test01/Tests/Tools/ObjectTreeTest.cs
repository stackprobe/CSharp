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
			ObjectTree.Conv(new int[] { 1, 2, 3 });

			ObjectTree.Conv(new List<int>(new int[] { 4, 5, 6 }));

			ObjectTree.Conv(Test01_Get789());

			{
				Dictionary<string, string> dict = DictionaryTools.Create<string>();

				dict.Add("A", "1");
				dict.Add("BB", "22");
				dict.Add("CCC", "333");

				ObjectTree.Conv(dict);
			}
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
