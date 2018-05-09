using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SyncQueue<T>
	{
		private object SYNCROOT = new object();
		private Queue<T> Inner = new Queue<T>();

		public void Enqueue(T element)
		{
			lock (SYNCROOT)
			{
				this.Inner.Enqueue(element);
			}
		}

		public T[] Dequeue(int count = 1)
		{
			List<T> dest = new List<T>();

			lock (SYNCROOT)
			{
				for (int index = 0; index < count && 1 <= this.Inner.Count; index++)
					dest.Add(this.Inner.Dequeue());
			}
			return dest.ToArray();
		}
	}
}
