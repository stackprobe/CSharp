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

			Mutex procMtx = new Mutex(false, "{281697a9-5205-46a6-93d1-05d1eda9bb94}");

			if (procMtx.WaitOne(0) && GlobalProcMtx.Create("{b14c50d4-1392-48c7-a8d4-5d648a9a0afd}", APP_TITLE))
			{
				CheckSelfDir();
				CheckCopiedExe();

				{
					const string DIR = "tmp";

					if (Directory.Exists(DIR))
						Directory.Delete(DIR, true);

					Directory.CreateDirectory(DIR);
				}

				//Gnd.I.Init_Files(); // moved -> MainWin_Shown()
				Gnd.I.DoLoad();
				//Gnd.I.ConsoleProcBegin(); // moved

				// orig >

				Application.EnableVisualStyles();
				Application.SetCompatibleTextRenderingDefault(false);
				Application.Run(new MainWin());

				// < orig

				// ログオフ・シャットダウンした場合、ここまで来ない。

				Gnd.I.ConsoleProcEnd();

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
