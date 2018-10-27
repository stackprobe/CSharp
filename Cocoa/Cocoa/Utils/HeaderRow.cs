using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Utils
{
	public class HeaderRow
	{
		public string[] Header;
		public string[] Row;

		public HeaderRow(string[] header, string[] row)
		{
			Header = header;
			Row = row;
		}

		public int GetColumnIndex(Predicate<string> match)
		{
			return ArrayTools.IndexOf(Header, match);
		}

		public int GetColumnIndex(string trgColName)
		{
			return this.GetColumnIndex(colName => colName == trgColName);
		}

		public string this[int colidx]
		{
			get
			{
				return Row[colidx];
			}
		}

		public string this[string colName]
		{
			get
			{
				return Row[GetColumnIndex(colName)];
			}
		}
	}
}
