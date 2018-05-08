using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class DLeave : IDisposable
	{
		public delegate void Leave_d();

		private Leave_d _leave;

		public DLeave(Leave_d leave)
		{
			_leave = leave;
		}

		public void Dispose()
		{
			_leave();
		}
	}
}
