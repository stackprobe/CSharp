using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class HugeQueueTest
	{
		public void Test01()
		{
			using (HugeQueue hq = new HugeQueue())
			{
				hq.FileSizeMax = 100L;

				Queue<byte[]> q = new Queue<byte[]>();

				for (int c = 0; c < 10000; c++)
				{
					long count = hq.GetCount();

					Console.WriteLine(c + ": " + count);

					if (count != (long)q.Count)
						throw null; // bugged !!!

					if (1L <= count && SecurityTools.CRandom.GetReal() < 0.5)
					{
						byte[] value1 = hq.Dequeue();
						byte[] value2 = q.Dequeue();

						if (BinTools.Comp(value1, value2) != 0)
							throw null; // bugged !!!
					}
					else
					{
						byte[] value = SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.GetInt(100));

						hq.Enqueue(value);
						q.Enqueue(value);
					}
				}
				if (hq.GetCount() != (long)q.Count)
					throw null; // bugged !!!
			}
		}

		public void Test02()
		{
			using (HugeQueue hq = new HugeQueue())
			{
				hq.FileSizeMax = 1000;

				Queue<byte[]> q = new Queue<byte[]>();


				for (int c = 0; c < 10000; c++)
				{
					Test02_a(hq, q, 0.5);
				}
				for (int c = 0; c < 10000; c++)
				{
					Test02_a(hq, q, 0.1);
				}
				for (int c = 0; c < 10000; c++)
				{
					Test02_a(hq, q, 0.8);
				}
				for (int c = 0; c < 10000; c++)
				{
					Test02_a(hq, q, 0.5);
				}


				for (int c = 0; c < 300000; c++)
				{
					Test02_a(hq, q, 0.5 + 0.4 * Math.Sin(c / 10000.0));
				}

				for (int c = 0; c < 300000; c++)
				{
					Test02_a(hq, q, 0.5 + 0.4 * Math.Sin(c / 30000.0));
				}


				if (hq.GetCount() != (long)q.Count)
					throw null; // bugged !!!
			}
		}

		private void Test02_a(HugeQueue hq, Queue<byte[]> q, double rate)
		{
			long count = hq.GetCount();

			if (count != (long)q.Count)
				throw null; // bugged !!!

			Console.WriteLine(count + ": " + rate.ToString("F3"));

			if (1L <= count && SecurityTools.CRandom.GetReal() < rate)
			{
				byte[] value1 = hq.Dequeue();
				byte[] value2 = q.Dequeue();

				if (BinTools.Comp(value1, value2) != 0)
					throw null; // bugged !!!
			}
			else
			{
				byte[] value = SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.GetInt(100));

				hq.Enqueue(value);
				q.Enqueue(value);
			}
		}
	}
}
