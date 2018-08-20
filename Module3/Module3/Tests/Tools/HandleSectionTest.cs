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
			using (var hm = new HandleSection<Mutex>(() => new Mutex(false, "{f7d50620-2387-4331-8bd8-7d7f19aeedbc}"), m => m.Dispose()))
			using (var hb = new HandleSection<bool>(() => hm.GetHandle().WaitOne(0), b => { if (b) hm.GetHandle().ReleaseMutex(); }))
			{
				if (hb.GetHandle())
				{
					Console.WriteLine("OK!");
				}
			}
		}
	}
}
