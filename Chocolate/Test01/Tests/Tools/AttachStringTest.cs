using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class AttachStringTest
	{
		public void Test01()
		{
			AttachString attStr = new AttachString();

			for (int c = 0; c < 10000; c++)
			{
				String[] tkns = Test01_MkTkns();
				String str = attStr.Untokenize(tkns);
				String[] tkns2 = attStr.Tokenize(str);

				Console.WriteLine("tkns_: " + string.Join(", ", tkns));
				Console.WriteLine("str: " + str);
				Console.WriteLine("tkns2: " + string.Join(", ", tkns2));

				if (ArrayTools.Comp(tkns, tkns2, StringTools.Comp) != 0)
					throw null; // bugged !!!
			}
		}

		private string[] Test01_MkTkns()
		{
			string[] tkns = new string[SecurityTools.CRandom.GetInt(10)];

			for (int index = 0; index < tkns.Length; index++)
				tkns[index] = SecurityTools.MakePassword(":$.ABC", SecurityTools.CRandom.GetInt(10));

			return tkns;
		}
	}
}
