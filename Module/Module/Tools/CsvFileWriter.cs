using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class CsvFileWriter : IDisposable
	{
		public CsvFileWriter(string file)
			: this(file, StringTools.ENCODING_SJIS)
		{ }

		private StreamWriter _writer;

		public CsvFileWriter(string file, Encoding encoding)
		{
			_writer = new StreamWriter(file, false, encoding);
		}

		private const char DELIMITER = ',';
		//private const char DELIMITER = '\t'; // TSL のとき

		public void writeCell(string cell)
		{
			if (
				cell.Contains('\n') ||
				cell.Contains('\"') ||
				cell.Contains(DELIMITER)
				)
			{
				_writer.Write('"');
				_writer.Write(cell.Replace("\"", "\"\""));
				_writer.Write('"');
			}
			else
				_writer.Write(cell);
		}

		public void writeCellDelimiter()
		{
			_writer.Write(DELIMITER);
		}

		public void writeNewLine()
		{
			_writer.Write('\n');
		}

		public void writeRow(string[] row)
		{
			for (int index = 0; index < row.Length; index++)
			{
				if (1 <= index)
					this.writeCellDelimiter();

				this.writeCell(row[index]);
			}
			this.writeNewLine();
		}

		public void Dispose()
		{
			_writer.Dispose();
			_writer = null;
		}
	}
}
