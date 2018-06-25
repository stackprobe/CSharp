using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Test01.Modules.Tests
{
	public class MultiThreadTaskPoolTest
	{
		public void Test01()
		{
			Console.WriteLine(DateTime.Now + " 1");
			using (MultiThreadTaskPool mttp = new MultiThreadTaskPool())
			{
				Console.WriteLine(DateTime.Now + " 2");
				for (int index = 0; index < 5; index++)
				{
					int tmp = index;

					Console.WriteLine(DateTime.Now + " 3");
					mttp.Add(() =>
					{
						Console.WriteLine(DateTime.Now + " 4 - " + tmp);
						Thread.Sleep(2000);
						Console.WriteLine(DateTime.Now + " 5 - " + tmp);
					});
					Console.WriteLine(DateTime.Now + " 6");

					//mttp.WaitToEnd();
					//mttp.RelayThrow();
				}
				Console.WriteLine(DateTime.Now + " 7");
			}
			Console.WriteLine(DateTime.Now + " 8");
		}
	}
}
