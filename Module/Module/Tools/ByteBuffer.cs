using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ByteBuffer
	{
		private List<Part> Parts = new List<Part>();
		private byte[] Buff = null;
		private int Index = -1;
		private int TotalSize = 0;
		private int BuffSize = 10;

		public int Length
		{
			get
			{
				return this.TotalSize;
			}
		}

		public void Clear()
		{
			this.Parts.Clear();
			this.Buff = null;
			this.TotalSize = 0;
		}

		public void Add(byte chr)
		{
			if (this.Buff == null)
			{
				this.Buff = new byte[this.BuffSize];
				this.Index = 0;
			}
			this.Buff[this.Index] = chr;
			this.Index++;

			if (this.BuffSize <= this.Index)
			{
				this.AddBuff();

				if (this.BuffSize < 1000000)
					this.BuffSize *= 2;
				else
					this.BuffSize = 2000000;
			}
			this.TotalSize++;
		}

		private void AddBuff()
		{
			this.Parts.Add(new Part()
			{
				Block = this.Buff,
				StartPos = 0,
				Size = this.Index,
			});
			this.Buff = null;
		}

		public void Add(byte[] block)
		{
			this.Add(block, 0, block.Length);
		}

		public void Add(byte[] block, int startPos, int size)
		{
			if (this.Buff != null)
			{
				this.AddBuff();

				if (3 < this.BuffSize)
					this.BuffSize /= 3;
				else
					this.BuffSize = 1;
			}
			this.Parts.Add(new Part()
			{
				Block = block,
				StartPos = startPos,
				Size = size,
			});
			this.TotalSize += size;
		}

		public byte[] Join()
		{
			byte[] dest = new byte[this.TotalSize];
			int wPos = 0;

			foreach (Part part in this.Parts)
			{
				Array.Copy(part.Block, part.StartPos, dest, wPos, part.Size);
				wPos += part.Size;
			}
			if (this.TotalSize != wPos) throw null; // 2bs
			return dest;
		}

		private class Part
		{
			public byte[] Block;
			public int StartPos;
			public int Size;
		}
	}
}
