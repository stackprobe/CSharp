using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Security.Cryptography;

namespace Charlotte
{
	public class AppTools
	{
		public static void Browse(string domain = "localhost", int portNo = 80, string path = "")
		{
			string portPart;

			if (portNo == 80)
				portPart = "";
			else
				portPart = ":" + portNo;

			BrowseUrl("http://" + domain + portPart + "/" + path);
		}

		public static void BrowseUrl(string url)
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = url;

				Process.Start(psi);
			}
			catch (Exception e)
			{
				SystemTools.WriteLog(e);
			}
		}

		public static string GetUrlPath(string trackName, string path, bool dirFlag)
		{
			path = path.Replace('\\', '/');

			if (path.EndsWith("/"))
				path.Substring(0, path.Length - 1);

			if (2 <= path.Length && path[1] == ':') // ローカル
			{
				path = path.Substring(0, 1) + "$" + path.Substring(2);
			}
			else if (path.StartsWith("//")) // ネットワーク
			{
				path = "$$" + path.Substring(1);
			}
			path = StringTools.EncodeUrl(path);

			if (dirFlag)
				path += '/';

			return trackName + ":CLIENT/" + path;
		}

		private static SHA512 _sha512 = SHA512.Create();

		public static string GetUserName4Disp()
		{
			string ret = Gnd.I.Sd.UserName;

			if (Gnd.I.Sd.TripKey != "")
			{
				byte[] bHash = _sha512.ComputeHash(StringTools.ENCODING_SJIS.GetBytes(Gnd.I.Sd.TripKey));
				string code = GetTripCode(bHash);

				ret += Consts.TRIP_PREFIX + code;
			}
			return ret;
		}

		private static string GetTripCode(byte[] src)
		{
			StringBuilder buff = new StringBuilder();

			GTC_Src = src;
			GTC_RPos = 0;

			for (int c = 0; c < 4; c++)
			{
				buff.Append(GetTripChar(5));
				buff.Append(GetTripChar(6));
				buff.Append(GetTripChar(5));
			}
			if (GTC_RPos != src.Length) throw null; // 2bs
			GTC_Src = null;
			GTC_RPos = -1;
			return buff.ToString();
		}

		private const string TRIP_CHRS = StringTools.DIGIT + StringTools.ALPHA + StringTools.alpha;
		private static byte[] GTC_Src = null;
		private static int GTC_RPos = -1;

		private static char GetTripChar(int size)
		{
			UInt64 value = 0;

			for (int c = 0; c < size; c++)
			{
				value *= 256;
				value += (UInt64)GTC_Src[GTC_RPos];

				GTC_RPos++;
			}
			return TRIP_CHRS[(int)(value % (UInt64)TRIP_CHRS.Length)];
		}
	}
}
