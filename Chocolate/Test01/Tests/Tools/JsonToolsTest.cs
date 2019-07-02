using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class JsonToolsTest
	{
		public void Test01()
		{
			//object src = DebugTools.ToListOrMap(DateTime.Now);
			object src = DebugTools.ToListOrMap(DateTime.Now, 4);
			//object src = DebugTools.ToListOrMap(DateTime.Now, 5); // 重すぎ

			string dest = JsonTools.Encode(src);

			File.WriteAllText(@"C:\temp\1.json", dest, Encoding.UTF8);

			src = JsonTools.Decode(File.ReadAllBytes(@"C:\temp\1.json"));
			dest = JsonTools.Encode(src);

			File.WriteAllText(@"C:\temp\2.json", dest, Encoding.UTF8);
		}

		public void Test02()
		{
			Test02_a("[ 1, 2, 3]");
			Test02_a("[ 1, 2, 3,]"); // warning x1
			Test02_a("{ \"a\": 1, \"b\": 2, \"c\": 3 }");
			Test02_a("{ \"a\": 1, \"b\": 2, \"c\": 3, }"); // warning x1
			Test02_a("{ 1: 2, 3: 4, 5: 6 }");  // warning x3
			Test02_a("{ 1: 2, 3: 4, 5: 6, }"); // warning x4

			Test02_a("1");
			Test02_a("\"a\"");
			Test02_a("[]");
			Test02_a("{}");
			Test02_a("[   ]");
			Test02_a("{   }");

			Test02_a(","); // warning x1
			Test02_a(",,"); // warning x1
			Test02_a(",,,"); // warning x1
			Test02_a("tea"); // warning x1
			Test02_a("tea time"); // warning x1
			Test02_a("tea time 123"); // warning x1

			Test02_a("true");
			Test02_a("false");
			Test02_a("null");
			Test02_a("-1.234e+99");
		}

		private static void Test02_a(string json)
		{
			Console.WriteLine("< " + json);

			object ret = JsonTools.Decode(json);

			Console.WriteLine("> " + ret);

			string ret2 = JsonTools.Encode(ret);
			string ret3 = JsonTools.Encode(ret, true);

			//Console.WriteLine(">2 " + ret2);
			//Console.WriteLine(">3 " + ret3);
		}

		public void Test03()
		{
			Console.WriteLine(JsonTools.Encode(ObjectTree.Conv(new object[]
			{
				"String",
				true,
				false,
				123,
				(Int16)456,
				789L,
				123.456,
				789.012F,
			}
			)));
		}
	}
}
