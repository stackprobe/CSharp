using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class CSemaphoreTest
	{
		private CSemaphore Test01_Semaphore = new CSemaphore(20);
		private object Test01_SYNCROOT = new object();
		private int Test01_Count = 0;

		public void Test01()
		{
			Queue<ThreadEx> ths = new Queue<ThreadEx>();

			for (int c = 0; c < 100; c++)
			{
				ths.Enqueue(new ThreadEx(() =>
				{
					for (int d = 0; d < 100; d++)
					{
						Test01_Semaphore.Section(() =>
						{
							lock (Test01_SYNCROOT)
							{
								Console.WriteLine("+ " + Test01_Count);
								Test01_Count++;
								Console.WriteLine("> " + Test01_Count);
							}

							Thread.Sleep(1);

							lock (Test01_SYNCROOT)
							{
								Console.WriteLine("- " + Test01_Count);
								Test01_Count--;
								Console.WriteLine("> " + Test01_Count);
							}
						});
					}
				}
				));
			}

			while (1 <= ths.Count)
				ths.Dequeue().WaitToEnd();
		}
	}
}
