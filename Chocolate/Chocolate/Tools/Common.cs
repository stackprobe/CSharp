using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Win32;

namespace Charlotte.Tools
{
	public class Common
	{
		public static string APP_IDENT;
		public static string APP_TITLE;

		public static string SelfFile;
		public static string SelfDir;

		public static ArgsReader ArgsReader;

		public static void CUIMain(Action<ArgsReader> mainFunc, string appIdent, string appTitle)
		{
			try
			{
				WriteLog = message => Console.WriteLine("[" + DateTime.Now + "] " + message);

				APP_IDENT = appIdent;
				APP_TITLE = appTitle;

				OnBoot();

				WorkingDir.Root = WorkingDir.CreateProcessRoot();

				ArgsReader = GetArgsReader();

				mainFunc(Common.ArgsReader);

				WorkingDir.Root.Dispose();
				WorkingDir.Root = null;
			}
			catch (Exception e)
			{
				WriteLog(e);
			}
		}

		public static void GUIMain(Func<Form> getMainForm, string appIdent, string appTitle)
		{
			Application.ThreadException += new ThreadExceptionEventHandler(ApplicationThreadException);
			AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomainUnhandledException);
			SystemEvents.SessionEnding += new SessionEndingEventHandler(SessionEnding);

			APP_IDENT = appIdent;
			APP_TITLE = appTitle;

			OnBoot();

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (procMutex.WaitOne(0))
			{
				if (GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
				{
					CheckSelfFile();
					Directory.SetCurrentDirectory(SelfDir);
					CheckLogonUserAndTmp();

					WorkingDir.Root = WorkingDir.CreateRoot();

					// core >

					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(getMainForm());

					// < core

					WorkingDir.Root.Dispose();
					WorkingDir.Root = null;

					GlobalProcMtx.Release();
				}
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		private static ArgsReader GetArgsReader()
		{
			ArgsReader ar = new ArgsReader(Environment.GetCommandLineArgs(), 1);

			if (ar.ArgIs("//R"))
			{
				ar = new ArgsReader(File.ReadAllLines(ar.NextArg(), StringTools.ENCODING_SJIS), 0);
			}
			return ar;
		}

		public static Action<object> WriteLog = message => { };

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

		public static void OnBoot()
		{
			SelfFile = Assembly.GetEntryAssembly().Location;
			SelfDir = Path.GetDirectoryName(SelfFile);
		}

		private static void CheckSelfFile()
		{
			string file = SelfFile;
			Encoding SJIS = Encoding.GetEncoding(932);

			if (file != SJIS.GetString(SJIS.GetBytes(file)))
			{
				MessageBox.Show(
					"Shift_JIS に変換出来ない文字を含むパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(4);
			}
			if (file.Substring(1, 2) != ":\\")
			{
				MessageBox.Show(
					"ネットワークパスからは実行できません。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(5);
			}
		}

		private static void CheckLogonUserAndTmp()
		{
			string userName = Environment.GetEnvironmentVariable("UserName");
			Encoding SJIS = Encoding.GetEncoding(932);

			if (
				userName == null ||
				userName == "" ||
				userName != SJIS.GetString(SJIS.GetBytes(userName)) ||
				userName.StartsWith(" ") ||
				userName.EndsWith(" ")
				)
			{
				MessageBox.Show(
					"Windows ログオン・ユーザー名に問題があります。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(7);
			}

			string tmp = Environment.GetEnvironmentVariable("TMP");

			if (
				tmp == null ||
				tmp == "" ||
				tmp != SJIS.GetString(SJIS.GetBytes(tmp)) ||
				//tmp.Length < 3 ||
				tmp.Length < 4 || // ルートDIR禁止
				tmp[1] != ':' ||
				tmp[2] != '\\' ||
				Directory.Exists(tmp) == false ||
				tmp.Contains(' ')
				)
			{
				MessageBox.Show(
					"環境変数 TMP に問題があります。",
					APP_TITLE + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);

				Environment.Exit(8);
			}
		}

		private class GlobalProcMtx
		{
			private static Mutex ProcMtx;

			public static bool Create(string ident, string title)
			{
				for (int c = 0; ; c++)
				{
					try
					{
						ProcMtx = new Mutex(false, @"Global\Global_" + Common.APP_IDENT);

						if (ProcMtx.WaitOne(0))
							break;

						ProcMtx.Close();
						ProcMtx = null;

						Common.WriteLog(new Exception());
					}
					catch (Exception e)
					{
						Common.WriteLog(e);
					}

					CloseProcMtx();

					if (8 < c)
					{
						System.Windows.Forms.MessageBox.Show(
							"Already started on the other logon session !",
							title + " / Error",
							System.Windows.Forms.MessageBoxButtons.OK,
							System.Windows.Forms.MessageBoxIcon.Error
							);

						return false;
					}

					System.Threading.Thread.Sleep(100);
				}
				return true;
			}

			public static void Release()
			{
				CloseProcMtx();
			}

			private static void CloseProcMtx()
			{
				try { ProcMtx.ReleaseMutex(); }
				catch { }

				try { ProcMtx.Close(); }
				catch { }

				ProcMtx = null;
			}
		}
	}
}
