using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{5e3090f8-92ef-4fbf-b9b1-93008a19d209}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			// ログの初期化
			{
				string logFile = ProcMain.SelfFile + ".log";

				FileTools.Delete(logFile); // 前回のログを削除

				ProcMain.WriteLog = message => File.AppendAllLines(logFile, new string[] { "[" + DateTime.Now + "] " + message }, Encoding.UTF8); // ログに追記
			}

			// ----

			ProcMain.WriteLog("Log 1");
			ProcMain.WriteLog("Log 2");
			ProcMain.WriteLog("Log 3");
		}
	}
}
