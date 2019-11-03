using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class SortedListTest
	{
		public void Test01()
		{
			Test01_a(new string[] { "1", "2", "3" }, "1", new string[] { "1" });
			Test01_a(new string[] { "1", "2", "3" }, "2", new string[] { "2" });
			Test01_a(new string[] { "1", "2", "3" }, "3", new string[] { "3" });
			Test01_a(new string[] { "1", "2", "2", "3", "3", "3" }, "0", new string[] { });
			Test01_a(new string[] { "1", "2", "2", "3", "3", "3" }, "1", new string[] { "1", });
			Test01_a(new string[] { "1", "2", "2", "3", "3", "3" }, "2", new string[] { "2", "2" });
			Test01_a(new string[] { "1", "2", "2", "3", "3", "3" }, "3", new string[] { "3", "3", "3" });
			Test01_a(new string[] { "1", "2", "2", "3", "3", "3" }, "4", new string[] { });
			Test01_a(new string[] { "1", "1", "1", "1", "3", "3", "3", "3" }, "2", new string[] { });
			Test01_a(new string[] { }, "1", new string[] { });
		}

		private void Test01_a(string[] arr, string target, string[] expect)
		{
			SortedList<string> list = new SortedList<string>(StringTools.Comp);
			list.AddRange(arr);
			string[] ans = list.GetMatch(list.GetFerret(target)).ToArray();

			if (ArrayTools.Comp<string>(ans, expect, StringTools.Comp) != 0)
				throw null;
		}

		public void Test02()
		{
			Test02_a(new string[] { "1", "2", "3" }, "1", new string[] { null, "1", "2" });
			Test02_a(new string[] { "1", "2", "3" }, "2", new string[] { "1", "2", "3" });
			Test02_a(new string[] { "1", "2", "3" }, "3", new string[] { "2", "3", null });
			Test02_a(new string[] { "1", "2", "2", "3", "3", "3" }, "0", new string[] { null, "1" });
			Test02_a(new string[] { "1", "2", "2", "3", "3", "3" }, "1", new string[] { null, "1", "2" });
			Test02_a(new string[] { "1", "2", "2", "3", "3", "3" }, "2", new string[] { "1", "2", "2", "3" });
			Test02_a(new string[] { "1", "2", "2", "3", "3", "3" }, "3", new string[] { "2", "3", "3", "3", null });
			Test02_a(new string[] { "1", "2", "2", "3", "3", "3" }, "4", new string[] { "3", null });
			Test02_a(new string[] { "1", "1", "1", "1", "3", "3", "3", "3" }, "2", new string[] { "1", "3" });
			Test02_a(new string[] { }, "1", new string[] { null, null });
		}

		private void Test02_a(string[] arr, string target, string[] expect)
		{
			SortedList<string> list = new SortedList<string>(StringTools.Comp);
			list.AddRange(arr);
			string[] ans = list.GetMatchWithEdge(list.GetFerret(target)).ToArray();

			if (ArrayTools.Comp<string>(ans, expect, (a, b) => StringTools.Comp("" + a, "" + b)) != 0)
				throw null;
		}
	}
}
