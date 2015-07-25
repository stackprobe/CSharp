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
			BootTools.OnBoot();

			Application.ThreadException += new ThreadExceptionEventHandler(ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(UnhandledException);

			SystemTools.WL_Enabled = true;

			Mutex procMtx = new Mutex(false, "{3884a7c2-49e5-4211-9c1b-cbc2c6890b95}");

			{
				const string DIR = "tmp";

				if (Directory.Exists(DIR))
					Directory.Delete(DIR, true);

				Directory.CreateDirectory(DIR);
			}

			Gnd.I.Sd.Load();
			Gnd.I.Sd.PostLoad();

			if (procMtx.WaitOne(0))
			{
				{
					Gnd.I.ChatMan = new ChatMan();
					Gnd.I.FileSvMan.Begin();
					Gnd.I.NamedTrackHttpMan.Begin();
					Gnd.I.NamedTrackMan.Begin();
					Gnd.I.RevClientMan.Begin();
				}

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				// ログオフ・シャットダウンした場合、ここまで来ない。

				BusyDlg.Perform(delegate
				{
					BusyDlg.I.SetMessage("コマンドの完了を待っています。");

					Gnd.I.ChatMan.End();

					BusyDlg.I.SetMessage("ログアウトしています。");

					Gnd.I.ChatMan.LogoutCommand(Gnd.I.Sd.Ident);
					Gnd.I.ChatMan.Destroy();
					Gnd.I.ChatMan = null;

					BusyDlg.I.SetMessage("ファイル転送サーバーを停止しています。");

					Gnd.I.FileSvMan.End();

					BusyDlg.I.SetMessage("ファイル転送クライアントを停止しています。");

					Gnd.I.NamedTrackHttpMan.End();

					BusyDlg.I.SetMessage("ファイル転送・中継サーバーを停止しています。(NT)");

					Gnd.I.NamedTrackMan.End();

					BusyDlg.I.SetMessage("ファイル転送・中継サーバーを停止しています。(RC)");

					Gnd.I.RevClientMan.End();
				});

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
				message = "[" + reason + "] " + message;
				SystemTools.WriteLog(message);
			}
			catch
			{ }

			Environment.Exit(1);
		}
	}
}
