using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SyncQueue<T>
	{
		private object SyncRoot = new object();
		private Queue<T> Inner = new Queue<T>();

		public void Enqueue(T element)
		{
			lock (this.SyncRoot)
			{
				this.Inner.Enqueue(element);
			}
		}

		public T[] Dequeue(int count = 1)
		{
			List<T> dest = new List<T>();

			lock (this.SyncRoot)
			{
				while (dest.Count < count && 1 <= this.Inner.Count)
					dest.Add(this.Inner.Dequeue());
			}
			return dest.ToArray();
		}

		public int Count
		{
			get
			{
				lock (this.SyncRoot)
				{
					return this.Inner.Count;
				}
			}
		}

		public void Rotate(Func<T, bool> peeker, int count = 1)
		{
			foreach (T element in this.Dequeue(count))
			{
				if (peeker(element))
				{
					this.Enqueue(element);
				}
			}
		}
	}
}
