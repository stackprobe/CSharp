using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class MtxSection : IDisposable
	{
		private Mutex _m;

		public MtxSection(Mutex m)
		{
			m.WaitOne();
			_m = m;
		}

		public void Dispose()
		{
			if (_m != null)
			{
				_m.ReleaseMutex();
				_m = null;
			}
		}
	}
}
