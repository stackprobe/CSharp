using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests2;

namespace Charlotte.Tests.Tools
{
	public class DebugToolsTest
	{
		public void test01()
		{
			object value = DebugTools.toListOrMap(new Sample());

			Console.WriteLine("" + value);
		}
	}
}
