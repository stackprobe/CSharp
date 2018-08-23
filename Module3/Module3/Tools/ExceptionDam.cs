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
				Exception e = this.Es[0];

				this.Es.Clear();

				throw new Exception("保留していた" + count + "個の例外のうち最初の例外をInnerにして例外を投げます。", e);
			}
		}
	}
}
