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

		public void Test05()
		{
			Console.WriteLine(StringTools.MultiReplace("ABC", "A", "ABC", "B", "BAC", "C", "CAB"));
			Console.WriteLine(StringTools.MultiReplace("[A][B][C]", "A", "(ABC)", "B", "(BAC)", "C", "(CAB)"));

			Console.WriteLine(StringTools.MultiReplace("@@@@", "@", "1", "@@", "2", "@@@", "3"));
			Console.WriteLine(StringTools.MultiReplace("@@@@@", "@", "1", "@@", "2", "@@@", "3"));
			Console.WriteLine(StringTools.MultiReplace("@@@@@@", "@", "1", "@@", "2", "@@@", "3"));
		}

		public void Test06()
		{
			Console.WriteLine(string.Join(", ", StringTools.Tokenize("A=B=C", "=", false, false, 2)));
		}

		public void Test07()
		{
			Test07_b("");

			Test07_a("\t\n\r " + StringTools.HALF + "あいうえおカキクケコ日本語");
			Test07_a("TEL☎123");
			Test07_a("TEL☎");
			Test07_a("☎〠❤");
		}

		private void Test07_a(string allowChars)
		{
			//Test07_b(""); // moved

			for (int c = 1; c < 10000; c++)
			{
				Test07_b(SecurityTools.MakePassword(allowChars, SecurityTools.CRandom.GetRange(1, 100)));
			}
		}

		private void Test07_b(string str)
		{
			string eStr = StringTools.LiteEncode(str);
			string dStr = StringTools.LiteDecode(eStr);

			if (str != dStr)
				throw null; // bugged !!!
		}

		public void Test08()
		{
			Test08_a("", StringTools.KANA, false); // 常に "" は false

			Test08_a("A", "A", true);
			Test08_a("A", "B", false);

			for (int c = 0; c < 10000; c++)
			{
				Test08_a(
					c.ToString(),
					StringTools.DECIMAL,
					true
					);
				Test08_a(
					SecurityTools.MakePassword(StringTools.ALPHA + StringTools.alpha + StringTools.DECIMAL, SecurityTools.CRandom.GetRange(200, 250)),
					StringTools.ALPHA,
					false // HACK: 確率的
					);
				Test08_a(
					SecurityTools.MakePassword(StringTools.ALPHA + StringTools.alpha, SecurityTools.CRandom.GetRange(100, 150)),
					StringTools.ALPHA + StringTools.alpha + StringTools.DECIMAL,
					true
					);
				Test08_a(
					SecurityTools.MakePassword(StringTools.KANA, SecurityTools.CRandom.GetRange(10, 15)) + "あ",
					StringTools.KANA,
					false
					);
			}
		}

		private void Test08_a(string target, string allowChars, bool expectedResult)
		{
			if (StringTools.LiteValidate(target, allowChars) != expectedResult)
				throw null; // bugged !!!
		}
	}
}
