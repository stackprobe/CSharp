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
			Sample sample = new Sample();

			sample.SubClass = new Sample2();
			sample.SubClass2 = new Sample2();

			object value = DebugTools.toListOrMap(sample);

			//Console.WriteLine("" + value);

			Console.WriteLine(JsonTools.encode(value));
		}
	}
}
