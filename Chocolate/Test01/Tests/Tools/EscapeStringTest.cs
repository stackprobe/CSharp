using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EscapeStringTest
	{
		public void Test01()
		{
			EscapeString es = new EscapeString();

			for (int c = 0; c < 10000; c++)
			{
				String str = Test01_MkStr();
				String enc = es.Encode(str);
				String dec = es.Decode(enc);

				Console.WriteLine("str: " + Test01_ForPrint(str));
				Console.WriteLine("enc: " + Test01_ForPrint(enc));
				Console.WriteLine("dec: " + Test01_ForPrint(dec));

				if (StringTools.Comp(str, dec) != 0)
					throw null; // bugged !!!
			}
		}

		private string Test01_MkStr()
		{
			return SecurityTools.MakePassword("\t\r\n trnsABCDEF", SecurityTools.CRandom.GetInt(30));
		}

		private string Test01_ForPrint(string str)
		{
			str = str.Replace("\t", "[TAB]");
			str = str.Replace("\r", "[CR]");
			str = str.Replace("\n", "[LF]");

			return "'" + str + "'";
		}
	}
}
