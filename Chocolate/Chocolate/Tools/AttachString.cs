using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AttachString
	{
		public AttachString()
			: this("\t\r\n ", "trns")
		{ }

		public AttachString(string decChrs, string encChrs)
			: this(':', '$', '.', decChrs, encChrs)
		{ }

		public AttachString(char delimiter, char escapeChr, char escapeDelimiter)
			: this(delimiter, escapeChr, escapeDelimiter, "", "")
		{ }

		public AttachString(char delimiter, char escapeChr, char escapeDelimiter, string decChrs, string encChrs)
			: this(delimiter, new EscapeString(
				decChrs + delimiter,
				escapeChr,
				encChrs + escapeDelimiter
				))
		{ }

		private char Delimiter;
		private EscapeString ES;

		public AttachString(char delimiter, EscapeString es)
		{
			this.Delimiter = delimiter;
			this.ES = es;
		}

		public string Untokenize(IEnumerable<string> tokens)
		{
			List<string> dest = new List<string>();

			foreach (string token in tokens)
				dest.Add(this.ES.Encode(token));

			dest.Add("");
			return string.Join("" + this.Delimiter, dest);
		}

		public string[] Tokenize(string str)
		{
			List<string> dest = new List<string>();

			foreach (string token in StringTools.Tokenize(str, "" + this.Delimiter))
				dest.Add(this.ES.Decode(token));

			dest.RemoveAt(dest.Count - 1);
			return dest.ToArray();
		}
	}
}
