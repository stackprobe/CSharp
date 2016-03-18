using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public abstract class FileSorter<Reader_t, Writer_t, Record_t>
		where Reader_t : class
		where Writer_t : class
		where Record_t : class
	{
		public void Perform(string rwFile)
		{
			this.Perform(rwFile, rwFile);
		}

		public void Perform(string rFile, string wFile)
		{
			Reader_t reader = this.ReadOpen(rFile);
			List<Record_t> records = null;
			long weight = 0L;
			long weightMax = this.GetWeightMax();
			Queue<string> midFiles = new Queue<string>();

			for (; ; )
			{
				Record_t record = this.ReadRecord(reader);

				if (record == null)
					break;

				if (records == null)
					records = new List<Record_t>();

				records.Add(record);
				weight += this.GetWeight(record);

				if (weightMax < weight)
				{
					midFiles.Enqueue(this.MakeMidFile(records));
					records = null;
					weight = 0L;
				}
			}
			if (records != null)
				midFiles.Enqueue(this.MakeMidFile(records));
		}

		private string MakeMidFile(List<Record_t> records)
		{
			string midFile = FileTools.MakeTempPath();

			records.Sort(this.Comp);

			Writer_t writer = this.WriteOpen(midFile);

			foreach (Record_t record in records)
				this.WriteRecord(writer, record);

			this.WriteClose(writer);
			return midFile;
		}

		public abstract Reader_t ReadOpen(string file);
		public abstract Record_t ReadRecord(Reader_t reader);
		public abstract void ReadClose(Reader_t reader);

		public abstract Writer_t WriteOpen(string file);
		public abstract void WriteRecord(Writer_t writer, Record_t record);
		public abstract void WriteClose(Writer_t writer);

		public abstract long GetWeight(Record_t record);
		public abstract long GetWeightMax();

		public abstract int Comp(Record_t a, Record_t b);

		public class TextFileSorter : FileSorter<StreamReader, StreamWriter, string>
		{
			private Encoding _encoding;

			public TextFileSorter(Encoding encoding)
			{
				_encoding = encoding;
			}

			public override StreamReader ReadOpen(string file)
			{
				return new StreamReader(file, _encoding);
			}

			public override string ReadRecord(StreamReader reader)
			{
				return reader.ReadLine();
			}

			public override void ReadClose(StreamReader reader)
			{
				reader.Dispose();
			}

			public override StreamWriter WriteOpen(string file)
			{
				return new StreamWriter(file, false,_encoding);
			}

			public override void WriteRecord(StreamWriter writer, string record)
			{
				writer.WriteLine(record);
			}

			public override void WriteClose(StreamWriter writer)
			{
				writer.Dispose();
			}

			public override long GetWeight(string record)
			{
				return 100 + record.Length * 2;
			}

			public override long GetWeightMax()
			{
				return 100000000; // 100 MB !!!
			}

			public override int Comp(string a, string b)
			{
				return string.Compare(a, b);
			}
		}
	}
}
