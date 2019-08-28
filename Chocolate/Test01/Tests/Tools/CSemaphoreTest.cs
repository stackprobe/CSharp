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
		public void Test01()
		{
			Test01_a(10);
			Test01_a(20);
			Test01_a(30);
			Test01_a(40);
			Test01_a(50);
			Test01_a(60);
			Test01_a(70);
			Test01_a(80);
			Test01_a(90);
			Test01_a(100);
		}

		private object Test01_SYNCROOT = new object();
		private int Test01_Count;
		private int Test01_CountMax;

		public void Test01_a(int permit)
		{
			Queue<ThreadEx> ths = new Queue<ThreadEx>();
			CSemaphore semaphore = new CSemaphore(permit);

			Test01_Count = 0;
			Test01_CountMax = 0;

			for (int c = 0; c < 100; c++)
			{
				ths.Enqueue(new ThreadEx(() =>
				{
					for (int d = 0; d < 100; d++)
					{
						semaphore.Section_A(() =>
						{
							lock (Test01_SYNCROOT)
							{
								//Console.WriteLine("+ " + Test01_Count);
								Test01_Count++;
								Test01_CountMax = Math.Max(Test01_CountMax, Test01_Count);
								//Console.WriteLine("> " + Test01_Count);
							}

							Thread.Sleep(1);

							lock (Test01_SYNCROOT)
							{
								//Console.WriteLine("- " + Test01_Count);
								Test01_Count--;
								//Console.WriteLine("> " + Test01_Count);
							}
						});
					}
				}
				));
			}

			while (1 <= ths.Count)
				ths.Dequeue().WaitToEnd();

			Console.WriteLine(permit + " -> " + Test01_CountMax + ", " + Test01_Count);
		}
	}
}
