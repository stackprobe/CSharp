using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using Microsoft.Win32;
using System.Text;
using System.IO;
using System.Reflection;

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
			Application.ThreadException += new ThreadExceptionEventHandler(ApplicationThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			//OnBoot();

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (HandleProcMutex(procMutex))
			{
				StopRunEv.WaitOne(0); // reset

				//if (GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
				{
					//CheckSelfDir();
					//Directory.SetCurrentDirectory(SelfDir);
					//CheckAloneExe();
					//CheckLogonUser();

					//Gnd.Load(Consts.SettingFile);

					// orig >

					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new MainWin());

					// < orig

					//Gnd.Save(Consts.SettingFile);

					//WorkingDir.Root.Dispose();
					//WorkingDir.Root = null;

					//GlobalProcMtx.Release();
				}
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		public static EventWaitHandle StopRunEv = new EventWaitHandle(false, EventResetMode.AutoReset, APP_IDENT + "_Stop");

		private static bool HandleProcMutex(Mutex procMutex)
		{
			bool ret = procMutex.WaitOne(0);

			if (ret == false)
			{
				using (Mutex m = new Mutex(false, APP_IDENT + "_Boot"))
				{
					if (m.WaitOne(0))
					{
						for (int c = 0; c < 3; c++)
						{
							StopRunEv.Set();
							ret = procMutex.WaitOne(2000);

							if (ret)
								break;
						}
						m.ReleaseMutex();
					}
				}
			}
			return ret;
		}

		public const string APP_IDENT = "{9e4a67f8-5a4b-4b1d-94e0-ed412e771568}";
		public const string APP_TITLE = "NoScreenSaver";

		private static void ApplicationThreadException(object sender, ThreadExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[Application_ThreadException]\n" + e.Exception,
					APP_TITLE + " / Error",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
			catch
			{ }

			Environment.Exit(1);
		}

		private static void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
		{
			try
			{
				MessageBox.Show(
					"[CurrentDomain_UnhandledException]\n" + e.ExceptionObject,
					APP_TITLE + " / Error",
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
	}
}
