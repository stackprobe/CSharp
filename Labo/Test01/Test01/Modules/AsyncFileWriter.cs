using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Test01.Modules
{
	public class AsyncFileWriter : IDisposable
	{
		private FileStream Writer = null;

		public void OpenFile(string file)
		{
			this.CloseFile();

			this.Writer = new FileStream(file, FileMode.Create, FileAccess.Write);
		}

		public void CloseFile()
		{
			this.WaitToWrite();

			if (this.Writer != null)
			{
				this.Writer.Dispose();
				this.Writer = null;
			}
		}

		private void WaitToWrite()
		{
			if (this.Th != null)
			{
				this.Th.Join();
				this.Th = null;
			}
		}

		private Thread Th = null;
		private Exception Ex = null;

		public void Write(byte[] filePart)
		{
			this.WaitToWrite();

			this.Th = new Thread(() =>
			{
				try
				{
					this.Writer.Write(filePart, 0, filePart.Length);
				}
				catch (Exception e)
				{
					if (this.Ex == null) // 最初の例外を保持
						this.Ex = e;
				}
			});

			this.Th.Start();
		}

		public void RelayThrow()
		{
			this.WaitToWrite();

			if (this.Ex != null)
				throw new Exception("Relay", this.Ex);
		}

		public void Dispose()
		{
			this.CloseFile();
		}
	}
}
