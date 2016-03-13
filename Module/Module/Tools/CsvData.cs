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
			_table.AddRow();

			for (; ; )
			{
				int chr = this.NextChar();

				if (chr == -1)
					break;

				StringBuilder buff = new StringBuilder();

				if (chr == '"')
				{
					for (; ; )
					{
						chr = this.NextChar();

						if (chr == -1)
							break;

						if (chr == '"')
						{
							chr = this.NextChar();

							if (chr != '"')
								break;
						}
						buff.Append((char)chr);
					}
				}
				else
				{
					for (; ; )
					{
						if (chr == _delimiter || chr == '\n')
							break;

						buff.Append((char)chr);
						chr = this.NextChar();

						if (chr == -1)
							break;
					}
				}
				_table.Add(buff.ToString());

				if (chr == '\n')
					_table.AddRow();
			}
			_text = null;
		}

		public AutoTable<string> Table
		{
			get
			{
				return _table;
			}
		}

		public string GetCell(int x, int y)
		{
			string cell = _table[x, y];

			if (StringTools.ContainsChar(cell, "\r\n\"" + _delimiter))
				cell = "\"" + cell.Replace("\"", "\"\"") + "\"";

			return cell;
		}

		public string GetLine(int y)
		{
			List<string> cells = new List<string>();

			for (int x = 0; x < _table.Width; x++)
				cells.Add(this.GetCell(x, y));

			return string.Join("" + _delimiter, cells);
		}

		public List<string> GetLines()
		{
			List<string> lines = new List<string>();

			for (int y = 0; y < _table.Height; y++)
				lines.Add(this.GetLine(y));

			return lines;
		}

		public string GetText()
		{
			return string.Join("\r\n", this.GetLines());
		}

		private void WriteFile(string csvFile, Encoding encoding)
		{
			File.WriteAllText(csvFile, this.GetText(), encoding);
		}

		public void WriteFile(string csvFile)
		{
			this.WriteFile(csvFile, StringTools.ENCODING_SJIS);
		}

		public void TT()
		{
			this.TrimAllCell();
			this.Trim();
		}

		public void TrimAllCell()
		{
			for (int x = 0; x < _table.Width; x++)
				for (int y = 0; y < _table.Height; y++)
					_table[x, y] = _table[x, y].Trim();
		}

		public void Trim()
		{
			this.TrimX();
			this.TrimY();
		}

		public void TrimX()
		{
			while (1 <= _table.Width)
			{
				for (int y = 0; y < _table.Height; y++)
					if (_table[_table.Width - 1, y] != "")
						return;

				_table.Width--;
			}
		}

		public void TrimY()
		{
			while (1 <= _table.Height)
			{
				for (int x = 0; x < _table.GetWidth(_table.Height - 1); x++)
					if (_table[x, _table.Height - 1] != "")
						return;

				_table.Height--;
			}
		}
	}
}
