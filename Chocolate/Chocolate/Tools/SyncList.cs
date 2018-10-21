using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SyncList<T>
	{
		private object SyncRoot = new object();
		private List<T> Inner = new List<T>();

		public void Clear()
		{
			lock (this.SyncRoot)
			{
				this.Inner.Clear();
			}
		}

		public void Add(T value)
		{
			lock (this.SyncRoot)
			{
				this.Inner.Add(value);
			}
		}

		public void AddRange(IEnumerable<T> values)
		{
			lock (this.SyncRoot)
			{
				this.Inner.AddRange(values);
			}
		}

		public void RemoveAt(int index)
		{
			lock (this.SyncRoot)
			{
				this.Inner.RemoveAt(index);
			}
		}

		public void RemoveAll(Predicate<T> match)
		{
			lock (this.SyncRoot)
			{
#if true
				this.Inner.RemoveAll(match);
#else
				int count = 0;

				for (int index = 0; index < this.Inner.Count; index++)
					if (match(this.Inner[index]) == false)
						this.Inner[count++] = this.Inner[index];

				this.Inner.RemoveRange(count, this.Inner.Count - count);
#endif
			}
		}

		public T this[int index]
		{
			get
			{
				lock (this.SyncRoot)
				{
					return this.Inner[index];
				}
			}

			set
			{
				lock (this.SyncRoot)
				{
					this.Inner[index] = value;
				}
			}
		}

		public T GetPost(int index, T value)
		{
			lock (this.SyncRoot)
			{
				T ret = this.Inner[index];
				this.Inner[index] = value;
				return ret;
			}
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

		public T[] ToArray()
		{
			lock (this.SyncRoot)
			{
				return this.Inner.ToArray();
			}
		}

		public void Invoke(Action<List<T>> routine)
		{
			lock (this.SyncRoot)
			{
				routine(this.Inner);
			}
		}
	}
}
