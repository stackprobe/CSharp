using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Utils
	{
		public class SyncBuffer<T>
		{
			private readonly object SYNCROOT = new object();
			private Queue<T> Buff = new Queue<T>();
			private int MaxSize;

			public SyncBuffer(int maxSize = 100)
			{
				if (maxSize < 1)
					throw new ArgumentException();

				this.MaxSize = maxSize;
			}

			public void Enqueue(T value)
			{
				lock (SYNCROOT)
				{
					if (this.MaxSize <= this.Buff.Count)
						this.Buff.Dequeue();

					this.Buff.Enqueue(value);
				}
			}

			public T[] Dequeue()
			{
				List<T> dest = new List<T>();

				lock (SYNCROOT)
				{
					while (1 <= this.Buff.Count)
						dest.Add(this.Buff.Dequeue());
				}
				return dest.ToArray();
			}
		}

		public class PeriodicPerform
		{
			private long Count;
			private int Period;
			private Action Perform;

			public PeriodicPerform(int period, Action perform)
			{
				this.Period = period;
				this.Perform = perform;
			}

			public void Kick()
			{
				if (this.Count++ % this.Period == 0)
					this.Perform();
			}
		}
	}
}
