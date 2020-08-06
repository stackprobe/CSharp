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

		public NamedEventPair() :
			this(Guid.NewGuid().ToString("B"))
		{ }

		public NamedEventPair(string name)
		{
			HandleDam.Transaction(hDam =>
			{
				this.HandleForSet = hDam.Add(new NamedEventUnit(name));
				this.HandleForWait = hDam.Add(new NamedEventUnit(name));
			});
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

		private bool Disposed = false;

		public void Dispose()
		{
			if (this.Disposed == false)
			{
				ExceptionDam.Section(eDam =>
				{
					eDam.Invoke(() => this.HandleForSet.Dispose());
					eDam.Invoke(() => this.HandleForWait.Dispose());

					this.HandleForSet = null;
					this.HandleForWait = null;

					this.Disposed = true;
				});
			}
		}
	}
}
