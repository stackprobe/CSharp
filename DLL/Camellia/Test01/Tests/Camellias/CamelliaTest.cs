using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Camellias;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Camellias
{
	public class CamelliaTest
	{
		private const string TestVectorFile = @"C:\Factory\Labo\utest\auto\OpenSource\camellia\testvector\t_camellia.txt";

		public void Test01()
		{
			Camellia camellia = null;
			byte[] plain = null;

			foreach (string line in File.ReadAllLines(TestVectorFile, Encoding.ASCII))
			{
				if (line.StartsWith("K No."))
				{
					camellia = new Camellia(GetBlockByLine(line));
				}
				else if (line.StartsWith("P No."))
				{
					plain = GetBlockByLine(line);
				}
				else if (line.StartsWith("C No."))
				{
					byte[] cipher = GetBlockByLine(line);

					byte[] plain2 = new byte[16];
					byte[] cipher2 = new byte[16];

					camellia.EncryptBlock(plain, cipher2);
					camellia.DecryptBlock(cipher, plain2);

					Console.WriteLine(BinTools.Hex.ToString(plain) + " > " + BinTools.Hex.ToString(cipher2)); // test
					Console.WriteLine(BinTools.Hex.ToString(plain2) + " < " + BinTools.Hex.ToString(cipher)); // test

					if (
						BinTools.Comp(plain, plain2) != 0 ||
						BinTools.Comp(cipher, cipher2) != 0
						)
						throw null; // bugged !!!
				}
			}
		}

		private byte[] GetBlockByLine(string line)
		{
			return BinTools.Hex.ToBytes(StringTools.GetIsland(line, ":").Right.Replace(" ", ""));
		}
	}
}
