using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class ExceptionDam
	{
		private List<Exception> Es = new List<Exception>();

		public T Get<T>(Func<T> getter, T defval)
		{
			try
			{
				return getter();
			}
			catch (Exception e)
			{
				this.Es.Add(e);
				return defval;
			}
		}

		public void Invoke(Action routine)
		{
			try
			{
				routine();
			}
			catch (Exception e)
			{
				this.Es.Add(e);
			}
		}

		public void Discharge()
		{
			if (1 <= this.Es.Count)
			{
				int count = this.Es.Count;
				Exception[] exs = this.Es.ToArray();

				this.Es.Clear();

				throw new AggregateException("保留していた" + count + "個の例外をInnerにして例外を投げます。", exs);
			}
		}
	}
}
