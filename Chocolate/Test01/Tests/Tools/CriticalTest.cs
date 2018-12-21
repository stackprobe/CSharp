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
	}
}
