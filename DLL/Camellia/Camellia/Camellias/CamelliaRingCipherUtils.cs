using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Camellias
{
	public static class CamelliaRingCipherUtils
	{
		private const int X_EXP_MIN = 20;
		private const int X_EXP_MAX = 50;

		private const int X_PTN_SIZE = 1024 * 1024;
		private const int X_PTN_EXP = 20;

		public static byte[] GenerateRawKey(string passphrase)
		{
			byte[] bPassphrase = StringTools.ENCODING_SJIS.GetBytes(passphrase);

			if (
				5 <= bPassphrase.Length &&
				bPassphrase[bPassphrase.Length - 1] == 0x5d && // ']'
				0x30 <= bPassphrase[bPassphrase.Length - 2] && bPassphrase[bPassphrase.Length - 2] <= 0x39 && // [0-9]
				0x30 <= bPassphrase[bPassphrase.Length - 3] && bPassphrase[bPassphrase.Length - 3] <= 0x39 && // [0-9]
				//bPassphrase[bPassphrase.Length - 4] // any char
				bPassphrase[bPassphrase.Length - 5] == 0x5b // '['
				)
			{
				byte xChr = bPassphrase[bPassphrase.Length - 4];
				int xExp =
						((bPassphrase[bPassphrase.Length - 3] & 0xff) - 0x30) * 10 +
						((bPassphrase[bPassphrase.Length - 2] & 0xff) - 0x30);

				if (xExp < X_EXP_MIN || X_EXP_MAX < xExp)
					throw new ArgumentException();

				byte[] xPtn = new byte[X_PTN_SIZE];

				for (int index = 0; index < X_PTN_SIZE; index++)
					xPtn[index] = xChr;

				int xNum = 1 << (xExp - X_PTN_EXP);

				return SecurityTools.GetSHA512(writer =>
				{
					writer(bPassphrase, 0, bPassphrase.Length - 5);

					for (int count = 0; count < xNum; count++)
						writer(xPtn, 0, X_PTN_SIZE);
				});
			}
			else
			{
				return SecurityTools.GetSHA512(bPassphrase);
			}
		}
	}
}
