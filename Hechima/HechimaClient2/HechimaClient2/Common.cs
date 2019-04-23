using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Charlotte.Tools;
using System.Numerics;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Drawing.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class Common
	{
		public static void WaitToBgServiceEnded(bool noDlg = false)
		{
			Gnd.Logger.writeLine("WaitToBgServiceEnded.1"); // test

			if (noDlg)
			{
				Gnd.Logger.writeLine("WaitToBgServiceEnded.2"); // test

				while (Gnd.bgService.End() == false)
				{
					Thread.Sleep(100);
				}
				Gnd.Logger.writeLine("WaitToBgServiceEnded.2.1"); // test
			}
			else
			{
				Gnd.Logger.writeLine("WaitToBgServiceEnded.3"); // test

				using (BusyDlg f = new BusyDlg(delegate()
				{
					Gnd.Logger.writeLine("WaitToBgServiceEnded.3.1"); // test

					while (Gnd.bgService.End() == false)
					{
						Thread.Sleep(100);
					}
					Gnd.Logger.writeLine("WaitToBgServiceEnded.3.2"); // test
				}
				))
				{
					Gnd.Logger.writeLine("WaitToBgServiceEnded.3.3"); // test
					f.ShowDialog();
					Gnd.Logger.writeLine("WaitToBgServiceEnded.3.4"); // test
				}
				Gnd.Logger.writeLine("WaitToBgServiceEnded.3.5"); // test
			}
			Gnd.Logger.writeLine("WaitToBgServiceEnded.4"); // test
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

		public static string ToHexString(Color color)
		{
			return StringTools.toHex(new byte[]
			{
				color.R,
				color.G,
				color.B,
			});
		}

		public static Color ToColorHex(string src)
		{
			byte[] bSrc = StringTools.hex(src);

			return Color.FromArgb(
				(int)bSrc[0],
				(int)bSrc[1],
				(int)bSrc[2]
				);
		}

		public static string ToString(Color[] colors)
		{
			List<string> dest = new List<string>();

			foreach (Color color in colors)
				dest.Add(ToHexString(color));

			return string.Join(":", dest);
		}

		public static Color[] ToColors(string src)
		{
			List<Color> dest = new List<Color>();

			foreach (string token in src.Split(':'))
				dest.Add(ToColorHex(token));

			return dest.ToArray();
		}

		public static string DateTimeToString(long dateTime)
		{
			return ("" + dateTime)
				.Insert(12, ":")
				.Insert(10, ":")
				.Insert(8, " ")
				.Insert(6, "/")
				.Insert(4, "/");
		}

		public static void SetTextBoxBorderStyle(TextBox tb, bool flat, bool valChgScrToEnd = false)
		{
			BorderStyle val;

			if (flat)
				val = BorderStyle.None;
			else
				val = BorderStyle.Fixed3D;

			if (tb.BorderStyle != val)
			{
				tb.BorderStyle = val;

				if (valChgScrToEnd)
				{
					tb.SelectionStart = tb.TextLength;
					tb.ScrollToCaret();
				}
			}
		}

		public static string[] GetAllFontFamily()
		{
			List<string> dest = new List<string>();

			try
			{
				foreach (FontFamily family in new InstalledFontCollection().Families)
				{
					dest.Add(family.Name);
				}
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}

			if (dest.Count == 0)
				dest.Add("メイリオ");

			return dest.ToArray();
		}

		public static bool IsBrightColor(Color color)
		{
			return 0xc0 * 3 < color.R + color.G + color.B;
		}

		public static List<MemberFont> ToMemberFonts(string src)
		{
			List<MemberFont> dest = new List<MemberFont>();

			foreach (string token in MemberFont_AttSt.tokenize(src))
			{
				MemberFont mf = new MemberFont();
				mf.SetString(token);
				dest.Add(mf);
			}
			return dest;
		}

		public static string ToString(List<MemberFont> src)
		{
			List<string> dest = new List<string>();

			foreach (MemberFont mf in src)
			{
				dest.Add(mf.GetString());
			}
			return MemberFont_AttSt.untokenize(dest.ToArray());
		}

		private static AttachString MemberFont_AttSt = new AttachString('/', '$', 'S');

		#region TaskBarFlash

		[DllImport("user32.dll")]
		static extern Int32 FlashWindowEx(ref FLASHWINFO pwfi);

		[StructLayout(LayoutKind.Sequential)]
		public struct FLASHWINFO
		{
			public UInt32 cbSize;    // FLASHWINFO構造体のサイズ
			public IntPtr hwnd;      // 点滅対象のウィンドウ・ハンドル
			public UInt32 dwFlags;   // 以下の「FLASHW_XXX」のいずれか
			public UInt32 uCount;    // 点滅する回数
			public UInt32 dwTimeout; // 点滅する間隔（ミリ秒単位）
		}

		// 点滅を止める
		public const UInt32 FLASHW_STOP = 0;
		// タイトルバーを点滅させる
		public const UInt32 FLASHW_CAPTION = 1;
		// タスクバー・ボタンを点滅させる
		public const UInt32 FLASHW_TRAY = 2;
		// タスクバー・ボタンとタイトルバーを点滅させる
		public const UInt32 FLASHW_ALL = 3;
		// FLASHW_STOPが指定されるまでずっと点滅させる
		public const UInt32 FLASHW_TIMER = 4;
		// ウィンドウが最前面に来るまでずっと点滅させる
		public const UInt32 FLASHW_TIMERNOFG = 12;

		public static void TaskBarFlash(Form win)
		{
			try
			{
				FLASHWINFO fwi = new FLASHWINFO();

				fwi.cbSize = Convert.ToUInt32(Marshal.SizeOf(fwi));
				fwi.hwnd = win.Handle;
				fwi.dwFlags = FLASHW_TRAY | FLASHW_TIMERNOFG;
				fwi.uCount = 0;
				fwi.dwTimeout = 0;

				FlashWindowEx(ref fwi);
			}
			catch
			{ }
		}

		#endregion

		public static void BrowseUrl(string url)
		{
			Gnd.Logger.writeLine("BrowsUrl.1");
			Gnd.Logger.writeLine("url: " + url);

			try
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = url;

				Process.Start(psi);
			}
			catch (Exception e)
			{
				Gnd.Logger.writeLine(e);
			}
			Gnd.Logger.writeLine("BrowsUrl.2");
		}
	}
}
