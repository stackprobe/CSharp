using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class CriticalTest
	{
		public void Test01()
		{
			Critical critical = new Critical();
			List<ThreadEx> ths = new List<ThreadEx>();
			int count = 0;

			for (int c = 0; c < 100; c++)
			{
				ths.Add(new ThreadEx(() =>
				{
					for (int d = 0; d < 100; d++)
					{
						critical.Section(() =>
						{
#if true
							count++;
#else
							int tmp = count;

							Thread.Sleep(0);

							count = tmp + 1;
#endif
						});
					}
				}
				));
			}

			foreach (ThreadEx th in ths)
				th.WaitToEnd();

			Console.WriteLine("100 x 100 == " + count);
		}

		public void Test02()
		{
			Critical critical = new Critical();

			Console.WriteLine("*1");
			DateTime t = DateTime.Now;

			critical.Section(() =>
			{
				for (int c = 0; c < 10000000; c++)
				{
					critical.ContextSwitching();
				}
			});

			DateTime t2 = DateTime.Now;
			Console.WriteLine("*2 " + t2 + ", " + (t2 - t).TotalMilliseconds);

			using (MultiThreadEx mte = new MultiThreadEx())
			{
				for (int i = 0; i < 2; i++)
				{
					mte.Add(() =>
					{
						critical.Section(() =>
						{
							for (int c = 0; c < 10000000; c++)
							{
								critical.ContextSwitching();
							}
						});
					});
				}
			}

			DateTime t3 = DateTime.Now;
			Console.WriteLine("*3 " + t3 + ", " + (t3 - t2).TotalMilliseconds);

			using (MultiThreadEx mte = new MultiThreadEx())
			{
				for (int i = 0; i < 3; i++)
				{
					mte.Add(() =>
					{
						critical.Section(() =>
						{
							for (int c = 0; c < 10000000; c++)
							{
								critical.ContextSwitching();
							}
						});
					});
				}
			}

			DateTime t4 = DateTime.Now;
			Console.WriteLine("*4 " + t4 + ", " + (t4 - t3).TotalMilliseconds);
		}
	}
}
