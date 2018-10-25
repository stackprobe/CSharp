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

		public void Test02()
		{
			try
			{
				using (MultiThreadTaskInvoker mtti = new MultiThreadTaskInvoker())
				{
					for (int c = 0; c < 1000; c++)
					{
						int f_c = c;

						mtti.AddTask(() =>
						{
							throw new Test02_Exception("c: " + f_c);
						});
					}
					mtti.RelayThrow();
				}
			}
			catch (Test02_Exception e)
			{
				Console.WriteLine("キャッチした例外：" + e);
			}
		}

		private class Test02_Exception : Exception
		{
			public Test02_Exception(string message)
				: base(message)
			{ }
		}

		public void Test03()
		{
			DateTime startedTime = DateTime.Now;

			using (MultiThreadTaskInvoker mtti = new MultiThreadTaskInvoker())
			{
				for (int c = 0; c < 1000000; c++)
				{
					int f_c = c;

					mtti.AddTask(() =>
					{
						// noop
					});
				}
				mtti.RelayThrow();
			}
			DateTime endedTime = DateTime.Now;

			Console.WriteLine("処理時間：" + ((endedTime - startedTime).TotalMilliseconds / 1000.0));
		}
	}
}
