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
			using (RandomUnit r1 = new RandomUnit(new SecurityTools.RNGRandomNumberGenerator()))
			using (RandomUnit r2 = new RandomUnit(new SecurityTools.RNGRandomNumberGenerator()))
			using (RandomUnit r3 = new RandomUnit(new SecurityTools.RNGRandomNumberGenerator()))
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
				Console.WriteLine(@"xx	xx	xx	ad	95	cd	ad	ad	ad"); // 最終行の想定値 @ 2018.4.4
				// xx == 不定
			}
		}

		public class AESTest
		{
			public void Test01()
			{
				using (StreamReader reader = new StreamReader(@"C:\Factory\Labo\utest\auto\OpenSource\aes128\testvector\t_aes128.txt", Encoding.ASCII))
				{
					SecurityTools.AES aes = null;

					for (; ; )
					{
						string line = reader.ReadLine();

						if (line == null)
							break;

						if (line.StartsWith("K No."))
						{
							if (aes != null)
								aes.Dispose();

							aes = new SecurityTools.AES(GetTVBlock(line));
						}
						else if (line.StartsWith("P No."))
						{
							byte[] plain = GetTVBlock(line);

							line = reader.ReadLine();

							if (line.StartsWith("C No.") == false)
								throw null;

							byte[] cipher = GetTVBlock(line);

							EncDecTest(aes, plain, cipher);
						}
					}
					if (aes != null)
					{
						aes.Dispose();
						aes = null;
					}
				}
			}

			private static byte[] GetTVBlock(string line)
			{
				return BinTools.Hex.ToBytes(line.Substring(line.IndexOf(':') + 1).Replace(" ", ""));
			}

			private static void EncDecTest(SecurityTools.AES aes, byte[] plain, byte[] cipher)
			{
				Console.WriteLine("aes: " + aes + ", " + aes.GetHashCode());
				Console.WriteLine("< " + BinTools.Hex.ToString(plain));
				Console.WriteLine("< " + BinTools.Hex.ToString(cipher));

				byte[] encans = new byte[16];
				byte[] decans = new byte[16];

				aes.EncryptBlock(plain, encans);
				aes.DecryptBlock(cipher, decans);

				Console.WriteLine("> " + BinTools.Hex.ToString(encans));
				Console.WriteLine("> " + BinTools.Hex.ToString(decans));

				if (BinTools.Comp(encans, cipher) != 0)
					throw null;

				if (BinTools.Comp(decans, plain) != 0)
					throw null;
			}
		}
	}
}
