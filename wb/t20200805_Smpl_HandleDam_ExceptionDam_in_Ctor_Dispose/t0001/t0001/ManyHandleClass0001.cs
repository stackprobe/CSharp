using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class ManyHandleClass0001 : IDisposable
	{
		private WorkingDir WD;
		private string File01;
		private string File02;
		private string File03;
		private StreamWriter Writer01;
		private StreamWriter Writer02;
		private StreamWriter Writer03;
		private string CsvFile01;
		private string CsvFile02;
		private string CsvFile03;
		private CsvFileWriter CsvWriter01;
		private CsvFileWriter CsvWriter02;
		private CsvFileWriter CsvWriter03;

		public ManyHandleClass0001()
		{
			HandleDam.Transaction(hDam =>
			{
				this.WD = hDam.Add(new WorkingDir());
				this.File01 = this.WD.GetPath("0001.txt");
				this.File02 = this.WD.GetPath("0002.txt");
				this.File03 = this.WD.GetPath("0003.txt");
				this.Writer01 = hDam.Add(new StreamWriter(this.File01, false, Encoding.ASCII));
				this.Writer02 = hDam.Add(new StreamWriter(this.File02, false, Encoding.ASCII));
				this.Writer03 = hDam.Add(new StreamWriter(this.File03, false, Encoding.ASCII));
				this.CsvFile01 = this.WD.GetPath("0001.csv");
				this.CsvFile02 = this.WD.GetPath("0002.csv");
				this.CsvFile03 = this.WD.GetPath("0003.csv");
				this.CsvWriter01 = hDam.Add(new CsvFileWriter(this.CsvFile01));
				this.CsvWriter02 = hDam.Add(new CsvFileWriter(this.CsvFile02));
				this.CsvWriter03 = hDam.Add(new CsvFileWriter(this.CsvFile03));
			});
		}

		public void Write01()
		{
			this.Writer01.WriteLine("123");
			this.Writer01.WriteLine("ABC");
			this.Writer01.WriteLine("abc");

			this.CsvWriter01.WriteRow(new string[] { "123", "ABC", "abc" });
			this.CsvWriter02.WriteRow(new string[] { "456", "DEF", "def" });
			this.CsvWriter03.WriteRow(new string[] { "789", "GHI", "ghi" });
		}

		private LimitCounter DisposeOnce = LimitCounter.One();

		public void Dispose()
		{
			if (this.DisposeOnce.Issue())
			{
				ExceptionDam.Section(eDam =>
				{
					eDam.Dispose(ref this.Writer01);
					eDam.Dispose(ref this.Writer02);
					eDam.Dispose(ref this.Writer03);
					eDam.Dispose(ref this.CsvWriter01);
					eDam.Dispose(ref this.CsvWriter02);
					eDam.Dispose(ref this.CsvWriter03);
					eDam.Dispose(ref this.WD);
				});
			}
		}
	}
}
