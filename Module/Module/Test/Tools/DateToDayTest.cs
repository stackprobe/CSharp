using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class DateToDayTest
	{
		public void test01()
		{
			Console.WriteLine("" + DateToDay.Today.getDate());
			Console.WriteLine("" + DateToDay.toDate(DateToDay.Today.getDay()));
		}
	}
}
