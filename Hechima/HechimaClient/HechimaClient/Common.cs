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
				while (Gnd.bgService.End() == false)
				{
					Thread.Sleep(100);
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

		public static string ToFairMessage(string message)
		{
			message = JString.toJString(message, true, false, false, true);

			message = message.Replace("　", "\t"); // 全角Spc -> Tab
			message = message.Trim();
			message = message.Replace("\t", "　"); // Tab -> 全角Spc

			return message;
		}

		public static long GetStamp()
		{
			return DateTimeToSec.Now.getDateTime();
		}

		public static string RemarkToTextBoxText(Remark remark)
		{
			StringBuilder buff = new StringBuilder();

			foreach (char rfChr in Gnd.setting.RemarkFormat)
			{
				switch (rfChr)
				{
					case 'R': buff.Append("\r\n"); break;
					case 'S': buff.Append(StampToTextBoxText(remark.Stamp)); break;
					case 'B': buff.Append(" "); break;
					case 'Z': buff.Append("　"); break;
					case 'I': buff.Append(IdentToTextBoxText(remark.Ident)); break;
					case 'M': buff.Append(remark.Message); break;

					default:
						throw null;
				}
			}
			return buff.ToString();
		}

		private static string StampToTextBoxText(long stamp)
		{
			stamp = Math.Max(stamp, 10000101000000L);
			stamp = Math.Min(stamp, 99991231235959L);

			int s = (int)(stamp % 100);
			stamp /= 100;
			int i = (int)(stamp % 100);
			stamp /= 100;
			int h = (int)(stamp % 100);
			stamp /= 100;
			int d = (int)(stamp % 100);
			stamp /= 100;
			int m = (int)(stamp % 100);
			int y = (int)(stamp / 100);

			if (Gnd.setting.ShowRemarkStampDate)
			{
				return "[" + y + "/" +
					StringTools.zPad(m, 2) + "/" +
					StringTools.zPad(d, 2) + " " +
					StringTools.zPad(h, 2) + ":" +
					StringTools.zPad(i, 2) + ":" +
					StringTools.zPad(s, 2) + "]";
			}
			return "[" +
				StringTools.zPad(h, 2) + ":" +
				StringTools.zPad(i, 2) + ":" +
				StringTools.zPad(s, 2) + "]";
		}

		private static string IdentToTextBoxText(string ident)
		{
			if (Gnd.setting.TripEnabled == false)
			{
				try // 2bs
				{
					int delimNameTripIndex = ident.IndexOf(Consts.DELIM_NAME_TRIP);

					string name = ident.Substring(0, delimNameTripIndex);
					string trip = ident.Substring(delimNameTripIndex + Consts.DELIM_NAME_TRIP.Length);

					if (Gnd.setting.IPDisabledWhenTripDisabled)
						ident = name;
					else
						ident = name + trip.Substring(trip.IndexOf(" @ "));
				}
				catch
				{ }
			}
			return ident;
		}
	}
}
