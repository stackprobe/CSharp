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

					//mttp.RelayThrow();
				}
				Console.WriteLine(DateTime.Now + " 7");
			}
			Console.WriteLine(DateTime.Now + " 8");
		}

		public void Test02()
		{
			try
			{
				using (MultiThreadTaskPool mttp = new MultiThreadTaskPool())
				{
					for (int index = 0; index < 100; index++)
					{
						int tmp = index;

						mttp.Add(() =>
						{
							throw new Exception("i=" + tmp);
						});
					}

					mttp.RelayThrow();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e); // ここへ到達する。最初の例外は i=0,1,2 あたりか...
			}
		}
	}
}
