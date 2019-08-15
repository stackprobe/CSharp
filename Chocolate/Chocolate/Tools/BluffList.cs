using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class BluffList<T>
	{
		public Func<int> GetCount = () => -1;
		public Func<int, T> GetElement = index => default(T);
		public Action<int, T> SetElement = (index, value) => { };

		// <---- prm

		public int Count
		{
			get
			{
				return this.GetCount();
			}
		}

		public T this[int index]
		{
			get
			{
				return this.GetElement(index);
			}

			set
			{
				this.SetElement(index, value);
			}
		}

		public IEnumerable<T> Iterate()
		{
			int count = this.GetCount();

			for (int index = 0; index < count; index++)
			{
				yield return this.GetElement(index);
			}
		}
	}
}
