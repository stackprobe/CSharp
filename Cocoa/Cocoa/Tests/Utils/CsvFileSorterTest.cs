using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Utils;

namespace Charlotte.Tests.Utils
{
	public class CsvFileSorterTest
	{
		private static string TMP_CSV_FILE = "C:/temp/Test01.csv";

		private class Test01_Sorter : CsvFileSorter
		{
			public Test01_Sorter()
				: base(TMP_CSV_FILE)
			{ }

			protected override int GetWeight(string[] row)
			{
				return 10000;
			}

			protected override int Capacity()
			{
				return 10000 * 30;
			}
		}

		public void Test01()
		{
			string[][] rows = new string[1000][];

			for (int index = 0; index < rows.Length; index++)
				rows[index] = new string[] { "" + index, "_" + index, "$$$" + index };

			SecurityTools.CRandom.Shuffle(rows.ToArray());

			using (CsvFileWriter writer = new CsvFileWriter(TMP_CSV_FILE))
			{
				writer.WriteRows(rows);
			}
			rows = null;

			using (CsvFileSorter sorter = new Test01_Sorter())
			{
				sorter.Sort((a, b) => int.Parse(a[0]) - int.Parse(b[0]));
			}
			using (CsvFileReader reader = new CsvFileReader(TMP_CSV_FILE))
			{
				rows = reader.ReadToEnd();
			}
			for (int index = 0; index < rows.Length; index++)
			{
				string[] row = rows[index];

				if (row[0] != ("" + index))
					throw null; // bugged !!!

				if (row[1] != ("_" + index))
					throw null; // bugged !!!

				if (row[2] != ("$$$" + index))
					throw null; // bugged !!!
			}
			Console.WriteLine("OK!");
		}
	}
}
