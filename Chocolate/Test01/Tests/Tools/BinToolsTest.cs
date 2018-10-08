using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class BinToolsTest
	{
		public void Test01()
		{
			for (int value = -10000; value <= 10000; value++)
			{
				Test01a(value);
			}
			for (int value = -2100000000; value <= 2100000000; value += 10000)
			{
				Test01a(value);
			}
			Test01a(int.MinValue);
			Test01a(int.MaxValue);

			Console.WriteLine("OK!");
		}

		private void Test01a(int value)
		{
			int ret = BinTools.ToInt(BinTools.ToBytes(value));

			if (ret != value)
				throw null;
		}

		public void Test02()
		{
			Test02a(new string[] { "ABC" });
			Test02a(new string[] { "abcdef", "123456" });
			Test02a(new string[] { "いろは", "にほへと", "ちりぬるを" });
		}

		private void Test02a(string[] strs)
		{
			byte[][] src = new byte[strs.Length][];

			for (int index = 0; index < strs.Length; index++)
				src[index] = Encoding.UTF8.GetBytes(strs[index]);

			byte[] mid = BinTools.Join(src);
			byte[][] dest = BinTools.Split(mid);

			for (int index = 0; index < dest.Length; index++)
				Console.WriteLine(Encoding.UTF8.GetString(dest[index]));

			Console.WriteLine("----");
		}
	}
}
