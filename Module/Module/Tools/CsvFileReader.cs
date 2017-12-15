using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CsvFileReader : IDisposable
	{
		public CsvFileReader(string file)
			: this(file, StringTools.ENCODING_SJIS)
		{ }

		private StreamReader _reader;

		public CsvFileReader(string file, Encoding encoding)
		{
			_reader = new StreamReader(file, encoding);
		}

		private const char DELIMITER = ',';
		//private const char DELIMITER = '\t'; // TSL のとき

		private int _chr;

		private int read()
		{
			do
			{
				_chr = _reader.Read();
			}
			while (_chr == '\r');

			return _chr;
		}

		private bool _enclosedCell;

		private string nextCell()
		{
			StringBuilder buff = new StringBuilder();

			if (this.read() == '"')
			{
				this.read();

				while (_chr != -1 && (_chr != '"' || this.read() == '"'))
				{
					buff.Append((char)_chr);
					this.read();
				}
				_enclosedCell = true;
			}
			else
			{
				while (_chr != -1 && _chr != '\n' && _chr != DELIMITER)
				{
					buff.Append((char)_chr);
					this.read();
				}
				_enclosedCell = false;
			}
			return buff.ToString();
		}

		public string[] nextRow()
		{
			List<string> row = new List<string>();

			do
			{
				row.Add(this.nextCell());
			}
			while (_chr != -1 && _chr != '\n');

			if (_chr == -1 && row.Count == 1 && row[0] == "" && _enclosedCell == false)
				return null;

			return row.ToArray();
		}

		public string[][] readToEnd()
		{
			List<string[]> rows = new List<string[]>();

			for (; ; )
			{
				string[] row = nextRow();

				if (row == null)
					break;

				rows.Add(row);
			}
			return rows.ToArray();
		}

		public void Dispose()
		{
			_reader.Dispose();
			_reader = null;
		}
	}
}
