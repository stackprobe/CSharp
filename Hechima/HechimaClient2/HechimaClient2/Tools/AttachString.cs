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

		private char _delimiter;
		private EscapeString _es;

		public AttachString(char delimiter, char escapeChr, char escapeDelimiter, string decChrs, string encChrs)
			: this(delimiter, new EscapeString(decChrs + delimiter, escapeChr, encChrs + escapeDelimiter))
		{ }

		public AttachString(char delimiter, EscapeString es)
		{
			_delimiter = delimiter;
			_es = es;
		}

		public string untokenize(string[] tokens)
		{
			List<string> dest = new List<string>();

			foreach (string token in tokens)
			{
				dest.Add(_es.encode(token));
			}
			dest.Add(""); // token=0対策
			return string.Join("" + _delimiter, dest);
		}

		public string[] tokenize(string str)
		{
			List<string> dest = new List<string>();

			foreach (string token in StringTools.tokenize(str, "" + _delimiter))
			{
				dest.Add(_es.decode(token));
			}
			dest.RemoveAt(dest.Count - 1); // token=0対策
			return dest.ToArray();
		}
	}
}
