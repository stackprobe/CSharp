using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.Tests
{
	public class Test0001
	{
		private IEnumerable<int> OneToTen()
		{
			Console.WriteLine("ott.start");

			for (int c = 0; c < 10; c++)
			{
				Console.WriteLine("ott.c: " + c);

				yield return c;

				Console.WriteLine("ott.d: " + c);
			}
			Console.WriteLine("ott.end");
		}

		public void Test01()
		{
			Console.WriteLine("----");

			foreach (int r in OneToTen())
				Console.WriteLine("r: " + r);

			Console.WriteLine("----");

			{
				int c = 0;

				foreach (int r in OneToTen())
				{
					Console.WriteLine("r: " + r);

					if (3 <= ++c)
						break;
				}
			}

			Console.WriteLine("----");

			OneToTen().GetEnumerator().Dispose();

			Console.WriteLine("----");

			using (IEnumerator<int> ott = OneToTen().GetEnumerator())
			{
				while (ott.MoveNext())
				{ }
			}

			Console.WriteLine("----");

			using (IEnumerator<int> ott = OneToTen().GetEnumerator())
			{
				Console.WriteLine(ott.MoveNext());
				Console.WriteLine(ott.MoveNext());
				Console.WriteLine(ott.MoveNext());
			}

			Console.WriteLine("----");

			using (IEnumerator<int> ott = OneToTen().GetEnumerator())
			{
				while (ott.MoveNext() && ott.Current < 3)
				{ }
			}
		}
	}
}
