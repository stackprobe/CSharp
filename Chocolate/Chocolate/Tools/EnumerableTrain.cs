using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Charlotte.Tools
{
	public class EnumerableTrain<T> : IEnumerable<T>
	{
		private List<IEnumerable<T>> Sources = new List<IEnumerable<T>>();

		public EnumerableTrain<T> AddOne(T element)
		{
			return this.Add(new T[] { element });
		}

#if false
		public EnumerableTrain<T> AddLot(params T[] elements)
		{
			return this.Add(elements);
		}
#endif

		public EnumerableTrain<T> Add(IEnumerable<T> src)
		{
			this.Sources.Add(src);
			return this;
		}

		/// <summary>
		/// こっちで良くね？
		/// </summary>
		/// <returns></returns>
		public IEnumerable<T> Iterate()
		{
			foreach (IEnumerable<T> src in this.Sources)
				foreach (T element in src)
					yield return element;
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator2();
		}

		public IEnumerator<T> GetEnumerator()
		{
			return GetEnumerator2();
		}

		private IEnumerator<T> GetEnumerator2()
		{
			return new Enumerator()
			{
				Trails = this.Sources.GetEnumerator(),
			};
		}

		private class Enumerator : IEnumerator<T>
		{
			public IEnumerator<T> Curr = new List<T>(0).GetEnumerator();
			public IEnumerator<IEnumerable<T>> Trails;

			public void Reset()
			{
				this.Curr.Dispose();
				this.Curr = new List<T>(0).GetEnumerator();
				this.Trails.Reset();
			}

			object IEnumerator.Current
			{
				get
				{
					return Current2();
				}
			}

			public T Current
			{
				get
				{
					return Current2();
				}
			}

			private T Current2()
			{
				return this.Curr.Current;
			}

			public bool MoveNext()
			{
				while (this.Curr.MoveNext() == false)
				{
					if (this.Trails.MoveNext() == false)
						return false;

					this.Curr.Dispose();
					this.Curr = this.Trails.Current.GetEnumerator();
				}
				return true;
			}

			public void Dispose()
			{
				if (this.Curr != null)
				{
					this.Curr.Dispose();
					this.Trails.Dispose();

					this.Curr = null;
				}
			}
		}
	}
}
