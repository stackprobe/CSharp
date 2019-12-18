using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Camellias;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Camellias
{
	public class CamelliaRingCBCTest
	{
		public void Test01()
		{
			for (int testCount = 1; testCount <= 1000; testCount++)
			{
				Console.WriteLine("testCount: " + testCount);

				byte[] rawKey = CreateRawKey();
				byte[] data = CreateEncryptableData();

				Test01_1(rawKey, data);
				Test01_2(rawKey, data);
			}
		}

		private byte[] CreateRawKey()
		{
			return SecurityTools.CRandom.GetBytes(SecurityTools.CRandom.ChooseOne(new int[]
			{
				//16 * 1,
				16 * 2,
				16 * 3,
				16 * 4,
				16 * 5,

				//24 * 1,
				24 * 2,
				24 * 3,
				24 * 4,
				24 * 5,

				32 * 1,
				32 * 2,
				32 * 3,
				32 * 4,
				32 * 5,
			}
			));
		}

		private byte[] CreateEncryptableData()
		{
			return SecurityTools.CRandom.GetBytes(16 * SecurityTools.CRandom.GetRange(2, 100));
		}

		private void Test01_1(byte[] rawKey, byte[] data)
		{
			byte[] wkData = BinTools.GetSubBytes(data);

			CamelliaRingCBC crcbc = new CamelliaRingCBC(rawKey);

			crcbc.Encrypt(wkData);

			if (BinTools.Comp(data, wkData) == 0)
				throw null; // 違うでしょ。

			crcbc.Decrypt(wkData);

			if (BinTools.Comp(data, wkData) != 0)
				throw null; // bugged !!!
		}

		private void Test01_2(byte[] rawKey, byte[] data)
		{
			byte[] data1 = BinTools.GetSubBytes(data);
			byte[] data2 = EncryptBlock_Factory(rawKey, data);

			CamelliaRingCBC crcbc = new CamelliaRingCBC(rawKey);

			crcbc.Encrypt(data1);

			if (BinTools.Comp(data1, data2) != 0)
				throw null; // bugged !!!
		}

		private static byte[] EncryptBlock_Factory(byte[] rawKey, byte[] data)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string rawKeyFile = wd.MakePath();
				string rDataFile = wd.MakePath();
				string wDataFile = wd.MakePath();

				File.WriteAllBytes(rawKeyFile, rawKey);
				File.WriteAllBytes(rDataFile, data);

				ProcessTools.Start(
					@"C:\Factory\Labo\Tools\CamelliaRingCBC.exe",
					string.Format("/K \"{0}\" /R \"{1}\" /E /W \"{2}\"", rawKeyFile, rDataFile, wDataFile)
					)
					.WaitForExit();

				return File.ReadAllBytes(wDataFile);
			}
		}
	}
}
