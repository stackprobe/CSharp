using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class ListTest0001
	{
		public string Prefix = "P_";

		public IEnumerable<string> GetIterable()
		{
			for (int c = 0; c < 10; c++)
			{
				yield return Prefix + c;
			}
		}

		public void Test01()
		{
			List<string> list = GetIterable().ToList();

			Prefix = "A_";

			foreach (string elem in list)
				Console.WriteLine(elem);
		}
	}
}
