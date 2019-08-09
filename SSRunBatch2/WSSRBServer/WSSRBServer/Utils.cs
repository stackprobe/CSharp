using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Utils
	{
		public static void PostMessage(object message)
		{
			StringMessages.Enqueue("[" + DateTime.Now + "] " + message);
		}

		public static SyncLimitedQueue<string> StringMessages = new SyncLimitedQueue<string>();

		public class SyncLimitedQueue<T>
		{
			private readonly object SYNCROOT = new object();
			private Queue<T> Buff = new Queue<T>();
			private int MaxSize;

			public SyncLimitedQueue(int maxSize = 100)
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

			public T[] DequeueAll()
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
			private Action Action;

			public PeriodicPerform(int period, Action action)
			{
				this.Period = period;
				this.Action = action;
			}

			public void Perform()
			{
				if (this.Count++ % this.Period == 0)
					this.Action();
			}
		}
	}
}
