using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class FieldsSerializerTest
	{
		public void test01()
		{
			Data01 d1 = new Data01()
			{
				s = "abc",
				i = 123,
				l = -456,
				b = true,
			};

			string[] lines = FieldsSerializer.serialize(d1);

			foreach (string line in lines)
				Console.WriteLine(line);

			Data01 d2 = new Data01();
			FieldsSerializer.deserialize(d2, lines);

			Console.WriteLine("s: " + d2.s);
			Console.WriteLine("i: " + d2.i);
			Console.WriteLine("l: " + d2.l);
			Console.WriteLine("b: " + d2.b);

			// ----

			d1 = new Data01();

			lines = FieldsSerializer.serialize(d1);

			foreach (string line in lines)
				Console.WriteLine(line);
		}

		public class Data01
		{
			public string s;
			public int i;
			public long l;
			public bool b;
		}
	}
}
