using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HTable
	{
		private string[] _header;
		private string[][] _rows;

		public HTable(string[] header, string[][] rows)
		{
			_header = header;
			_rows = rows;
		}

		public HTable(CsvFileReader reader)
		{
			_header = reader.nextRow();
			_rows = reader.readToEnd();
		}

		public int rowcnt
		{
			get
			{
				return _rows.Length;
			}
		}

		public int colcnt
		{
			get
			{
				return _header.Length;
			}
		}

		public class Row
		{
			private string[] _header;
			private string[] _row;

			public Row(string[] header, string[] row)
			{
				_header = header;
				_row = row;
			}

			public int colcnt
			{
				get
				{
					return _header.Length;
				}
			}

			public int colTitleToColIndex(string colTitle)
			{
				for (int colidx = 0; colidx < _header.Length; colidx++)
					if (colTitle == _header[colidx])
						return colidx;

				return -1;
			}

			public string this[int colidx]
			{
				get
				{
					return _row[colidx];
				}
			}

			public string this[string colTitle]
			{
				get
				{
					return _row[this.colTitleToColIndex(colTitle)];
				}
			}

			public string[] header
			{
				get
				{
					return _header;
				}
			}

			public string[] row
			{
				get
				{
					return _row;
				}
			}
		}

		public Row this[int rowidx]
		{
			get
			{
				return new Row(_header, _rows[rowidx]);
			}
		}

		public void sort(Comparison<Row> comp)
		{
			ArrayTools.sort<string[]>(_rows, delegate(string[] a, string[] b)
			{
				return comp(new Row(_header, a), new Row(_header, b));
			});
		}

		public class Reader
		{
			private HTable _table;

			public Reader(HTable table)
			{
				_table = table;
			}

			private int _rowidx = 0;

			public Row nextRow()
			{
				if (_rowidx < _table.rowcnt)
				{
					return _table[_rowidx++];
				}
				return null;
			}

			public int nextRowIndex
			{
				get
				{
					return _rowidx;
				}
			}
		}

		public string[] header
		{
			get
			{
				return _header;
			}
		}

		public string[][] rows
		{
			get
			{
				return _rows;
			}
		}
	}
}
