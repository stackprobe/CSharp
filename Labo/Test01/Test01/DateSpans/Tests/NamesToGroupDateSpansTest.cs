﻿using System;
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
	}
}
