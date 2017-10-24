using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Consts
	{
		public const string S_DUMMY = "-";

		public const string DELIM_NAME_TRIP = "◆";

		/// <summary>
		/// 旧バージョンのクライアントを弾きたい時に文字列を変更するのだ！
		/// </summary>
		public const string PASSWORD_TRAILER = "-SERIAL=100[x22]";

		public static readonly Color[] COLORFUL_DAYS_FORE_COLORS = Common.ToColors(
			"00100e:6c5800:a1d8ff:ff8b93:ffb3d7:abffd4:a1faff:1f3400:562500:310013:ebd5ff:fafdff"
			);
		public static readonly Color[] COLORFUL_DAYS_BACK_COLORS = Common.ToColors(
			"aececb:fcd424:0775c4:f70f1f:b51d66:00a752:00b1bb:a1ca62:f29047:fa98bf:7e51a6:464b4f"
			);
	}
}
