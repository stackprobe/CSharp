using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringTools
	{
		public static Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static List<char> ToCharList(string src)
		{
			return src.ToCharArray().ToList();
		}

		public static string ToString(List<char> src)
		{
			return new string(src.ToArray());
		}

		public static string Set(string str, int index, char chr)
		{
			List<char> tmp = ToCharList(str);
			tmp[index] = chr;
			return ToString(tmp);
		}

		public static string Insert(string str, int insertPos, string ptn)
		{
			List<char> tmp = ToCharList(str);
			tmp.InsertRange(insertPos, ptn.ToCharArray());
			return ToString(tmp);
		}

		public static string Remove(string str, int startPos, int count)
		{
			List<char> tmp = ToCharList(str);
			tmp.RemoveRange(startPos, count);
			return ToString(tmp);
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

		public class Comp : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a == b;
			}

			public int GetHashCode(string str)
			{
				return str.GetHashCode();
			}
		}

		public class CompIgnoreCase : IEqualityComparer<string>
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

		public static string Combine(string path1, string path2)
		{
			return PathFltr(path1 + '\\' + path2);
		}

		public static string PathFltr(string path)
		{
			path = path.Replace('/', '\\');

			bool networkFlag = path.StartsWith("\\\\");

			for (int c = 0; c < 20; c++)
				path = path.Replace("\\\\", "\\");

			if (networkFlag)
				path = '\\' + path;

			return path;
		}

		public static string Trim(string src)
		{
			List<char> tmp = ToCharList(src);

			for (int index = 0; index < tmp.Count; index++)
				if (tmp[index] < ' ')
					tmp[index] = ' ';

			string dest = ToString(tmp);

			for (int c = 0; c < 20; c++)
				dest = dest.Replace("  ", " ");

			dest = StringTools.RemoveStartsWith(dest, " ");
			dest = StringTools.RemoveEndsWith(dest, " ");

			return dest;
		}

		public static string RemoveStartsWith(string str, string ptn, bool ignoreCase = false)
		{
			if (StartsWith(str, ptn, ignoreCase))
				return str.Substring(ptn.Length);

			return str;
		}

		public static string RemoveEndsWith(string str, string ptn, bool ignoreCase = false)
		{
			if (EndsWith(str, ptn, ignoreCase))
				return str.Substring(0, str.Length - ptn.Length);

			return str;
		}

		public static bool StartsWith(string str, string ptn, bool ignoreCase)
		{
			if (ignoreCase)
			{
				str = str.ToLower();
				ptn = ptn.ToLower();
			}
			return str.StartsWith(ptn);
		}

		public static bool EndsWith(string str, string ptn, bool ignoreCase)
		{
			if (ignoreCase)
			{
				str = str.ToLower();
				ptn = ptn.ToLower();
			}
			return str.EndsWith(ptn);
		}
	}
}
