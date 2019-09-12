using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Tests
{
	public class Test0002
	{
		private IEnumerable<int> Test01_Ret123()
		{
			yield return 1;
			yield return 2;
			yield return 3;
		}

		public void Test01()
		{
			Func<int> reader = EnumerableTools.Supplier(Test01_Ret123());

			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine(reader());
			}
		}

		private IEnumerable<string> Test02_Seq()
		{
			using (new AnonyDisposable(() => Console.WriteLine("DISPOSE")))
			{
				for (; ; )
				{
					yield return "123";
				}
			}
		}

		public void Test02()
		{
			foreach (string value in Test02_Seq())
			{
				Console.WriteLine(value);
				Console.WriteLine("*1");
				break;
			}

			// ^ ここで Test02_Seq() の DISPOSE が呼ばれる。

			Console.WriteLine("*2");

			// ----

			using (IEnumerator<object> reader = Test02_Seq().GetEnumerator())
			{
				Console.WriteLine(reader.MoveNext());
				Console.WriteLine(reader.Current);
				Console.WriteLine("*1");
			}

			// ^ ここで Test02_Seq() の DISPOSE が呼ばれる。

			Console.WriteLine("*2");

			// ----

			{
				IEnumerator<object> reader = Test02_Seq().GetEnumerator();

				Console.WriteLine(reader.MoveNext());
				Console.WriteLine(reader.Current);
				Console.WriteLine("*1");

				//reader = null;
			}

			// Test02_Seq() の DISPOSE は呼ばれない。

			Console.WriteLine("*2");

			for (int c = 0; c < 30; c++)
			{
				GC.Collect();

				Thread.Sleep(100);
			}

			Console.WriteLine("*3");
		}

		public void Test02_B()
		{
			while (Console.KeyAvailable == false)
			{
				Console.WriteLine("hit any key to stop...");

				for (int c = 0; c < 1000000; c++)
				{
					Test02_Seq().GetEnumerator().MoveNext();

					// ^ を放置してもメモリリークはしない模様...
				}
				GC.Collect();
			}
		}
	}
}
