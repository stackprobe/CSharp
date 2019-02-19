﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class HTTPBodyOutputStream : IDisposable
	{
		private WorkingDir WD = null;
		private string BuffFile = null;
		private int WroteSize = 0;

		private string GetBuffFile()
		{
			if (this.WD == null)
			{
				this.WD = new WorkingDir();
				this.BuffFile = this.WD.MakePath();
			}
			return this.BuffFile;
		}

		public void Write(byte[] data, int offset = 0)
		{
			this.Write(data, offset, data.Length - offset);
		}

		public void Write(byte[] data, int offset, int count)
		{
			using (FileStream writer = new FileStream(this.GetBuffFile(), FileMode.Append, FileAccess.Write))
			{
				writer.Write(data, offset, count);
			}
			this.WroteSize += count;
		}

		public int Count
		{
			get
			{
				return this.WroteSize;
			}
		}

		public byte[] ToByteArray()
		{
			return this.WroteSize == 0 ? BinTools.EMPTY : File.ReadAllBytes(this.GetBuffFile());
		}

		public void Dispose()
		{
			if (this.WD != null)
			{
				this.WD.Dispose();
				this.WD = null;
			}
		}
	}
}
