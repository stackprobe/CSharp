using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;
using System.Drawing;
using System.Windows.Forms;

namespace Charlotte
{
	public class Gnd
	{
		public static bool DebugMode = true;
		public static bool MsdnDivFlag = true;
		public static bool SffBinaryMode = true;
		public static UInt64 Radix = 10; // 2 ～ 2^64-1
		public static int Basement = 100; // 0 ～ IMAX
		public static int BracketMin = 36; // 0 ～ 36
		public static int OperandLenMax = 100000; // 1 ～ IMAX
		public static int AnswerLenMax = 200000; // 1 ～ IMAX

		public static void LoadConf()
		{
			try
			{
				List<string> lines = LoadConfFile(StringTools.Combine(BootTools.SelfDir, "FatCalc.conf"));
				int c = 0;

				DebugMode = StringTools.ToFlag(lines[c++]);
				MsdnDivFlag = StringTools.ToFlag(lines[c++]);
				SffBinaryMode = StringTools.ToFlag(lines[c++]);
				Radix = UInt64Tools.ToUInt64(lines[c++], 2, UInt64.MaxValue, 10);
				Basement = IntTools.ToInt(lines[c++], 0, IntTools.IMAX, 100);
				BracketMin = IntTools.ToInt(lines[c++], 0, 36, 36);
				OperandLenMax = IntTools.ToInt(lines[c++], 1, IntTools.IMAX, 100000);
				AnswerLenMax = IntTools.ToInt(lines[c++], 1, IntTools.IMAX, 200000);
			}
			catch
			{ }
		}

		private static List<string> LoadConfFile(string file)
		{
			List<string> dest = new List<string>();

			foreach (string line in File.ReadAllLines(file, StringTools.ENCODING_SJIS))
				if (line != "" && line[0] != ';')
					dest.Add(line);

			return dest;
		}

		public static string ErrorMessage = "";
		public static List<object> Anythings = new List<object>();
		public static MainWin MainWin;

		// MainWin status {

		public static int MainWin_L = -1; // -1 == 未設定
		public static int MainWin_T;
		public static int MainWin_W;
		public static int MainWin_H;
		public static int MainTab_SelectedIndex;

		// }

		public static void LoadDataFile()
		{
			try
			{
				string[] lines = File.ReadAllLines(StringTools.Combine(BootTools.SelfDir, "FatCalc.dat"), StringTools.ENCODING_SJIS);
				int c = 0;

				MainWin_L = int.Parse(lines[c++]);
				MainWin_T = int.Parse(lines[c++]);
				MainWin_W = int.Parse(lines[c++]);
				MainWin_H = int.Parse(lines[c++]);
				MainTab_SelectedIndex = int.Parse(lines[c++]);
			}
			catch
			{ }
		}

		public static void SaveDataFile()
		{
			// option
			{
				MainWin.SaveStatus();
			}

			try
			{
				List<string> lines = new List<string>();

				lines.Add("" + MainWin_L);
				lines.Add("" + MainWin_T);
				lines.Add("" + MainWin_W);
				lines.Add("" + MainWin_H);
				lines.Add("" + MainTab_SelectedIndex);

				File.WriteAllLines(StringTools.Combine(BootTools.SelfDir, "FatCalc.dat"), lines, StringTools.ENCODING_SJIS);
			}
			catch
			{ }
		}
	}
}
