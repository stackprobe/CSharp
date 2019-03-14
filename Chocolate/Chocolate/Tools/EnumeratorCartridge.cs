using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class EnumeratorCartridge<T>
	{
		private IEnumerator<T> Inner;
		private T DefaultValue;
		private T CurrentValue;
		private int Remaining;

		public EnumeratorCartridge(IEnumerable<T> inner, T defval = default(T))
			: this(inner.GetEnumerator(), defval)
		{ }

		public EnumeratorCartridge(IEnumerator<T> inner, T defval = default(T))
		{
			this.Inner = inner;
			this.DefaultValue = defval;
			this.CurrentValue = defval;
			this.Remaining = inner.MoveNext() ? 2 : 1;
		}

		private void Forward()
		{
			if (this.Remaining == 2)
			{
				this.CurrentValue = this.Inner.Current;

				if (this.Inner.MoveNext() == false)
					this.Remaining = 1;
			}
			else if (this.Remaining == 1)
			{
				this.CurrentValue = this.DefaultValue;
				this.Remaining = 0;
			}
		}

		public bool HasNext()
		{
			return 2 <= this.Remaining;
		}

		public bool HasCurrent()
		{
			return 1 <= this.Remaining;
		}

		public T Next()
		{
			this.Forward();
			return this.CurrentValue;
		}

		public T Current
		{
			get
			{
				return this.CurrentValue;
			}
		}

		public bool MoveNext()
		{
			this.Next();
			return this.HasCurrent();
		}

		public EnumeratorCartridge<T> Seek(int count = 1)
		{
			for (; 1 <= count; count--)
				this.Forward();

			return this;
		}

		public IEnumerable<T> Iterate()
		{
#if true
			while (this.HasNext())
				yield return this.Next();
#else // same_code
			while (this.MoveNext())
				yield return this.Current;
#endif
		}
	}
}
