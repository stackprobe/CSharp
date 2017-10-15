using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;
using System.Numerics;

namespace Charlotte
{
	public class Common
	{
		public static void WaitToBgServiceEndable()
		{
			using (BusyDlg f = new BusyDlg(delegate()
			{
				while (Gnd.bgService.IsEndable() == false)
				{
					Thread.Sleep(100);
					Gnd.bgService.Perform();
				}
			}
			))
			{
				f.ShowDialog();
			}
		}

		private static readonly string TRIP_CHRS = StringTools.DIGIT + StringTools.ALPHA + StringTools.alpha;

		public static string ToTrip(string src)
		{
			byte[] bSrc = SecurityTools.getSHA512(StringTools.ENCODING_SJIS.GetBytes(src));

			// 正の値にする。
			{
				byte[] bSrc2 = new byte[bSrc.Length + 1];

				Array.Copy(bSrc, 0, bSrc2, 0, bSrc.Length); // bSrc2 = bSrc + { 0x00 }
				bSrc = bSrc2;
			}

			BigInteger biVal = new BigInteger(bSrc);

			StringBuilder buff = new StringBuilder();

			for (int i = 0; i < 22; i++)
			{
				buff.Append(TRIP_CHRS[(biVal % TRIP_CHRS.Length).ToByteArray()[0]]);
				biVal /= TRIP_CHRS.Length;
			}
			return buff.ToString();
		}
	}
}
