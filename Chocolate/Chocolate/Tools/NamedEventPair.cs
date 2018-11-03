using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class NamedEventPair : IDisposable
	{
		private NamedEventUnit HandleForSet;
		private NamedEventUnit HandleForWait;

		public NamedEventPair(string name)
		{
			this.HandleForSet = new NamedEventUnit(name);
			this.HandleForWait = new NamedEventUnit(name); // fixme これが失敗すると HandleForSet が宙に浮く。
		}

		public void Set()
		{
			this.HandleForSet.Set();
		}

		public void WaitForever()
		{
			this.HandleForWait.WaitForever();
		}

		public bool WaitForMillis(int millis)
		{
			return this.HandleForWait.WaitForMillis(millis);
		}

		public void Dispose()
		{
			if (this.HandleForSet != null)
			{
				this.HandleForSet.Dispose();
				this.HandleForWait.Dispose();

				this.HandleForSet = null;
			}
		}
	}
}
