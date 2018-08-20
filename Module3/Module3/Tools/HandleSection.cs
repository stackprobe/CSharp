using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HandleSection<T> : IDisposable
	{
		private T Handle;
		private Action<T> Leave;

		public HandleSection(Func<T> enter, Action<T> leave)
		{
			this.Handle = enter();
			this.Leave = leave;
		}

		public T GetHandle()
		{
			return this.Handle;
		}

		public void Dispose()
		{
			if (this.Leave != null)
			{
				this.Leave(this.Handle);
				this.Leave = null;
			}
		}
	}
}
