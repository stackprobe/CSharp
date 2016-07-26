using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class PostBox<T>
	{
		private object SYNCROOT = new object();
		private Queue<T> _values = new Queue<T>();
		private T _dummyValue;

		public PostBox(T dummyValue)
		{
			_dummyValue = dummyValue;
		}

		public void Clear()
		{
			lock (SYNCROOT)
			{
				_values.Clear();
			}
		}

		public void Post(T value)
		{
			lock (SYNCROOT)
			{
				_values.Enqueue(value);
			}
		}

		public T Get()
		{
			lock (SYNCROOT)
			{
				if (_values.Count == 0)
					return _dummyValue;

				return _values.Dequeue();
			}
		}
	}
}
