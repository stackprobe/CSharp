using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class DateTimeToSecTest
	{
		public void test01()
		{
			Console.WriteLine("" + DateTimeToSec.Now.getDateTime());
			Console.WriteLine("" + DateTimeToSec.toDateTime(DateTimeToSec.Now.getSec()));
		}
	}
}
