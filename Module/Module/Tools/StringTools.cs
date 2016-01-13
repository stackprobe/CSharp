using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringTools
	{
		public static Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static string Insert(string str, int index, string appendix)
		{
			return str.Substring(0, index) + appendix + str.Substring(index);
		}

		public static string Remove(string str, int index, int count)
		{
			return str.Substring(0, index) + str.Substring(index + count);
		}

		public static string[] RemoveEmpty(string[] src)
		{
			List<string> dest = new List<string>();

			foreach (string line in src)
				if (string.IsNullOrEmpty(line) == false)
					dest.Add(line);

			return dest.ToArray();
		}

		public static string ZPad(int value, int minlen = 1)
		{
			return ZPad("" + value, minlen);
		}

		public static string ZPad(string str, int minlen = 1)
		{
			return LPad(str, minlen, '0');
		}

		public static string LPad(string str, int minlen, char padding)
		{
			while (str.Length < minlen)
			{
				str = padding + str;
			}
			return str;
		}

		public class IgnoreCaseIEComparer : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a.ToLower() == b.ToLower();
			}

			public int GetHashCode(string str)
			{
				return str.ToLower().GetHashCode();
			}
		}

		public static bool IsSame(string[] lines1, string[] lines2, bool ignoreCase = false)
		{
			if (lines1.Length != lines2.Length)
				return false;

			for (int index = 0; index < lines1.Length; index++)
				if (IsSame(lines1[index], lines2[index], ignoreCase) == false)
					return false;

			return true;
		}

		public static bool IsSame(string str1, string str2, bool ignoreCase = false)
		{
			if (ignoreCase)
			{
				str1 = str1.ToLower();
				str2 = str2.ToLower();
			}
			return str1 == str2;
		}
	}
}
