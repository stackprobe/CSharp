using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public static class Consts
	{
		public const string S_DUMMY = "-";

		public const string DELIM_NAME_TRIP = "◆";

		/// <summary>
		/// 旧バージョンのクライアントを弾きたい時に文字列を変更するのだ！
		/// </summary>
		public const string PASSWORD_TRAILER = "-SERIAL=100[x22]";

		public static readonly Color[] COLORFUL_DAYS_FORE_COLORS = Common.ToColors(
			"576765:7e6a12:033a62:7b070f:5a0e33:005329:00585d:506531:794823:7d4c5f:3f2853:232527"
			);
		public static readonly Color[] COLORFUL_DAYS_BACK_COLORS = Common.ToColors(
			"d6e6e5:fde991:83bae1:fb878f:da8eb2:7fd3a8:7fd8dd:d0e4b0:f8c7a3:fccbdf:bea8d2:a2a5a7"
			);
	}
}
