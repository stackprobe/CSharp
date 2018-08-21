using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class HandleSectionTools
	{
		public static HandleSection<bool> Lock(Mutex mtx, int timeoutMillis = Timeout.Infinite)
		{
			return new HandleSection<bool>(() => mtx.WaitOne(timeoutMillis), b => { if (b) mtx.ReleaseMutex(); });
		}
	}
}
