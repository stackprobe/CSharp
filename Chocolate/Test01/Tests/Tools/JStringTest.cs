using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class JStringTest
	{
		public void Test01()
		{
			for (int c = 0; c < 100; c++)
			{
				int l = SecurityTools.CRandom.GetInt(100);
				byte[] b = SecurityTools.CRandom.GetBytes(l);
				string s = JString.ToJString(b, true, true, true, true);

				Console.WriteLine(string.Join(", ", b.Select(chr => chr.ToString("x2"))));
				Console.WriteLine("\"" + s + "\"");
			}
		}
	}
}
