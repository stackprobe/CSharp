using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Consts
	{
		public static readonly Encoding ENCODING_SJIS = Encoding.GetEncoding(932);

		public const int SEND_FILE_MAX = 100;
		public const int RECV_FILE_MAX = 100;
		public const int SEND_FILE_SIZE_MAX = 100000000; // 100 MB
		public const int RECV_FILE_SIZE_MAX = 100000000; // 100 MB
	}
}
