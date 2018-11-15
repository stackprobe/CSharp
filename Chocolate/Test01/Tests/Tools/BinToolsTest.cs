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
			for (int c = -1000; c <= 1000; c++)
			{
				Test01_Int(c);
			}
			for (int c = int.MinValue; c <= int.MinValue + 1000; c++)
			{
				Test01_Int(c);
			}
			for (int c = int.MaxValue; int.MaxValue - 1000 <= c; c--)
			{
				Test01_Int(c);
			}
			for (int c = (int)(int.MinValue * 0.9); c <= (int)(int.MaxValue * 0.9); c += SecurityTools.CRandom.GetInt(int.MaxValue / 1000))
			{
				Test01_Long(c);
			}

			for (long c = -1000L; c <= 1000L; c++)
			{
				Test01_Long(c);
			}
			for (long c = long.MinValue; c <= long.MinValue + 1000L; c++)
			{
				Test01_Long(c);
			}
			for (long c = long.MaxValue; long.MaxValue - 1000L <= c; c--)
			{
				Test01_Long(c);
			}
			for (long c = int.MinValue * 10L; c <= int.MaxValue * 10L; c += SecurityTools.CRandom.GetInt64(int.MaxValue / 1000L))
			{
				Test01_Long(c);
			}
			for (long c = (long)(long.MinValue * 0.9); c <= (long)(long.MaxValue * 0.9); c += SecurityTools.CRandom.GetInt64(long.MaxValue / 1000000L))
			{
				Test01_Long(c);
			}

			Console.WriteLine("OK!");
		}

		private void Test01_Int(int c)
		{
			int a = BinTools.ToInt(BinTools.ToBytes(c));

			//Console.WriteLine(c + " -> " + a); // test

			if (a != c)
				throw null; // bugged !!!
		}

		private void Test01_Long(long c)
		{
			long a = BinTools.ToInt64(BinTools.ToBytes64(c));

			//Console.WriteLine(c + " -> " + a); // test

			if (a != c)
				throw null; // bugged !!!
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

			byte[] mid = BinTools.SplittableJoin(src);
			byte[][] dest = BinTools.Split(mid);

			for (int index = 0; index < dest.Length; index++)
				Console.WriteLine(Encoding.UTF8.GetString(dest[index]));

			Console.WriteLine("----");
		}
	}
}
