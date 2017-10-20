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

		private StreamReader Reader;

		public CsvFileReader(string file, Encoding encoding)
		{
			this.Reader = new StreamReader(file, encoding);
		}

		private const char DELIMITER = ',';
		//private const char DELIMITER = '\t'; // TSL のとき

		private int Chr;

		private int Read()
		{
			do
			{
				this.Chr = this.Reader.Read();
			}
			while (this.Chr == '\r');

			return this.Chr;
		}

		private bool EnclosedCell;

		private string NextCell()
		{
			StringBuilder buff = new StringBuilder();

			if (this.Read() == '"')
			{
				this.EnclosedCell = true;
				this.Read();

				while (this.Chr != -1 && (this.Chr != '"' || this.Read() == '"'))
				{
					buff.Append((char)this.Chr);
					this.Read();
				}
			}
			else
			{
				this.EnclosedCell = false;

				while (this.Chr != -1 && this.Chr != '\n' && this.Chr != DELIMITER)
				{
					buff.Append((char)this.Chr);
					this.Read();
				}
			}
			return buff.ToString();
		}

		public string[] NextRow()
		{
			List<string> row = new List<string>();

			do
			{
				row.Add(this.NextCell());
			}
			while (this.Chr != -1 && this.Chr != '\n');

			if (this.Chr == -1 && row.Count == 1 && row[0] == "" && this.EnclosedCell == false)
				return null;

			return row.ToArray();
		}

		public void Dispose()
		{
			this.Reader.Dispose();
			this.Reader = null;
		}
	}
}
