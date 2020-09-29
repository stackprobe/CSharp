using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EnumerableToolsTest
	{
		public void Test01()
		{
			//Console.WriteLine(EnumerableTools.Join("ABC").ToArray()); // ビルドエラー：そんなメソッドありません！
			Console.WriteLine(EnumerableTools.Join("ABC", "DEF").ToArray());
			Console.WriteLine(EnumerableTools.Join("ABC", "DEF", "GHI").ToArray());

			Console.WriteLine(EnumerableTools.Join(new string[] { "ABC" }).ToArray());
			Console.WriteLine(EnumerableTools.Join(new string[] { "ABC", "DEF" }).ToArray());
			Console.WriteLine(EnumerableTools.Join(new string[] { "ABC", "DEF", "GHI" }).ToArray());

			{
				int[] aaa = new int[] { 1, 2, 3 };
				int[] bbb = new int[] { 4, 5, 6 };
				int[] ccc = new int[] { 7, 8, 9 };

				Console.WriteLine(string.Join(", ", EnumerableTools.Join(aaa, bbb, ccc)));

				Console.WriteLine(string.Join(", ", aaa.Concat(bbb).Concat(ccc)));
			}
		}
	}
}
