using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HTable
	{
		private Header _header;
		private string[][] _rows;

		public HTable(Header header, string[][] rows)
		{
			_header = header;
			_rows = rows;
		}

		public HTable(string[] header, string[][] rows)
		{
			_header = new Header(header);
			_rows = rows;
		}

		public HTable(CsvFileReader reader)
		{
			_header = new Header(reader.nextRow());
			_rows = reader.readToEnd();
		}

		public int colcnt
		{
			get
			{
				return _header.colcnt;
			}
		}

		public int rowcnt
		{
			get
			{
				return _rows.Length;
			}
		}

		public class Header
		{
			private string[] _header;
			private Dictionary<string, int> _indexes;

			public Header(string[] header)
			{
				_header = header;
				_indexes = DictionaryTools.create<int>();

				for (int colidx = 0; colidx < header.Length; colidx++)
					_indexes.Add(header[colidx], colidx);
			}

			public int colcnt
			{
				get
				{
					return _header.Length;
				}
			}

			public int indexOf(string colTitle)
			{
				return _indexes[colTitle];
			}
		}

		public Header header
		{
			get
			{
				return _header;
			}
		}

		public class Row
		{
			private Header _header;
			private string[] _row;

			public Row(Header header, string[] row)
			{
				_header = header;
				_row = row;
			}

			public Row(string[] header, string[] row)
			{
				_header = new Header(header);
				_row = row;
			}

			public Header header
			{
				get
				{
					return _header;
				}
			}

			public int colcnt
			{
				get
				{
					return _header.colcnt;
				}
			}

			public string getCell(int colidx)
			{
				return _row[colidx];
			}

			public int indexOf(string colTitle)
			{
				return _header.indexOf(colTitle);
			}

			public string getCell(string colTitle)
			{
				return _row[indexOf(colTitle)];
			}

			public string this[int colidx]
			{
				get
				{
					return getCell(colidx);
				}
			}

			public string this[string colTitle]
			{
				get
				{
					return getCell(colTitle);
				}
			}
		}

		public Row getRow(int rowidx)
		{
			return new Row(_header, _rows[rowidx]);
		}

		public Row this[int rowidx]
		{
			get
			{
				return getRow(rowidx);
			}
		}
	}
}
