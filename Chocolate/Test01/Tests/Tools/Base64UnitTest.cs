using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class Base64UnitTest
	{
		private static readonly string[] TEST_VECTOR = new string[]
		{
			"", "",
			"f", "Zg==",
			"fo", "Zm8=",
			"foo", "Zm9v",
			"foob", "Zm9vYg==",
			"fooba", "Zm9vYmE=",
			"foobar", "Zm9vYmFy",
		};

		public void Test01()
		{
			for (int index = 0; index < TEST_VECTOR.Length; index += 2)
			{
				byte[] plain = Encoding.ASCII.GetBytes(TEST_VECTOR[index]);
				string encoded = TEST_VECTOR[index + 1];

				string enc = new Base64Unit().Encode(plain);
				byte[] dec = new Base64Unit().Decode(encoded);

				if (ArrayTools.Comp(plain, dec, BinTools.Comp) != 0)
					throw null; // bugged !!!

				if (encoded != enc)
					throw null; // bugged !!!
			}
		}

		public void Test02()
		{
			Test_Random();
		}

		private void Test_Random()
		{
			for (int c = 0; c < 1000; c++)
			{
				Test_Random(Test_Random_GetData(1000));
			}
			for (int c = 0; c < 30; c++)
			{
				Test_Random(Test_Random_GetData(1000000));
			}
			for (int c = 0; c < 10; c++)
			{
				Test_Random(Test_Random_GetData(8000000));
			}
		}

		private void Test_Random(byte[] data)
		{
			{
				Console.WriteLine("*Base64");

				Base64Unit b64 = new Base64Unit();

				string encData = b64.Encode(data);
				byte[] decData = b64.Decode(encData);

				Console.WriteLine("data: " + data.Length);
				Console.WriteLine("encData: " + encData.Length);
				Console.WriteLine("decData: " + decData.Length);

				if (ArrayTools.Comp(data, decData, BinTools.Comp) != 0)
					throw null; // bugged !!!
			}

			{
				Console.WriteLine("*Base64Url");

				Base64Unit.NoPadding b64 = Base64Unit.CreateByC6364P("-_=").GetNoPadding();

				string encData = b64.Encode(data);
				byte[] decData = b64.Decode(encData);

				Console.WriteLine("data: " + data.Length);
				Console.WriteLine("encData: " + encData.Length);
				Console.WriteLine("decData: " + decData.Length);

				if (ArrayTools.Comp(data, decData, BinTools.Comp) != 0)
					throw null; // bugged !!!
			}
		}

		private byte[] Test_Random_GetData(int maxSize)
		{
			return SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.GetInt(maxSize));
		}
	}
}
