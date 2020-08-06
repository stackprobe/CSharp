using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;

namespace Charlotte
{
	public class ArrayTest0001
	{
		public void Test01()
		{
			Test01_a(v => ArrayTools.ToArray(v));
			Test01_a(v => v.ToArray());
		}

		private void Test01_a(Func<IEnumerable<int>, int[]> a_toArray)
		{
			try
			{
				for (int count = 100000000; ; count += 10000000)
				{
					Console.WriteLine("count: " + count);

					IEnumerable<int> arr = this.GetIterator(count);

					int[] arr2 = a_toArray(arr);

					if (arr.Count() != arr2.Length)
						throw null;

					for (int index = 0; index < count; index++)
						if (index != arr2[index])
							throw null;

					arr = null;
					arr2 = null;

					GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced);
					GC.WaitForPendingFinalizers();

					Thread.Sleep(1000);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private IEnumerable<int> GetIterator(int count)
		{
			for (int index = 0; index < count; index++)
				yield return index;
		}
	}
}
