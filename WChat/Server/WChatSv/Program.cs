using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;

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
			Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

			Mutex procMtx = new Mutex(false, "{281697a9-5205-46a6-93d1-05d1eda9bb94}");

			{
				const string DIR = "tmp";

				if (Directory.Exists(DIR))
					Directory.Delete(DIR, true);

				Directory.CreateDirectory(DIR);
			}

			if (procMtx.WaitOne(0))
			{
				Gnd.I.DoLoad();
				//Gnd.I.ConsoleProcBegin(); // moved

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				// ログオフ・シャットダウンした場合、ここまで来ない。

				Gnd.I.ConsoleProcEnd();

				procMtx.ReleaseMutex();
			}
			procMtx.Close();
		}

		private static void ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			UnhandledError(e.Exception, "ThreadException");
		}

		private static void UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			UnhandledError(e.ExceptionObject, "UnhandledException");
		}

		private static void UnhandledError(object message, string reason)
		{
			try
			{
				MessageBox.Show(
					"[" + reason + "]" + message,
					"Chat Server - エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}
	}
}
