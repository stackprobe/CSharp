using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class CsvFileReader_WriterTest
	{
		public void test01()
		{
			test01(@"C:\var\test.csv", @"C:\temp\1.csv");
		}

		public void test01(string rFile, string wFile)
		{
			using (CsvFileReader reader = new CsvFileReader(rFile))
			using (CsvFileWriter writer = new CsvFileWriter(wFile))
			{
				for (; ; )
				{
					string[] row = reader.nextRow();

					if (row == null)
						break;

					writer.writeRow(row);
				}
			}
		}
	}
}
