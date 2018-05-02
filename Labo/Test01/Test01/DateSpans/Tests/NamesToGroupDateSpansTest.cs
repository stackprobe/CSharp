using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.DateSpans.Tests
{
	public class NamesToGroupDateSpansTest
	{
		public void Test01()
		{
			string[] names = new string[]
			{
				"AAAA20180101BBBB.dat",
				"AAAA20180102BBBB.dat",
				"AAAA20180103BBBB.dat",
				"AAAA20180104BBBB.dat",
				"AAAA20180105BBBB.dat",
				"AAAA20180110BBBB.dat",
				"ccc20180111ddd.dat",
				"ccc20180112ddd.dat",
				"ccc20180113ddd.dat",
				"hogehoge",
				"hogehoge",
				"hoge01",
				"hoge02",
			};

			NamesToGroupDateSpans ntgds = new NamesToGroupDateSpans();
			ntgds.Add(names);
			Console.WriteLine(ntgds.GetString());
		}

		public void Test02()
		{
			string[] names = new string[]
			{
				"20171221",
				"20171222",
				"20171223",
				"20171224",
				"20171225",
				"20180101.dat",
				"20180101.dat",
				"20180101.dat",
				"_20180111",
				"_20180112",
				"_20180113",
				"_20180114",
				"_20180112",
				"_20180113",
				"_20180114",
				"_20180112",
				"_20180113",
				"_20180114",
				"_20180115",
			};

			NamesToGroupDateSpans ntgds = new NamesToGroupDateSpans();
			ntgds.Add(names);
			Console.WriteLine(ntgds.GetString());
		}
	}
}
