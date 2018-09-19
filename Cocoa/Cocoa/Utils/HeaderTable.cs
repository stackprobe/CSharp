using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Utils
{
	public class HeaderTable
	{
		public string[] Header;
		public string[][] Rows;

		public HeaderTable(string csvFile)
		{
			using (CsvFileReader reader = new CsvFileReader(csvFile))
			{
				Header = reader.ReadRow();
				Rows = reader.ReadToEnd();
			}
		}

		public int GetColumnIndex(Func<string, bool> predicate)
		{
			return ArrayTools.IndexOf(Header, predicate);
		}

		public int GetColumnIndex(string trgColName)
		{
			return this.GetColumnIndex(colName => colName == trgColName);
		}

		public HeaderRow this[int rowidx]
		{
			get
			{
				return new HeaderRow(Header, Rows[rowidx]);
			}
		}

		public string this[int rowidx, int colidx]
		{
			get
			{
				return Rows[rowidx][colidx];
			}
		}

		public string this[int rowidx, string colName]
		{
			get
			{
				return Rows[rowidx][GetColumnIndex(colName)];
			}
		}

		// ----

		public string[] GetColumn(int colidx)
		{
			List<string> dest = new List<string>();

			for (int rowidx = 0; rowidx < Rows.Length; rowidx++)
			{
				dest.Add(Rows[rowidx][colidx]);
			}
			return dest.ToArray();
		}

		public string[] GetColumn(string colName)
		{
			return GetColumn(GetColumnIndex(colName));
		}
	}
}
