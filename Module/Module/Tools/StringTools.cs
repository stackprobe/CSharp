using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringTools
	{
		public static Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static string Set(string str, int index, char chr)
		{
			return str.Substring(0, index) + chr + str.Substring(index + 1);
		}

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

		public static string Combine(string path1, string path2)
		{
			return PathFltr(path1 + '\\' + path2);
		}

		public static string PathFltr(string path)
		{
			bool networkFlag = path.StartsWith("\\\\");

			for (int c = 0; c < 20; c++)
				path = path.Replace("\\\\", "\\");

			if (networkFlag)
				path = '\\' + path;

			return path;
		}

		public static string Trim(string str)
		{
			for (int index = 0; index < str.Length; index++)
				if (str[index] < ' ')
					str = Set(str, index, ' ');

			for (int c = 0; c < 20; c++)
				str = str.Replace("  ", " ");

			if (str.StartsWith(" "))
				str = str.Substring(1);

			if (str.EndsWith(" "))
				str = str.Substring(0, str.Length - 1);

			return str;
		}
	}
}
