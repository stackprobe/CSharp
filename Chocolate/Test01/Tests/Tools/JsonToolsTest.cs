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
	}
}
