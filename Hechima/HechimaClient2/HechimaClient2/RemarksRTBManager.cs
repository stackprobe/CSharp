using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	public class RemarksRTBManager
	{
		public RTBManager RTBMgr;

		public RemarksRTBManager(RTBManager rtbMgr)
		{
			this.RTBMgr = rtbMgr;
			this.RTBMgr.Clear();
		}

		private string SavedRTF = null;

		public bool IsSaved()
		{
			return this.SavedRTF != null;
		}

		public void Save()
		{
			this.SavedRTF = this.RTBMgr.RTB.Rtf;
		}

		/// <summary>
		/// 発言を追加する。
		/// </summary>
		/// <param name="remarks">追加する発言</param>
		/// <param name="loadFlag">セーブしていた場合、ロードして追加してセーブクリアする</param>
		public void Add(List<Remark> remarks, bool loadFlag = false)
		{
			List<RTBManager.Token> tokens = this.RemarksToTokens(remarks);

			if (loadFlag && this.SavedRTF != null)
			{
				this.RTBMgr.Join(this.SavedRTF, tokens);
				this.SavedRTF = null;
			}
			else
			{
				this.RTBMgr.Add(tokens);
			}
		}

		private List<RTBManager.Token> RemarksToTokens(List<Remark> remarks)
		{
			List<RTBManager.Token> dest = new List<RTBManager.Token>();

			foreach (Remark remark in remarks)
			{
				this.AddTo(dest, remark);
			}
			return dest;
		}

		private void AddTo(List<RTBManager.Token> dest, Remark remark)
		{
			string rf = Gnd.setting.RemarkFormat;

			rf = StringTools.replaceLoop(rf, "RR", "r", 10);

			foreach (char rfChr in rf)
			{
				switch (rfChr)
				{
					case 'r':
						{
							dest.Add(new RTBManager.Token(
								new Font(Gnd.setting.RemarksTextDefaultFontFamily, Gnd.setting.RemarksTextDefaultFontSize),
								Gnd.setting.RemarksTextDefaultFontColor,
								"\n\n"
								));
						}
						break;

					case 'R':
						{
							dest.Add(new RTBManager.Token(
								new Font(Gnd.setting.RemarksTextDefaultFontFamily, Gnd.setting.RemarksTextDefaultFontSize),
								Gnd.setting.RemarksTextDefaultFontColor,
								"\n"
								));
						}
						break;

					case 'S':
						{
							dest.Add(new RTBManager.Token(
								new Font("メイリオ", 10f),
								Color.Black,
								StampToTextBoxText(remark.Stamp)
								));
						}
						break;

					case 'B':
						{
							dest.Add(new RTBManager.Token(
								new Font(Gnd.setting.RemarksTextDefaultFontFamily, Gnd.setting.RemarksTextDefaultFontSize),
								Gnd.setting.RemarksTextDefaultFontColor,
								" " // 半角空白
								));
						}
						break;

					case 'Z':
						{
							dest.Add(new RTBManager.Token(
								new Font(Gnd.setting.RemarksTextDefaultFontFamily, Gnd.setting.RemarksTextDefaultFontSize),
								Gnd.setting.RemarksTextDefaultFontColor,
								"　" // 全角空白
								));
						}
						break;

					case 'I':
						{
							dest.Add(new RTBManager.Token(
								new Font("メイリオ", 10f),
								Color.Black,
								IdentToTextBoxText(remark.Ident)
								));
						}
						break;

					case 'M':
						{
							dest.Add(new RTBManager.Token(
								new Font("メイリオ", 10f),
								Color.Black,
								remark.Message
								));
						}
						break;

					default:
						throw null;
				}
			}
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
