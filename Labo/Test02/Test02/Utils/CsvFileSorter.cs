using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Utils
{
	public class CsvFileSorter : HugeSorter<string[]>, IDisposable
	{
		private string RFile;
		private string WFile;
		private WorkingDir WD;
		private CsvFileReader Reader = null;
		private CsvFileWriter Writer = null;

		public CsvFileSorter(string rwFile)
			: this(rwFile, rwFile)
		{ }

		public CsvFileSorter(string rFile, string wFile)
		{
			this.RFile = rFile;
			this.WFile = wFile;
			this.WD = new WorkingDir();
		}

		protected override void BeforeFirstRead()
		{
			this.Reader = new CsvFileReader(this.RFile);
		}

		protected override string[] Read()
		{
			return this.Reader.ReadRow();
		}

		protected override void AfterLastRead()
		{
			this.Reader.Dispose();
			this.Reader = null;
		}

		protected override void BeforeFirstWrite()
		{
			this.Writer = new CsvFileWriter(this.WFile);
		}

		protected override void Write(string[] row)
		{
			this.Writer.WriteRow(row);
		}

		protected override void AfterLastWrite()
		{
			this.Writer.Dispose();
			this.Writer = null;
		}

		protected override void Copy(IPart part)
		{
			File.Copy(((Part)part).PartFile, this.WFile, true);
		}

		protected override int GetWeight(string[] row)
		{
			int weight = 10;

			foreach (string cell in row)
				weight += cell.Length + 10;

			return weight;
		}

		protected override int Capacity()
		{
			return 300000000; // 300 M char
		}

		protected override IPart CreatePart()
		{
			return new Part()
			{
				PartFile = this.WD.MakePath(),
			};
		}

		private class Part : IPart
		{
			public string PartFile;

			private CsvFileWriter Writer;
			private CsvFileReader Reader;

			public void BeforeFirstWrite()
			{
				this.Writer = new CsvFileWriter(this.PartFile);
			}

			public void Write(string[] row)
			{
				this.Writer.WriteRow(row);
			}

			public void AfterLastWrite()
			{
				this.Writer.Dispose();
				this.Writer = null;
			}

			public void BeforeFirstRead()
			{
				this.Reader = new CsvFileReader(this.PartFile);
			}

			public string[] Read()
			{
				return this.Reader.ReadRow();
			}

			public void AfterLastRead()
			{
				this.Reader.Dispose();
				this.Reader = null;
			}

			public void Dispose()
			{
				ExceptionDam.Section(eDam =>
				{
					if (this.Reader != null)
					{
						eDam.Invoke(this.Reader.Dispose);
						this.Reader = null;
					}
					if (this.Writer != null)
					{
						eDam.Invoke(this.Writer.Dispose);
						this.Writer = null;
					}
					eDam.Invoke(() => FileTools.Delete(this.PartFile));
				});
			}
		}

		public void Dispose()
		{
			ExceptionDam.Section(eDam =>
			{
				if (this.Reader != null)
				{
					eDam.Invoke(this.Reader.Dispose);
					this.Reader = null;
				}
				if (this.Writer != null)
				{
					eDam.Invoke(this.Writer.Dispose);
					this.Writer = null;
				}
				if (this.WD != null)
				{
					eDam.Invoke(this.WD.Dispose);
					this.WD = null;
				}
			});
		}
	}
}
