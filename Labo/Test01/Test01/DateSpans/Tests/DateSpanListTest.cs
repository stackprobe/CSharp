using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.DateSpans.Tests
{
	public class DateSpanListTest
	{
		public void Test01()
		{
			Test01_a("20180101,20180102,20180103,20180104,20180105");
			Test01_a("20180101,20180102,20180104,20180105");
			Test01_a("20180101-20180110,20180111-20180120");
			Test01_a("20180101-20180110,20180120-20180131");
			Test01_a("20180103,20180102,20180101");
			//Test01_a("20100101-20201231");

			//Test01_a("20180303-20180202"); // 例外
			//Test01_a("2018xxyy"); // 例外
			//Test01_a("20181332"); // 例外
		}

		private void Test01_a(string str)
		{
			Console.WriteLine("< " + str);

			{
				DateSpanList dsl = new DateSpanList();
				dsl.Add(str);
				dsl.Sort();
				dsl.Join();
				Console.WriteLine("> " + dsl.GetString());
			}

			{
				DateSpanList dsl = new DateSpanList();
				dsl.Add(str);
				dsl.Sort();
				dsl.Distinct();
				dsl.Join();
				Console.WriteLine("> " + dsl.GetString());
			}

			{
				DateSpanList dsl = new DateSpanList();
				dsl.Add(str);
				dsl.Sort();
				Console.WriteLine("> " + dsl.GetStringDates());
			}
		}
	}
}
