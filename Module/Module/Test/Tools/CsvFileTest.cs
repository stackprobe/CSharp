using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Test.Tools
{
	public class CsvFileTest
	{
		public void test01()
		{
			test01(@"C:\var\test.csv", @"C:\temp\1.csv");
		}

		public void test01(string rFile, string wFile)
		{
			List<List<string>> rows;

			using (CsvFile.Reader reader = new CsvFile.Reader(rFile))
			{
				rows = reader.readRows();
			}
			using (CsvFile.Writer writer = new CsvFile.Writer(wFile))
			{
				writer.writeRows(rows);
			}
		}
	}
}
