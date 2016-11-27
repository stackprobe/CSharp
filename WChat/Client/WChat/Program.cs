using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.IO;
using Microsoft.Win32;
using System.Text;

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

			Application.ThreadException += new ThreadExceptionEventHandler(Application_ThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			Mutex procMtx = new Mutex(false, "{3884a7c2-49e5-4211-9c1b-cbc2c6890b95}");

			if (procMtx.WaitOne(0) && GlobalProcMtx.Create("{76d55e1a-41ea-433b-820a-7e1af73fe90a}", APP_TITLE))
			{
				CheckSelfDir();
				CheckCopiedExe();

				{
					const string DIR = "tmp";

					if (Directory.Exists(DIR))
						Directory.Delete(DIR, true);

					Directory.CreateDirectory(DIR);
				}

				SystemTools.WL_Start();

#if false // test
				SystemTools.WriteLog("LOG_TEST_01");
				SystemTools.WriteLog("LOG_TEST_02");
				SystemTools.WriteLog("LOG_TEST_03");
#endif

				Gnd.I.Sd.Load();
				Gnd.I.Sd.PostLoad();

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

					BusyDlg.I.SetMessage("プログラムを終了しています。");
				});

				GlobalProcMtx.Release();
				procMtx.ReleaseMutex();
			}
			procMtx.Close();
		}

		public const string APP_TITLE = "Chat";

		private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(2);
		}

		private static void SessionEnding(object sender, SessionEndingEventArgs e)
		{
			Environment.Exit(3);
		}

		private static void CheckSelfDir()
		{
			string dir = BootTools.SelfDir;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (dir != SJIS.GetString(SJIS.GetBytes(dir)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (dir.StartsWith("\\\\"))
			{
				MessageBox.Show(
					"ネットワークフォルダからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		private static void CheckCopiedExe()
		{
			if (File.Exists("JIS0208.txt")) // リリースに含まれるファイル
				return;

			if (Directory.Exists(@"..\Debug")) // ? devenv
				return;

			MessageBox.Show(
				"WHY AM I ALONE ?",
				"",
				MessageBoxButtons.OK,
				MessageBoxIcon.Error
				);

			Environment.Exit(6);
		}
	}
}
