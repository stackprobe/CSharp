using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class Critical
	{
		private object Enter_SYNCROOT = new object();
		private object SYNCROOT = new object();
		private int Entry = 0;

		// @ 2019.1.7
		// Monitor.Enter -> Monitor.Exit は同一スレッドでなけれなならないっぽい。
		// Enter -> 別スレッドで Leave 出来るように ---> Monitor.Wait -> Monitor.Pulse にした。

		public void Enter()
		{
			lock (Enter_SYNCROOT)
			{
				lock (SYNCROOT)
				{
					Entry++;

					if (Entry == 2)
						Monitor.Wait(SYNCROOT);
				}
			}
		}

		public void Leave()
		{
			lock (SYNCROOT)
			{
				if (Entry == 0)
					throw null; // never

				Entry--;

				if (Entry == 1)
					Monitor.Pulse(SYNCROOT);
			}
		}

		public T Section_Get<T>(Func<T> routine)
		{
			this.Enter();
			try
			{
				return routine();
			}
			finally
			{
				this.Leave();
			}
		}

		public void Section(Action routine)
		{
			this.Enter();
			try
			{
				routine();
			}
			finally
			{
				this.Leave();
			}
		}

		public T Unsection_Get<T>(Func<T> routine)
		{
			this.Leave();
			try
			{
				return routine();
			}
			finally
			{
				this.Enter();
			}
		}

		public void Unsection(Action routine)
		{
			this.Leave();
			try
			{
				routine();
			}
			finally
			{
				this.Enter();
			}
		}
	}
}
