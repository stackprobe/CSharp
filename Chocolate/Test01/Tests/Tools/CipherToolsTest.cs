using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public static class CipherToolsTest
	{
		public class AESTest
		{
			public void Test01()
			{
				using (StreamReader reader = new StreamReader(@"C:\Factory\Labo\utest\auto\OpenSource\aes128\testvector\t_aes128.txt", Encoding.ASCII))
				{
					CipherTools.AES aes = null;

					for (; ; )
					{
						string line = reader.ReadLine();

						if (line == null)
							break;

						if (line.StartsWith("K No."))
						{
							if (aes != null)
								aes.Dispose();

							aes = new CipherTools.AES(GetTVBlock(line));
						}
						else if (line.StartsWith("P No."))
						{
							byte[] plain = GetTVBlock(line);

							line = reader.ReadLine();

							if (!line.StartsWith("C No."))
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

			private static void EncDecTest(CipherTools.AES aes, byte[] plain, byte[] cipher)
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
