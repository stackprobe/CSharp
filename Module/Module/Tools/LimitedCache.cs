using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class LimitedCache<T>
	{
		private List<T> _values = new List<T>(); // [旧 ... 新]
		private int _limit;

		public LimitedCache(int limit = 10)
		{
			_limit = limit;
		}

		public delegate bool Accept_d(T value);
		public delegate T Create_d();
		public delegate void Release_d(T value);

		public T get(Accept_d accept, Create_d create, Release_d release)
		{
			for (int index = _values.Count - 1; 0 <= index; index--)
			{
				T value = _values[index];

				if (accept(value))
				{
					_values.RemoveAt(index);
					_values.Add(value);

					return value;
				}
			}

			if (_limit <= _values.Count)
			{
				T value = _values[0];

				release(value);

				_values.RemoveAt(0);
			}

			{
				T value = create();

				_values.Add(value);

				return value;
			}
		}

		public void clear(Release_d release)
		{
			foreach (T value in _values)
			{
				release(value);
			}
			_values.Clear();
		}
	}
}
