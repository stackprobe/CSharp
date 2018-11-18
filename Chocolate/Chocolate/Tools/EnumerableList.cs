using System;
using System.Linq;
using System.Text;
using System.Collections;
using System.Collections.Generic;

namespace Charlotte.Tools
{
	public class EnumerableList<T> : IEnumerable<T>
	{
		private List<IEnumerable<T>> Sources = new List<IEnumerable<T>>();

		public EnumerableList<T> AddOne(T element)
		{
			return this.Add(new T[] { element });
		}

		public EnumerableList<T> Add(IEnumerable<T> src)
		{
			this.Sources.Add(src);
			return this;
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
