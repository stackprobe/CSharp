﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Satellite
{
	public class StringTools
	{
		public const string HEXADECIMAL = "0123456789ABCDEF";
		public const string hexadecimal = "0123456789abcdef";

		public static string ToHex(byte[] data)
		{
			StringBuilder buff = new StringBuilder();

			foreach (byte chr in data)
			{
				buff.Append(hexadecimal[chr >> 4]);
				buff.Append(hexadecimal[chr & 0x0f]);
			}
			return buff.ToString();
		}

		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public static string Format(params object[] prms)
		{
			string str = "$";

			foreach (object prm in prms)
			{
				int d = str.IndexOf('$');

				if (d == -1)
					break;

				str = str.Substring(0, d) + prm + str.Substring(d + 1);
			}
			return str;
		}

		public static string ZPad(int value, int minlen)
		{
			return ZPad("" + value, minlen);
		}

		public static string ZPad(string str, int minlen)
		{
			return LPad(str, '0', minlen);
		}

		public static string LPad(string str, char pad, int minlen)
		{
			while (str.Length < minlen)
			{
				str = pad + str;
			}
			return str;
		}

		public static string GetUUID()
		{
			return Guid.NewGuid().ToString("B");
		}

		public static bool EqualsIgnoreCase(string s1, string s2)
		{
			return s1.ToLower() == s2.ToLower();
		}
	}
}
