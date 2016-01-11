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
	}
}
