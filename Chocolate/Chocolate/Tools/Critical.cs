using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class Critical
	{
		private object SYNCROOT = new object();

		private void Enter()
		{
			Monitor.Enter(SYNCROOT);
		}

		private void Leave()
		{
			Monitor.Exit(SYNCROOT);
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
