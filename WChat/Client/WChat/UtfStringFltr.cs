using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class UtfStringFltr
	{
		private const string SERIALIZED_PREFIX = "$UTF8HEX_";

		public static string Serialize(string text)
		{
			if (Gnd.I.Sd.TimeLineTextUTF対応)
			{
				try
				{
					if (text.StartsWith(SERIALIZED_PREFIX))
						SystemTools.WriteLog("多重 Serialize しようとした可能性があります。");

					text = SERIALIZED_PREFIX + StringTools.ToHex(Encoding.UTF8.GetBytes(text));
				}
				catch (Exception e)
				{
					SystemTools.WriteLog(e);
				}
			}
			return text;
		}

		public static string Deserialize(string text)
		{
			if (Gnd.I.Sd.TimeLineTextUTF対応)
			{
				try
				{
					if (text.StartsWith(SERIALIZED_PREFIX) == false)
						throw new Exception("Serialize されていない文字列を Desirialize しようとしました。");

					string tmp = text;

					tmp = Encoding.UTF8.GetString(StringTools.Hex(tmp.Substring(SERIALIZED_PREFIX.Length)));
					tmp = ToUtfJString(tmp);

					text = tmp;
				}
				catch (Exception e)
				{
					SystemTools.WriteLog(e);
				}
			}
			return text;
		}

		/// <summary>
		/// JString.ToJString()によるノーマライズ？をすり抜けてしまうので、その代替
		/// </summary>
		/// <param name="text"></param>
		/// <returns></returns>
		public static string ToUtfJString(string text)
		{
			text = JString.ToLfOnly(text);
			return text;
		}
	}
}
