using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class HugeQueue : IDisposable
	{
		public long FileSizeLimit = 100000000L; // 100 MB, 0L <= であること。*1

		private WorkingDir WD;
		private string RFile;
		private string WFile;
		private FileStream Reader;
		private FileStream Writer;
		private Queue<string> MidFiles = new Queue<string>();
		private long Count = 0L;

		public HugeQueue()
		{
			HandleDam.Section(hDam =>
			{
				this.WD = hDam.Add(new WorkingDir());
				this.RFile = this.WD.MakePath();
				this.WFile = this.WD.MakePath();

				File.WriteAllBytes(this.RFile, BinTools.EMPTY);

				this.Reader = hDam.Add(new FileStream(this.RFile, FileMode.Open, FileAccess.Read));
				this.Writer = hDam.Add(new FileStream(this.WFile, FileMode.Create, FileAccess.Write));
			});
		}

		public long GetCount()
		{
			return this.Count;
		}

		public bool HasElements()
		{
			return this.Count != 0L;
		}

		public void Enqueue(byte[] value)
		{
			if (this.FileSizeLimit < this.Writer.Position) // 少しでも MidFiles に追加しないようにするため、判定は先にしたい。
			{
				// *1 ... FileSizeLimit < 0L のとき、ここで空のファイルを MidFiles に追加してしまう。

				this.Writer.Dispose();

				this.MidFiles.Enqueue(this.WFile);
				this.WFile = this.WD.MakePath();

				this.Writer = new FileStream(this.WFile, FileMode.Create, FileAccess.Write);
			}
			FileTools.Write(this.Writer, BinTools.ToBytes(value.Length));
			FileTools.Write(this.Writer, value);
			this.Count++;
		}

		public byte[] Dequeue()
		{
			if (this.Count == 0L)
				throw new Exception("空のキューから読み込もうとしました。");

			this.Count--;

			byte[] bSize = new byte[4];
			int readSize = this.Reader.Read(bSize, 0, 4);

			if (readSize == 0)
			{
				if (1 <= this.MidFiles.Count)
				{
					this.Reader.Dispose();

					FileTools.Delete(this.RFile);
					this.RFile = this.MidFiles.Dequeue();

					this.Reader = new FileStream(this.RFile, FileMode.Open, FileAccess.Read);
				}
				else
				{
					this.Reader.Dispose();
					this.Writer.Dispose();

					{
						string tmp = this.RFile;
						this.RFile = this.WFile;
						this.WFile = tmp;
					}

					this.Reader = new FileStream(this.RFile, FileMode.Open, FileAccess.Read);
					this.Writer = new FileStream(this.WFile, FileMode.Create, FileAccess.Write);
				}
				readSize = this.Reader.Read(bSize, 0, 4);
			}
			if (readSize != 4)
				throw new Exception("不正なサイズの読み込みサイズ：" + readSize);

			int size = BinTools.ToInt(bSize);

			if (size < 0 || IntTools.IMAX < size)
				throw new Exception("不正なサイズ：" + size);

			byte[] value = new byte[size];
			readSize = this.Reader.Read(value, 0, size);

			if (readSize != size)
				throw new Exception("不正なデータの読み込みサイズ：" + readSize + ", " + size);

			return value;
		}

		public void Dispose()
		{
			if (this.WD != null)
			{
				ExceptionDam.Section(eDam =>
				{
					eDam.Invoke(() => this.Reader.Dispose());
					eDam.Invoke(() => this.Writer.Dispose());

					eDam.Invoke(() => this.WD.Dispose());
					this.WD = null;
				});
			}
		}
	}
}
