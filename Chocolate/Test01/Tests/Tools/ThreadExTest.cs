using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Tests.Tools
{
	public class ThreadExTest
	{
		public void Test01()
		{
			try
			{
				using (ThreadEx te = new ThreadEx(() => { throw new Exception("例外01"); }))
				{
					te.RelayThrow();
				}
			}
			catch (Exception e)
			{
				Console.WriteLine("キャッチした例外：" + e);
			}
		}

		public void Test02()
		{
			Console.WriteLine("*1");

			using (new ThreadEx(() =>
			{
				Console.WriteLine("*t1.1");
				Thread.Sleep(2000);
				Console.WriteLine("*t1.2");
			}))
			using (new ThreadEx(() =>
			{
				Console.WriteLine("*t2.1");
				Thread.Sleep(2000);
				Console.WriteLine("*t2.2");
			}))
			using (new ThreadEx(() =>
			{
				Console.WriteLine("*t3.1");
				Thread.Sleep(2000);
				Console.WriteLine("*t3.2");
			}))
			{
				Console.WriteLine("*2.1");
				Thread.Sleep(2000);
				Console.WriteLine("*2.2");
			}
			Console.WriteLine("*3");
		}
	}
}
