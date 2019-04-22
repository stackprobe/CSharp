using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class SecurityToolsTest
	{
		public void Test01()
		{
			using (RandomUnit r1 = new RandomUnit(new SecurityTools.CSPRandomNumberGenerator()))
			using (RandomUnit r2 = new RandomUnit(new SecurityTools.CSPRandomNumberGenerator()))
			using (RandomUnit r3 = new RandomUnit(new SecurityTools.CSPRandomNumberGenerator()))
			// --
			using (RandomUnit a1 = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(1)))
			using (RandomUnit a2 = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(2)))
			using (RandomUnit a3 = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(3)))
			// --
			using (RandomUnit a1_1 = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(1)))
			using (RandomUnit a1_2 = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(1)))
			using (RandomUnit a1_3 = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(1)))
			{
				RandomUnit[] rs = new RandomUnit[]
				{
					r1,
					r2,
					r3,
					// --
					a1,
					a2,
					a3,
					// --
					a1_1,
					a1_2,
					a1_3,
				};

				for (int c = 0; c < 1000; c++)
				{
					for (int i = 0; i < rs.Length; i++)
					{
						if (1 <= i)
							Console.Write("\t");

						Console.Write(BinTools.Hex.ToString(rs[i].GetByte()));
					}
					Console.WriteLine("");
				}

				Console.WriteLine("----");
				Console.WriteLine(@"xx	xx	xx	e2	a1	36	e2	e2	e2"); // 最終行の想定値 @ 2019.2.3
				// xx == 不定
			}

			//	Console.WriteLine(@"xx	xx	xx	ad	95	cd	ad	ad	ad"); // 最終行の想定値 @ 2018.4.4 // 16bitの場合
		}

		public void Test02()
		{
			using (RandomUnit rand = new RandomUnit(new SecurityTools.AESRandomNumberGenerator(123)))
			{
				for (int c = 0; c < 100; c++)
				{
					Console.WriteLine(BinTools.Hex.ToString(rand.GetBytes(16)));
				}
			}
		}

		public void Test03()
		{
			Test03_a("");
			Test03_a("ABC");
			Test03_a("ABCDEF");
			Test03_a("ABCDEFHIJ");
			Test03_a("123");
			Test03_a("123:456");
			Test03_a("123:456:789");
			Test03_a(":");
			Test03_a("::");
			Test03_a(":::");
			Test03_a("aaaa:");
			Test03_a(":aaaa");
			Test03_a(":aaaa:");
		}

		private void Test03_a(string src)
		{
			byte[][] bParts = src.Split(':').Select(token => Encoding.ASCII.GetBytes(token)).ToArray();
			byte[] bSrc = BinTools.Join(bParts);

			Console.WriteLine("bSrc: " + BinTools.Hex.ToString(bSrc));

			foreach (byte[] bPart in bParts)
				Console.WriteLine("bPart: " + BinTools.Hex.ToString(bPart));

			{
				byte[] hash1 = SecurityTools.GetSHA512(bSrc);
				byte[] hash2 = SecurityTools.GetSHA512(bParts);

				Console.WriteLine("hash1: " + BinTools.Hex.ToString(hash1));
				Console.WriteLine("hash2: " + BinTools.Hex.ToString(hash2));

				if (BinTools.Comp(hash1, hash2) != 0)
					throw null; // bugged !!!
			}

			{
				byte[] hash1 = SecurityTools.GetMD5(bSrc);
				byte[] hash2 = SecurityTools.GetMD5(bParts);

				Console.WriteLine("hash1: " + BinTools.Hex.ToString(hash1));
				Console.WriteLine("hash2: " + BinTools.Hex.ToString(hash2));

				if (BinTools.Comp(hash1, hash2) != 0)
					throw null; // bugged !!!
			}
		}
	}
}
