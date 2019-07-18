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
			new Question(new string[]
			{
				"    #### ",
				"##### S# ",
				"#   11 # ",
				"# 1222 # ",
				"##12231##",
				" # 2221 #",
				" # 11   #",
				" #  #####",
				" ####    ",
			})
			.Solve();
		}

		public void Test02()
		{
			new Question(new string[]
			{
				"   #### ",
				"#### S# ",
				"#   1 # ",
				"# 1222##",
				"##1 32 #",
				" #  1  #",
				" #  ####",
				" ####   ",
			})
			.Solve();
		}
	}
}
