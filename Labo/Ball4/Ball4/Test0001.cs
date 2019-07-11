using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Test0001
	{
		public void Test01()
		{
			new Solution(new string[]
			{
				"0000000",
				"0000000",
				"0000000",
				"0000000",
				"2000000",
			})
			.Perform();
		}

		public void Test02()
		{
			new Solution(new string[]
			{
				"0000000",
				"0000000",
				"0002000",
				"1212121",
				"2121212",
			})
			.Perform();
		}

		public void Test03()
		{
			new Solution(new string[]
			{
				"0000000",
				"0000000",
				"2121212",
				"1212121",
				"2121212",
			})
			.Perform();
		}
	}
}
