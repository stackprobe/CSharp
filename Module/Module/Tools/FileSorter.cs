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
		public void MergeSort(string rwFile)
		{
			this.MergeSort(rwFile, rwFile);
		}

		public void MergeSort(string rFile, string wFile)
		{
			using (new FileStream(rFile, FileMode.Open, FileAccess.Read)) // read check !
			{ }

			Queue<string> divFiles = this.MakeDivFiles(rFile);

			while (2 < divFiles.Count)
			{
				string divFile1 = divFiles.Dequeue();
				string divFile2 = divFiles.Dequeue();
				string divFile3 = FileTools.MakeTempPath();

				this.MergeFile(divFile1, divFile2, divFile3);

				divFiles.Enqueue(divFile3);
			}

			using (new FileStream(wFile, FileMode.Create, FileAccess.Write)) // write check !
			{ }

			switch (divFiles.Count)
			{
				case 2:
					this.MergeFile(divFiles.Dequeue(), divFiles.Dequeue(), wFile);
					break;

				case 1:
					this.FlowFile(divFiles.Dequeue(), wFile);
					break;

				case 0:
					this.WriteClose(this.WriteOpen(wFile));
					break;

				default:
					throw null;
			}
		}

		private Queue<string> MakeDivFiles(string rFile)
		{
			Reader_t reader = this.ReadOpen(rFile);
			List<Record_t> records = new List<Record_t>();
			long weight = 0L;
			long weightMax = this.GetWeightMax();
			Queue<string> divFiles = new Queue<string>();

			for (; ; )
			{
				Record_t record = this.ReadRecord(reader);

				if (record == null)
				{
					break;
				}
				records.Add(record);
				weight += this.GetWeight(record);

				if (weightMax < weight)
				{
					divFiles.Enqueue(this.MakeDivFile(records));
					records.Clear();
					weight = 0L;
				}
			}
			this.ReadClose(reader);

			if (1 <= records.Count)
			{
				divFiles.Enqueue(this.MakeDivFile(records));
			}
			return divFiles;
		}

		private string MakeDivFile(List<Record_t> records)
		{
			string wFile = FileTools.MakeTempPath();

			records.Sort(this.Comp);

			Writer_t writer = this.WriteOpen(wFile);

			foreach (Record_t record in records)
			{
				this.WriteRecord(writer, record);
			}
			this.WriteClose(writer);
			return wFile;
		}

		private void MergeFile(string rFile1, string rFile2, string wFile)
		{
			Reader_t reader1 = this.ReadOpen(rFile1);
			Reader_t reader2 = this.ReadOpen(rFile2);
			Writer_t writer = this.WriteOpen(wFile);
			Record_t record1 = this.ReadRecord(reader1);
			Record_t record2 = this.ReadRecord(reader2);

			for (; ; )
			{
				int ret;

				if (record1 == null)
				{
					if (record2 == null)
					{
						break;
					}
					ret = 1;
				}
				else if (record2 == null)
				{
					ret = -1;
				}
				else
				{
					ret = this.Comp(record1, record2);
				}
				if (ret < 0)
				{
					this.WriteRecord(writer, record1);
					record1 = this.ReadRecord(reader1);
				}
				else if (0 < ret)
				{
					this.WriteRecord(writer, record2);
					record2 = this.ReadRecord(reader2);
				}
				else
				{
					this.WriteRecord(writer, record1);
					this.WriteRecord(writer, record2);
					record1 = this.ReadRecord(reader1);
					record2 = this.ReadRecord(reader2);
				}
			}
			this.ReadClose(reader1);
			this.ReadClose(reader2);
			this.WriteClose(writer);

			File.Delete(rFile1);
			File.Delete(rFile2);
		}

		private void FlowFile(string rFile, string wFile)
		{
			File.Delete(wFile);
			File.Move(rFile, wFile);
		}

		protected abstract Reader_t ReadOpen(string file);
		protected abstract Record_t ReadRecord(Reader_t reader);
		protected abstract void ReadClose(Reader_t reader);

		protected abstract Writer_t WriteOpen(string file);
		protected abstract void WriteRecord(Writer_t writer, Record_t record);
		protected abstract void WriteClose(Writer_t writer);

		protected abstract long GetWeight(Record_t record);
		protected abstract long GetWeightMax();

		protected abstract int Comp(Record_t a, Record_t b);
	}
}
