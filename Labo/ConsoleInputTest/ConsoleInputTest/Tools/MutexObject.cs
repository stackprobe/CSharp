using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class MutexObject : IDisposable
	{
		private Mutex _m;

		public MutexObject(string name)
		{
			_m = new Mutex(false, name);
		}

		public bool waitForMillis(int millis)
		{
			return _m.WaitOne(millis);
		}

		public void waitForever()
		{
			_m.WaitOne();
		}

		public void release()
		{
			_m.ReleaseMutex();
		}

		public void Dispose()
		{
			if (_m != null)
			{
				_m.Dispose();
				_m = null;
			}
		}

		public static IDisposable section(string name)
		{
			MutexObject mo = new MutexObject(name);

			mo.waitForever();

			return new DLeave(delegate
			{
				mo.release();
				mo.Dispose();
			});
		}

		public static IDisposable section(MutexObject mo)
		{
			mo.waitForever();

			return new DLeave(delegate
			{
				mo.release();
			});
		}
	}
}
