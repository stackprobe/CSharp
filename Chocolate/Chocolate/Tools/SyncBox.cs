using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class SyncBox<T>
	{
		private object SyncRoot = new object();
		private T Value;

		public SyncBox()
		{ }

		public SyncBox(T value)
		{
			this.Value = value;
		}

		public T Get()
		{
			lock (this.SyncRoot)
			{
				return this.Value;
			}
		}

		public void Post(T value)
		{
			lock (this.SyncRoot)
			{
				this.Value = value;
			}
		}

		public T GetPost(T value)
		{
			lock (this.SyncRoot)
			{
				T ret = this.Value;
				this.Value = value;
				return ret;
			}
		}
	}
}
