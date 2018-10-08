using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class SharedQueueTest
	{
		public void Test01()
		{
			SharedQueue sq = new SharedQueue("{4830ae3c-cd10-4143-8c36-af42d050e5de}");

			sq.Clear();

			sq.Enqueue(Encoding.ASCII.GetBytes("ABC"));
			sq.Enqueue(Encoding.ASCII.GetBytes("abcdef"));
			sq.Enqueue(Encoding.ASCII.GetBytes("123456789"));

#if false // params 廃止
			sq.Enqueue(
				Encoding.UTF8.GetBytes("いろは"),
				Encoding.UTF8.GetBytes("にほへと"),
				Encoding.UTF8.GetBytes("ちりぬるを")
				);
#else
			sq.Enqueue(new byte[][]
			{
				Encoding.UTF8.GetBytes("いろは"),
				Encoding.UTF8.GetBytes("にほへと"),
				Encoding.UTF8.GetBytes("ちりぬるを")
			});
#endif

			sq.Enqueue(new byte[][]
			{
				Encoding.ASCII.GetBytes("001"),
				Encoding.ASCII.GetBytes("002"),
				Encoding.ASCII.GetBytes("003"),
			});

			sq.Enqueue(new List<byte[]>(new byte[][]
			{
				Encoding.ASCII.GetBytes("004"),
				Encoding.ASCII.GetBytes("005"),
				Encoding.ASCII.GetBytes("006"),
			}));

			sq.Enqueue(Test01_b(new byte[][]
			{
				Encoding.ASCII.GetBytes("007"),
				Encoding.ASCII.GetBytes("008"),
				Encoding.ASCII.GetBytes("009"),
			}));

			foreach (byte[] value in sq.DequeueAll())
			{
				Console.WriteLine("value: " + Encoding.UTF8.GetString(value));
			}
		}

		private IEnumerable<byte[]> Test01_b(byte[][] src)
		{
			foreach (byte[] value in src)
			{
				yield return value;
			}
		}
	}
}
