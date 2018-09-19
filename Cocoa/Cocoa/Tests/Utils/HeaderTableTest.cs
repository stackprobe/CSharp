using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Utils;

namespace Charlotte.Tests.Utils
{
	public class HeaderTableTest
	{
		public void Test01()
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string csvFile = wd.MakePath();

				using (CsvFileWriter writer = new CsvFileWriter(csvFile))
				{
					writer.WriteCell("A");
					writer.WriteCell("B");
					writer.WriteCell("C");
					writer.EndRow();

					writer.WriteCell("1");
					writer.WriteCell("2");
					writer.WriteCell("3");
					writer.EndRow();

					writer.WriteCell("4");
					writer.WriteCell("5");
					writer.WriteCell("6");
					writer.EndRow();

					writer.WriteCell("7");
					writer.WriteCell("8");
					writer.WriteCell("9");
					writer.EndRow();
				}
				HeaderTable ht = new HeaderTable(csvFile);

				string[] column = ht.GetColumn("B");

				Console.WriteLine(string.Join(", ", column)); // expect: 2, 5, 8
			}
		}
	}
}
