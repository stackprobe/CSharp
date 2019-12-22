using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Camellias;
using System.IO;

namespace Charlotte.Tests.Camellias
{
	public class CamelliaRingCipherUtilsTest
	{
		public void Test01()
		{
			Test01_b("ABC");
			Test01_b("abc");
			Test01_b("abc");
			Test01_b("abc[x25]");
			Test01_b("abc_[a27]");
			Test01_b("abc[$27]_");
			Test01_b("abc2[$30]");
			Test01_b("abc3[$31]");

			for (int testCount = 0; testCount < 100; testCount++)
			{
				Console.WriteLine("testCount: " + testCount);

				String passphrase = SecurityTools.MakePassword();

				Test01_b(passphrase);

				passphrase +=
					"[" +
					SecurityTools.CRandom.ChooseOne(StringTools.HALF.ToArray()) +
					SecurityTools.CRandom.GetRange(20, 25) +
					//SecurityTools.cRandom.getRangeInt(20, 50) +
					"]";

				Test01_b(passphrase);
			}
		}

		private void Test01_b(string passphrase)
		{
			Console.WriteLine("< " + passphrase);

			byte[] rawKey1 = CamelliaRingCipherUtils.GenerateRawKey(passphrase);
			byte[] rawKey2 = GenerateRawKeyByFactory(passphrase);

			Console.WriteLine("1> " + BinTools.Hex.ToString(rawKey1));
			Console.WriteLine("2> " + BinTools.Hex.ToString(rawKey2));

			if (BinTools.Comp(rawKey1, rawKey2) != 0)
				throw null; // bugged !!!
		}

		private byte[] GenerateRawKeyByFactory(string passphrase)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				String rFile = wd.MakePath();
				String wFile = wd.MakePath();

				File.WriteAllBytes(rFile, StringTools.ENCODING_SJIS.GetBytes(passphrase));

				ProcessTools.Start(
					"C:/Factory/Labo/Tools/CipherGenerateRawKey.exe",
					string.Format("\"{0}\" \"{1}\"", rFile, wFile)
					)
					.WaitForExit();

				return File.ReadAllBytes(wFile);
			}
		}
	}
}
