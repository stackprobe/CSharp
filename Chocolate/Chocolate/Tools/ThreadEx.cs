using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class ThreadEx : IDisposable
	{
		private Thread Th;
		private Exception Ex = null;

		public ThreadEx(Action routine)
		{
			Th = new Thread(() =>
			{
				try
				{
					routine();
				}
				catch (Exception e)
				{
					Ex = e;
				}
			});

			Th.Start();
		}

		public void WaitToEnd()
		{
			if (Th != null)
			{
				Th.Join();
				Th = null;
			}
		}

		public void RelayThrow()
		{
			this.WaitToEnd();

			if (this.Ex != null)
				throw new Exception("Relay", this.Ex);
		}

		public Exception GetException()
		{
			this.WaitToEnd();
			return this.Ex;
		}

		public void Dispose()
		{
			this.WaitToEnd();
		}
	}
}
