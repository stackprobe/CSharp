using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class EscapeString
	{
		public EscapeString()
			: this("\t\r\n ", '$', "trns")
		{ }

		private string _decChrs;
		private char _escapeChr;
		private string _encChrs;

		public EscapeString(string decChrs, char escapeChr, string encChrs)
		{
			if (
				decChrs == null ||
				encChrs == null ||
				decChrs.Length != encChrs.Length ||
				StringTools.hasSameChar(decChrs + escapeChr + encChrs)
				)
				throw new ArgumentException();

			_decChrs = decChrs + escapeChr;
			_escapeChr = escapeChr;
			_encChrs = encChrs + escapeChr;
		}

		public char getEscapeChr()
		{
			return _escapeChr;
		}

		public string encode(string str)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				int chrPos = _decChrs.IndexOf(chr);

				if (chrPos == -1)
				{
					buff.Append(chr);
				}
				else
				{
					buff.Append(_escapeChr);
					buff.Append(_encChrs[chrPos]);
				}
			}
			return buff.ToString();
		}

		public string decode(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (chr == _escapeChr && index + 1 < str.Length)
				{
					index++;
					chr = str[index];
					int chrPos = _encChrs.IndexOf(chr);

					if (chrPos != -1)
						chr = _decChrs[chrPos];
				}
				buff.Append(chr);
			}
			return buff.ToString();
		}
	}
}
