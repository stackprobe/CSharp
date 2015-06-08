using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Text;
using System.Security.Cryptography;

namespace Charlotte
{
	public class SystemTools
	{
		public static bool WL_Enabled = false;
		private static int WL_Count = 0;

		public static void WriteLog(object e)
		{
			try
			{
				if (WL_Enabled == false)
					return;

				{
					string message = "" + e;

					int index = StringTools.IndexOfChar(message, "\r\n");

					if (index != -1)
						message = message.Substring(0, index);

					EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, delegate
					{
						Gnd.I.MainWin.SetStatusMessage(message);
					});
				}

				if (1000 < WL_Count)
					return;

				using (StreamWriter sw = new StreamWriter(GetLogFile(), 1 <= WL_Count, StringTools.ENCODING_SJIS))
				{
					sw.WriteLine("[" + DateTime.Now + "." + WL_Count + "] " + e);
				}
				WL_Count++;
			}
			catch
			{ }
		}

		public static string GetLogFile()
		{
			return GetSelfFile() + ".log";
		}

		public static string GetSaveDataFile()
		{
			return GetSelfFile() + ".dat";
		}

		public static string GetSelfFile()
		{
			return Environment.GetCommandLineArgs()[0];
		}

		public static string GetTmp()
		{
			return GetEnv("TMP", @"C:\temp");
		}

		public static string GetEnv(string name, string defval)
		{
			try
			{
				string value = Environment.GetEnvironmentVariable(name);

				if (value == null || value.Length == 0)
					value = defval;

				return value;
			}
			catch
			{
				return defval;
			}
		}

		public class Discontinued : Exception
		{ }

		public static List<string> GetAllFontFamily()
		{
			List<string> result = new List<string>();

			foreach (FontFamily family in new InstalledFontCollection().Families)
			{
				result.Add(family.Name);
			}
			return result;
		}

		public static bool IsBrightColor(Color color)
		{
			return 0xc0 * 3 < color.R + color.G + color.B;
		}

		public delegate bool IsSame_d<T>(T a, T b);

		private static RNGCryptoServiceProvider _rngc = new RNGCryptoServiceProvider();

		public static uint GetCryptoRand()
		{
			byte[] buff = new byte[4];

			_rngc.GetBytes(buff);

			return
				((uint)buff[0] << 24) |
				((uint)buff[1] << 16) |
				((uint)buff[2] << 8) |
				((uint)buff[3] << 0);
		}

		public static uint GetCryptoRand(uint modulo)
		{
			return GetCryptoRand() % modulo; // FIXME
		}

		public static uint GetCryptoRand(uint minval, uint maxval)
		{
			return GetCryptoRand(maxval + 1 - minval) + minval;
		}
	}
}
