﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Consts
	{
		public const string EVENT_PREFERENCE = "PREFERENCE";
		public const string EVENT_REGULAR = "REGULAR";
		public const string EVENT_BACKGROUND = "BACKGROUND";

		public const string DEFAULT_FONT_NAME = "メイリオ";

		public enum TimeFormat_e
		{
			デフォルト,
			デフォルト_曜日なし,
			シンプル,
			年月日時分秒,
			年月日時分秒_曜日なし,
			POSIX_TIME,
		};

		public enum FileDLMode_e
		{
			デフォルト,
			ダウンロード,
			リンクページを開く,
		};

		public enum PathClickMode_e
		{
			確認する,
			確認せずに開く,
			何もしない,
		}
	}
}
