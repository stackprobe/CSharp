using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringTools
	{
		public const string DIGIT = "0123456789";
		public const string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public const string alpha = "abcdefghijklmnopqrstuvwxyz";
		public const string HEXADECIMAL = "0123456789ABCDEF";
		public const string hexadecimal = "0123456789abcdef";
		public const string OCTODECIMAL = "01234567";
		public const string BINADECIMAL = "01";

		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static List<string> tokenize(string str, string delimiters, bool meaningFlag = false, bool ignoreEmpty = false)
		{
			StringBuilder buff = new StringBuilder();
			List<string> tokens = new List<string>();

			foreach (char chr in str)
			{
				if (delimiters.Contains(chr) == meaningFlag)
				{
					buff.Append(chr);
				}
				else
				{
					if (ignoreEmpty == false || buff.Length != 0)
						tokens.Add(buff.ToString());

					buff = new StringBuilder();
				}
			}
			if (ignoreEmpty == false || buff.Length != 0)
				tokens.Add(buff.ToString());

			return tokens;
		}

		public static string replace(string str, string fromChrs, char toChr)
		{
			foreach (char fromChr in fromChrs)
			{
				str = str.Replace(fromChr, toChr);
			}
			return str;
		}

		public static string replaceLoop(string str, string fromPtn, string toPtn, int maxCount)
		{
			for (int count = 0; count < maxCount; count++)
			{
				str = str.Replace(fromPtn, toPtn);
			}
			return str;
		}

		public static Comparison<string> comp = delegate(string a, string b)
		{
			return a.CompareTo(b);
		};

		public static Comparison<string> compIgnoreCase = delegate(string a, string b)
		{
			return a.ToLower().CompareTo(b.ToLower());
		};

		public static string zPad(string str, int minlen, string padding = "0")
		{
			while (str.Length < minlen)
			{
				str = padding + str;
			}
			return str;
		}

		public static bool startsWithIgnoreCase(string str, string ptn)
		{
			return str.ToLower().StartsWith(ptn.ToLower());
		}

		public static bool endsWithIgnoreCase(string str, string ptn)
		{
			return str.ToLower().EndsWith(ptn.ToLower());
		}

		public static bool equalsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}

		public static string getUUID()
		{
			return Guid.NewGuid().ToString("B");
		}
	}
}
