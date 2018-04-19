using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Reflection;
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
			Common.GUIMain(() => new MainWin(), APP_IDENT, APP_TITLE);
		}

		public const string APP_IDENT = "{cb133c0e-badf-4af9-81ae-fc50bd1ffc79}";
		public const string APP_TITLE = "TTTT";
	}
}
