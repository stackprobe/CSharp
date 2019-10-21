using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.wb.t20190720
{
	public class Test0001
	{
		public void Test01()
		{
			var items = new[]
			{
				new[] { "1", "S-0001" },
				new[] { "2", "S-0002" },
				new[] { "3", "S-0003" },
			};

			var ret = items.First(v => v[0] == "2");

			Console.WriteLine(ret[1]);

			// - - -

			Console.WriteLine(string.Join(":", items.Select(v => v[0])));
			Console.WriteLine(string.Join(":", items.Select(v => v[1])));

			// ----

			var vals = new[]
			{
				null,
				null,
				"AAAA",
				null,
				"BBBB",
				"CCCC",
			};

			var val = vals.First(v => v != null);

			Console.WriteLine(val);
		}
	}
}
