using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			ProcMain.GUIMain(() => new MainWin(), APP_IDENT, APP_TITLE);
		}

		public const string APP_IDENT = "{dd3f480e-396d-4a58-ba8d-08af103df502}";
		public const string APP_TITLE = "SimplePaint";
	}
}
