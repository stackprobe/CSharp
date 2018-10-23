using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class MultiThreadEx : IDisposable
	{
		private List<ThreadEx> Ths = new List<ThreadEx>();

		public void Add(Action routine)
		{
			Ths.Add(new ThreadEx(routine));
		}

		public bool IsEnded(int millis = 0)
		{
			foreach (ThreadEx th in Ths)
				if (th.IsEnded(millis) == false)
					return false;

			return true;
		}

		public void WaitToEnd()
		{
			foreach (ThreadEx th in Ths)
				th.WaitToEnd();
		}

		public void RelayThrow()
		{
			this.WaitToEnd();

			Exception[] es = Ths.Select(th => th.GetException()).Where(e => e != null).ToArray();

			if (1 <= es.Length)
				throw new Exception("Relay: " + string.Join("\r\n", es.Select(e => e + "　<---- 内部の例外ここまで")));
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
