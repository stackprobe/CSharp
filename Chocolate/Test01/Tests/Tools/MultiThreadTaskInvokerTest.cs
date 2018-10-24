using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Tests.Tools
{
	public class MultiThreadTaskInvokerTest
	{
		public void Test01()
		{
			using (MultiThreadTaskInvoker mtti = new MultiThreadTaskInvoker())
			{
				for (int c = 0; c < 10; c++)
				{
					mtti.AddTask(() =>
					{
						Console.WriteLine("*1");
						Thread.Sleep(500);
						Console.WriteLine("*2");
					});
				}
			}
		}
	}
}
