using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class PostBox<T>
	{
		private object SYNCROOT = new object();
		private T _lastValue;

		public PostBox(T initValue)
		{
			_lastValue = initValue;
		}

		public T Post(T valueNew)
		{
			lock (SYNCROOT)
			{
				T ret = _lastValue;
				_lastValue = valueNew;
				return ret;
			}
		}
	}
}
