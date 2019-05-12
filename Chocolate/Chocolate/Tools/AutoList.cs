using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AutoList<T>
	{
		private T[] Buffer;

		public AutoList(int capacity = 0)
		{
			this.Buffer = new T[capacity];
		}

		public void EnsureCapacity(int capacity)
		{
			if (this.Buffer.Length < capacity)
			{
				T[] tmp = new T[capacity];

				Array.Copy(this.Buffer, tmp, this.Buffer.Length);

				this.Buffer = tmp;
			}
		}

		public T this[int index]
		{
			get
			{
#if true
				return index < this.Buffer.Length ? this.Buffer[index] : default(T);
#else
				this.EnsureCapacity(index + 1);
				return this.Buffer[index];
#endif
			}

			set
			{
				this.EnsureCapacity(index + 1);
				this.Buffer[index] = value;
			}
		}
	}
}
