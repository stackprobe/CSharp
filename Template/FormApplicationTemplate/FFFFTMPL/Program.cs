﻿using System;
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

			OnBoot();

			Mutex procMutex = new Mutex(false, APP_IDENT);

			if (procMutex.WaitOne(0))
			{
				if (GlobalProcMtx.Create(APP_IDENT, APP_TITLE))
				{
					CheckSelfFile();
					Directory.SetCurrentDirectory(SelfDir);
					CheckAloneExe();
					CheckLogonUserAndTmp();

					//WorkingDir.Root = WorkingDir.CreateRoot();

					Gnd.I = new Gnd();

					Gnd.I.LoadConf(Gnd.I.ConfFile);
					Gnd.I.Load(Gnd.I.SettingFile);

					// orig >

					Application.EnableVisualStyles();
					Application.SetCompatibleTextRenderingDefault(false);
					Application.Run(new MainWin());

					// < orig

					Gnd.I.Save(Gnd.I.SettingFile);

					//WorkingDir.Root.Dispose();
					//WorkingDir.Root = null;

					GlobalProcMtx.Release();
				}
				procMutex.ReleaseMutex();
			}
			procMutex.Close();
		}

		public static void WriteLog(object message)
		{
			//LogQueue.I.Add(message);
		}

		public const string APP_IDENT = "{40d6bc7d-352a-416b-8fae-7a639e07035e}";
		public const string APP_TITLE = "FFFFTMPL";

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

		public static string SelfFile;
		public static string SelfDir;

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

		private static void CheckAloneExe()
		{
			if (File.Exists("FFFFTMPL.sig")) // リリースに含まれるファイル
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
	}
}
