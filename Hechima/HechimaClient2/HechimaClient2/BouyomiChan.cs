using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class BouyomiChan
	{
		public const int COMMAND_READ_MESSAGE = 1;

		public const int SPEED_MIN = 50;
		public const int SPEED_MAX = 300;
		public const int SPEED_DEF = 0xffff;
		public const int TONE_MIN = 50;
		public const int TONE_MAX = 200;
		public const int TONE_DEF = 0xffff;
		public const int VOLUME_MIN = 0;
		public const int VOLUME_MAX = 100;
		public const int VOLUME_DEF = 0xffff;
		public const int VOICE_MIN = 0;
		public const int VOICE_MAX = 8;

		public const int CHARSET_SJIS = 2;

		public const string DEFAULT_MESSAGE = "ぼうよみンゴ";

		public string ServerDomain = "localhost";
		public int ServerPort = 50001;
		public int Speed = SPEED_DEF;
		public int Tone = TONE_DEF;
		public int Volume = VOLUME_DEF;
		public int Voice = VOICE_MIN;
		public string Message = DEFAULT_MESSAGE;

		public byte[] GetSendData()
		{
			byte[] bMsg = StringTools.ENCODING_SJIS.GetBytes(Message);
			int bMsgLen = bMsg.Length;
			List<byte> buff = new List<byte>();

			AddToBuff(buff, (COMMAND_READ_MESSAGE >> 0) & 0xff);
			AddToBuff(buff, (COMMAND_READ_MESSAGE >> 8) & 0xff);
			AddToBuff(buff, (Speed >> 0) & 0xff);
			AddToBuff(buff, (Speed >> 8) & 0xff);
			AddToBuff(buff, (Tone >> 0) & 0xff);
			AddToBuff(buff, (Tone >> 8) & 0xff);
			AddToBuff(buff, (Volume >> 0) & 0xff);
			AddToBuff(buff, (Volume >> 8) & 0xff);
			AddToBuff(buff, (Voice >> 0) & 0xff);
			AddToBuff(buff, (Voice >> 8) & 0xff);
			AddToBuff(buff, CHARSET_SJIS);
			AddToBuff(buff, (bMsgLen >> 0) & 0xff);
			AddToBuff(buff, (bMsgLen >> 8) & 0xff);
			AddToBuff(buff, (bMsgLen >> 16) & 0xff);
			AddToBuff(buff, (bMsgLen >> 24) & 0xff);
			buff.AddRange(bMsg);

			return buff.ToArray();
		}

		private void AddToBuff(List<byte> buff, int chr)
		{
			buff.Add((byte)chr);
		}
	}
}
