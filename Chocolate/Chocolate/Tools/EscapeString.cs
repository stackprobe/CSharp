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

		private string AllowedChrs;
		private char EscapeChr;
		private string DisallowedChrs;

		public EscapeString(string allowedChrs, char escapeChr, string disallowedChrs)
		{
			if (
				allowedChrs == null ||
				disallowedChrs == null ||
				allowedChrs.Length != disallowedChrs.Length ||
				StringTools.HasSameChar(allowedChrs + escapeChr + disallowedChrs)
				)
				throw new ArgumentException();

			this.AllowedChrs = allowedChrs + escapeChr;
			this.EscapeChr = escapeChr;
			this.DisallowedChrs = disallowedChrs + escapeChr;
		}

		public string Encode(string str)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char chr in str)
			{
				int chrPos = this.AllowedChrs.IndexOf(chr);

				if (chrPos == -1)
				{
					buff.Append(chr);
				}
				else
				{
					buff.Append(this.EscapeChr);
					buff.Append(this.DisallowedChrs[chrPos]);
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
					int chrPos = this.DisallowedChrs.IndexOf(chr);

					if (chrPos != -1)
					{
						chr = this.AllowedChrs[chrPos];
					}
				}
				buff.Append(chr);
			}
			return buff.ToString();
		}
	}
}
