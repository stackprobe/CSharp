using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class CsvData
	{
		private AutoTable<string> _table = new AutoTable<string>("");
		private char _delimiter;

		public CsvData()
			: this(',')
		{ }

		public static CsvData CreateTsv()
		{
			return new CsvData('\t');
		}

		public CsvData(char delimiter)
		{
			_delimiter = delimiter;
		}

		public void Clear()
		{
			_table.Clear();
		}

		public void ReadFile(string csvFile)
		{
			this.ReadFile(csvFile, StringTools.ENCODING_SJIS);
		}

		public void ReadFile(string csvFile, Encoding encoding)
		{
			this.ReadText(File.ReadAllText(csvFile, encoding));
		}

		private string _text;
		private int _rPos;

		private int NextChar()
		{
			char chr;

			do
			{
				if (_text.Length <= _rPos)
					return -1;

				chr = _text[_rPos];
				_rPos++;
			}
			while (chr == '\r');

			return chr;
		}

		public void ReadText(string text)
		{
			_text = text;
			_rPos = 0;

			_table.Clear();

			for (; ; )
			{
				_table.AddRow();

				// TODO
			}
			_text = null;
		}

		// TODO
	}
}
