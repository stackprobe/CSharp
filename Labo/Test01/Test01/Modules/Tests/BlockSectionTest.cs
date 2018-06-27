using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Test01.Modules.Tests
{
	public class BlockSectionTest
	{
		public void Test01() // 入れ子
		{
			Console.WriteLine("*1");
			BlockSection.Invoke(() =>
			{
				Console.WriteLine("*2");
				BlockSection.Invoke(() =>
				{
					Console.WriteLine("*3");
				});
				Console.WriteLine("*4");
			});
			Console.WriteLine("*5");
		}

		public void Test02() // 入れ子 in Thread
		{
			Thread th = null;

			Console.WriteLine("*1");
			BlockSection.Invoke(() =>
			{
				th = new Thread(() =>
				{
					Console.WriteLine("*2");
					BlockSection.Invoke(() =>
					{
						Console.WriteLine("*3");
					});
					Console.WriteLine("*4");
				});
				th.Start();
				Thread.Sleep(2000);
			});
			Console.WriteLine("*5");

			th.Join();
		}
	}
}
