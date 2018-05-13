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

		private string DecChrs;
		private char EscapeChr;
		private string EncChrs;

		public EscapeString(string decChrs, char escapeChr, string encChrs)
		{
			if (
				decChrs == null ||
				encChrs == null ||
				decChrs.Length != encChrs.Length ||
				StringTools.HasSameChar(decChrs + escapeChr + encChrs)
				)
				throw new ArgumentException();

			this.DecChrs = decChrs + escapeChr;
			this.EscapeChr = escapeChr;
			this.EncChrs = encChrs + escapeChr;
		}

		public string Encode(string str)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				int chrPos = this.DecChrs.IndexOf(chr);

				if (chrPos == -1)
				{
					buff.Append(chr);
				}
				else
				{
					buff.Append(this.EscapeChr);
					buff.Append(this.EncChrs[chrPos]);
				}
			}
			return buff.ToString();
		}

		public string Decode(string str)
		{
			StringBuilder buff = new StringBuilder();

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (chr == this.EscapeChr && index + 1 < str.Length)
				{
					index++;
					chr = str[index];
					int chrPos = this.EncChrs.IndexOf(chr);

					if (chrPos != -1)
					{
						chr = this.DecChrs[chrPos];
					}
				}
				buff.Append(chr);
			}
			return buff.ToString();
		}
	}
}
