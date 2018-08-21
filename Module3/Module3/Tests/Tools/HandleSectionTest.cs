using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class HandleSectionTest
	{
		public void Test01()
		{
			using (var m = new HandleSection<Mutex>(() => new Mutex(false, "{f7d50620-2387-4331-8bd8-7d7f19aeedbc}"), v => v.Dispose()))
			using (var b = new HandleSection<bool>(() => m.Handle.WaitOne(0), v => { if (v) m.Handle.ReleaseMutex(); }))
			{
				if (b.Handle)
				{
					Console.WriteLine("Locking!");
				}
			}
		}
	}
}
