using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class StringTools
	{
		public static Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static string BINADECIMAL = "01";
		public static string OCTODECIMAL = "012234567";
		public static string DECIMAL = "0123456789";
		public static string HEXADECIMAL = "0123456789ABCDEF";
		public static string hexadecimal = "0123456789abcdef";

		public static string ALPHA = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
		public static string alpha = "abcdefghijklmnopqrstuvwxyz";
		public static string PUNCT =
			GetString_SJISHalfCodeRange(0x21, 0x2f) +
			GetString_SJISHalfCodeRange(0x3a, 0x40) +
			GetString_SJISHalfCodeRange(0x5b, 0x60) +
			GetString_SJISHalfCodeRange(0x7b, 0x7e);

		public static string ASCII = DECIMAL + ALPHA + alpha + PUNCT; // == GetString_SJISHalfCodeRange(0x21, 0x7e)
		public static string KANA = GetString_SJISHalfCodeRange(0xa1, 0xdf);

		public static string HALF = ASCII + KANA;

		public static string GetString_SJISHalfCodeRange(int codeMin, int codeMax)
		{
			byte[] buff = new byte[codeMax - codeMin + 1];

			for (int code = codeMin; code <= codeMax; code++)
			{
				buff[code - codeMin] = (byte)code;
			}
			return ENCODING_SJIS.GetString(buff);
		}

		public static string MBC_DECIMAL = GetString_SJISCodeRange(0x82, 0x4f, 0x58);
		public static string MBC_ALPHA = GetString_SJISCodeRange(0x82, 0x60, 0x79);
		public static string mbc_alpha = GetString_SJISCodeRange(0x82, 0x81, 0x9a);
		public static string MBC_SPACE = GetString_SJISCodeRange(0x81, 0x40, 0x40);
		public static string MBC_PUNCT =
			GetString_SJISCodeRange(0x81, 0x41, 0x7e) +
			GetString_SJISCodeRange(0x81, 0x80, 0xac) +
			GetString_SJISCodeRange(0x81, 0xb8, 0xbf) + // 集合
			GetString_SJISCodeRange(0x81, 0xc8, 0xce) + // 論理
			GetString_SJISCodeRange(0x81, 0xda, 0xe8) + // 数学
			GetString_SJISCodeRange(0x81, 0xf0, 0xf7) +
			GetString_SJISCodeRange(0x81, 0xfc, 0xfc) +
			GetString_SJISCodeRange(0x83, 0x9f, 0xb6) + // ギリシャ語大文字
			GetString_SJISCodeRange(0x83, 0xbf, 0xd6) + // ギリシャ語小文字
			GetString_SJISCodeRange(0x84, 0x40, 0x60) + // キリル文字大文字
			GetString_SJISCodeRange(0x84, 0x70, 0x7e) + // キリル文字小文字(1)
			GetString_SJISCodeRange(0x84, 0x80, 0x91) + // キリル文字小文字(2)
			GetString_SJISCodeRange(0x84, 0x9f, 0xbe) + // 枠線
			GetString_SJISCodeRange(0x87, 0x40, 0x5d) + // 機種依存文字(1)
			GetString_SJISCodeRange(0x87, 0x5f, 0x75) + // 機種依存文字(2)
			GetString_SJISCodeRange(0x87, 0x7e, 0x7e) + // 機種依存文字(3)
			GetString_SJISCodeRange(0x87, 0x80, 0x9c) + // 機種依存文字(4)
			GetString_SJISCodeRange(0xee, 0xef, 0xfc); // 機種依存文字(5)

		public static string MBC_HIRA = GetString_SJISCodeRange(0x82, 0x9f, 0xf1);
		public static string MBC_KANA =
			GetString_SJISCodeRange(0x83, 0x40, 0x7e) +
			GetString_SJISCodeRange(0x83, 0x80, 0x96);

		private static string GetString_SJISCodeRange(int lead, int trailMin, int trailMax)
		{
			byte[] buff = new byte[(trailMax - trailMin + 1) * 2];

			for (int trail = trailMin; trail <= trailMax; trail++)
			{
				buff[(trail - trailMin) * 2 + 0] = (byte)lead;
				buff[(trail - trailMin) * 2 + 1] = (byte)trail;
			}
			return ENCODING_SJIS.GetString(buff);
		}

		public static int Comp(string a, string b)
		{
			return BinTools.Comp(Encoding.UTF8.GetBytes(a), Encoding.UTF8.GetBytes(b));
		}

		public static int CompIgnoreCase(string a, string b)
		{
			return Comp(a.ToLower(), b.ToLower());
		}

		public class IEComp : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a == b;
			}

			public int GetHashCode(string a)
			{
				return a.GetHashCode();
			}
		}

		public class IECompIgnoreCase : IEqualityComparer<string>
		{
			public bool Equals(string a, string b)
			{
				return a.ToLower() == b.ToLower();
			}

			public int GetHashCode(string a)
			{
				return a.ToLower().GetHashCode();
			}
		}

		public static bool EqualsIgnoreCase(string a, string b)
		{
			return a.ToLower() == b.ToLower();
		}

		public static bool StartsWithIgnoreCase(string str, string ptn)
		{
			return str.ToLower().StartsWith(ptn.ToLower());
		}

		public static bool EndsWithIgnoreCase(string str, string ptn)
		{
			return str.ToLower().EndsWith(ptn.ToLower());
		}

		public static bool ContainsIgnoreCase(string str, string ptn)
		{
			return str.ToLower().Contains(ptn.ToLower());
		}

		public static int IndexOfIgnoreCase(string str, string ptn)
		{
			return str.ToLower().IndexOf(ptn.ToLower());
		}

		public class Island
		{
			public string Str;
			public int Start;
			public int End;

			public string Left
			{
				get
				{
					return this.Str.Substring(0, this.Start);
				}
			}

			public int InnerLength
			{
				get
				{
					return this.End - this.Start;
				}
			}

			public string Inner
			{
				get
				{
					return this.Str.Substring(this.Start, this.InnerLength);
				}
			}

			public string Right
			{
				get
				{
					return this.Str.Substring(this.End);
				}
			}
		}

		public static Island GetIsland(string str, string mid, int startIndex = 0)
		{
			int index = str.IndexOf(mid, startIndex);

			if (index == -1)
				return null;

			return new Island()
			{
				Str = str,
				Start = index,
				End = index + mid.Length,
			};
		}

		public class Enclosed
		{
			public Island StartPtn;
			public Island EndPtn;

			public string Str
			{
				get
				{
					return this.StartPtn.Str;
				}

				set
				{
					this.StartPtn.Str = value;
					this.EndPtn.Str = value;
				}
			}

			public string Left
			{
				get
				{
					return this.Str.Substring(0, this.StartPtn.End); // == this.StartPtn.Left + this.StartPtn.Inner
				}
			}

			public int InnerLength
			{
				get
				{
					return this.EndPtn.Start - this.StartPtn.End;
				}
			}

			public string Inner
			{
				get
				{
					return this.Str.Substring(this.StartPtn.End, this.InnerLength);
				}
			}

			public string Right
			{
				get
				{
					return this.Str.Substring(this.EndPtn.Start); // == this.EndPtn.Inner + this.EndPtn.Right
				}
			}
		}

		public static Enclosed GetEnclosed(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			Enclosed ret = new Enclosed();

			ret.StartPtn = GetIsland(str, startPtn, startIndex);

			if (ret.StartPtn == null)
				return null;

			ret.EndPtn = GetIsland(str, endPtn, ret.StartPtn.End);

			if (ret.EndPtn == null)
				return null;

			return ret;
		}

		public static Enclosed GetEnclosedIgnoreCase(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			Enclosed ret = GetEnclosed(
				str.ToLower(),
				startPtn.ToLower(),
				endPtn.ToLower(),
				startIndex
				);

			if (ret != null)
				ret.Str = str;

			return ret;
		}

		public static Enclosed[] GetAllEnclosed(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			List<Enclosed> dest = new List<Enclosed>();

			for (; ; )
			{
				Enclosed encl = GetEnclosed(str, startPtn, endPtn, startIndex);

				if (encl == null)
					break;

				dest.Add(encl);
				startIndex = encl.EndPtn.End;
			}
			return dest.ToArray();
		}

		public static Enclosed[] GetAllEnclosedIgnoreCase(string str, string startPtn, string endPtn, int startIndex = 0)
		{
			Enclosed[] ret = GetAllEnclosed(
				str.ToLower(),
				startPtn.ToLower(),
				endPtn.ToLower(),
				startIndex
				);

			foreach (Enclosed encl in ret)
				encl.Str = str;

			return ret;
		}

		public static string[] Tokenize(string str, string delimiters, bool meaningFlag = false, bool ignoreEmpty = false)
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

			return tokens.ToArray();
		}

		public static bool HasSameChar(string str)
		{
			for (int r = 1; r < str.Length; r++)
				for (int l = 0; l < r; l++)
					if (str[l] == str[r])
						return true;

			return false;
		}

		public static string ReplaceChars(string str, string rChrs, char wChr)
		{
			foreach (char rChr in rChrs)
				str = str.Replace(rChr, wChr);

			return str;
		}

		public static string ReplaceLoop(string str, string rPtn, string wPtn, int count = 30)
		{
			while (1 <= count)
			{
				str = str.Replace(rPtn, wPtn);
				count--;
			}
			return str;
		}

		public static string AntiNullOrEmpty(string str, string defval = "_")
		{
			return string.IsNullOrEmpty(str) ? defval : str;
		}

		public static string AntiNull(string str)
		{
			return str == null ? "" : str;
		}

		public static string SetCharAt(string str, int index, char chr)
		{
			return str.Substring(0, index) + chr + str.Substring(index + 1);
		}
	}
}
