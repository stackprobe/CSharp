using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.wb.t20190630
{
	public class Test0001
	{
		public void Test01()
		{
			long s1 = DateTime.Now.Ticks / 10000000L;
			long s2 = DateTimeToSec.Now.GetSec();

			Console.WriteLine(s1);
			Console.WriteLine(s2);
		}
	}
}
