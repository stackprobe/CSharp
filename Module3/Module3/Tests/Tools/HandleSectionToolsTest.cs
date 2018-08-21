using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class HandleSectionToolsTest
	{
		public void Test01()
		{
			using (Mutex m = new Mutex(false, "{cc43d273-046c-4de3-a57c-80364028d1a6}"))
			{
				using (var k = HandleSectionTools.Lock(m, 2000))
				{
					if (k.Handle)
					{
						Console.WriteLine("Locking...");
					}
				}
				using (var k = HandleSectionTools.Lock(m, 0))
				{
					if (k.Handle)
					{
						Console.WriteLine("Locking...");
					}
				}
				using (var k = HandleSectionTools.Lock(m))
				{
					Console.WriteLine("Locking...");
				}
			}
		}
	}
}
