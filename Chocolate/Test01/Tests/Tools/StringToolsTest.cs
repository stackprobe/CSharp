using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class StringToolsTest
	{
		public void Test01()
		{
			{
				StringTools.Enclosed encl = StringTools.GetEnclosedIgnoreCase(
@"<html>
<head>
	<title>123</title>
</head>
<body>
	<div>EnclosedByDiv_01</div>
	<div>EnclosedByDiv_02</div>
	<div>EnclosedByDiv_03</div>
</body>
</html>",
					"<DIV>",
					"</DIV>"
					);

				if (encl.Inner != "EnclosedByDiv_01") throw null;

				encl = StringTools.GetEnclosedIgnoreCase(encl.Str, "<DIV>", "</DIV>", encl.EndPtn.End);

				if (encl.Inner != "EnclosedByDiv_02") throw null;

				encl = StringTools.GetEnclosedIgnoreCase(encl.EndPtn.Right, "<DIV>", "</DIV>");

				if (encl.Inner != "EnclosedByDiv_03") throw null;
			}

			{
				StringTools.Enclosed[] encls = StringTools.GetAllEnclosed("<<<a>>><<<b>>><<<c>>>", "<<<", ">>>");

				if (encls.Length != 3) throw null;
				if (encls[0].Inner != "a") throw null;
				if (encls[1].Inner != "b") throw null;
				if (encls[2].Inner != "c") throw null;
			}
		}

		public void Test02()
		{
			Console.WriteLine("" + StringTools.Comp("deymd", "dsymd"));
		}

		public void Test03()
		{
			Console.WriteLine(StringTools.SetCharAt("ABCDE", 0, '$'));
			Console.WriteLine(StringTools.SetCharAt("ABCDE", 1, '$'));
			Console.WriteLine(StringTools.SetCharAt("ABCDE", 2, '$'));
			Console.WriteLine(StringTools.SetCharAt("ABCDE", 3, '$'));
			Console.WriteLine(StringTools.SetCharAt("ABCDE", 4, '$'));
		}

		public void Test04()
		{
			string a = StringTools.ASCII;
			string b = StringTools.GetString_SJISHalfCodeRange(0x21, 0x7e);

			Console.WriteLine("a: " + a);
			Console.WriteLine("b: " + b);

			a = Sort(a);
			b = Sort(b);

			Console.WriteLine("a: " + a);
			Console.WriteLine("b: " + b);

			if (a != b)
				throw null; // bugged !!!
		}

		private string Sort(string str)
		{
			char[] chrs = str.ToCharArray();
			Array.Sort(chrs, (a, b) => (int)a - (int)b);
			return new string(chrs);
		}
	}
}
