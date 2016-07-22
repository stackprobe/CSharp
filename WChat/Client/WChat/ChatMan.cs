using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class ChatMan
	{
		private static string _file;

		public static string GetFile()
		{
			if (_file == null)
			{
				_file = "Chat.exe";

				FJammer.Decode(_file);

				if (File.Exists(_file) == false)
					_file = @"C:\Factory\SubTools\Chat\Chat.exe";
			}
			return _file;
		}

		private ProcessMan ProcMan = new ProcessMan();
		private WorkDir WorkDir = new WorkDir();
		private string OutputFile;

		// ---- command ----

		private void BeforeCommand()
		{
			this.WorkDir.Clear();
			this.OutputFile = null;
		}

		public void RemarkCommand(string stamp, string message)
		{
			this.BeforeCommand();

			string file = this.WorkDir.MakePath();

			File.WriteAllLines(file, new string[]
			{
				"/R",
				stamp,
				message,
			},
			StringTools.ENCODING_SJIS);
			this.DoCommand(file);
		}

		public void TimeLineCommand(string btnStmp, string endStmp)
		{
			this.BeforeCommand();

			string file = this.WorkDir.MakePath();

			this.OutputFile = this.WorkDir.MakePath();

			File.WriteAllLines(file, new string[]
			{
				"/T",
				btnStmp,
				endStmp,
				this.OutputFile,
			},
			StringTools.ENCODING_SJIS);
			this.DoCommand(file);
		}

		public void HeartbeatCommand(string ident, string message)
		{
			this.BeforeCommand();

			string file = this.WorkDir.MakePath();

			this.OutputFile = this.WorkDir.MakePath();

			File.WriteAllLines(file, new string[]
			{
				"/H",
				ident,
				message,
				this.OutputFile,
			},
			StringTools.ENCODING_SJIS);
			this.DoCommand(file);
		}

		public void LogoutCommand(string ident)
		{
			this.BeforeCommand();

			string file = this.WorkDir.MakePath();

			File.WriteAllLines(file, new string[]
			{
				"/O",
				ident,
			},
			StringTools.ENCODING_SJIS);
			this.DoCommand(file);
		}

		public void ServerTimeDiffCommand()
		{
			this.BeforeCommand();

			string file = this.WorkDir.MakePath();

			this.OutputFile = this.WorkDir.MakePath();

			File.WriteAllLines(file, new string[]
			{
				"/STD",
				this.OutputFile,
			},
			StringTools.ENCODING_SJIS);
			this.DoCommand(file);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="bcDomain">棒読みちゃんドメイン</param>
		/// <param name="bcPort">棒読みちゃんポート番号</param>
		/// <param name="message">メッセージ</param>
		/// <param name="speedPml"></param>
		/// <param name="tonePml"></param>
		/// <param name="volumePml"></param>
		/// <param name="voice"></param>
		public void BouyomichanCommand(string bcDomain, int bcPort, string message, int speedPml, int tonePml, int volumePml, int voice)
		{
			this.BeforeCommand();

			message = StringTools.ReplaceChar(message, "\t\r\n", ' ');
			message = JString.ToJString(message, true, false, false, true);
			message = message.Trim();

			string file = this.WorkDir.MakePath();

			File.WriteAllLines(file, new string[]
			{
				"/B",
				message,
				"" + speedPml,
				"" + tonePml,
				"" + volumePml,
				"" + voice,
			},
			StringTools.ENCODING_SJIS);
			this.DoCommand(file, bcDomain, bcPort);
		}

		private void DoCommand(string file)
		{
			this.DoCommand(file, Gnd.I.Sd.ServerDomain, Gnd.I.Sd.ServerPort);
		}

		private void DoCommand(string file, string serverDomain, int serverPort)
		{
			this.ProcMan.Start(
				GetFile(),
				"/S " + serverDomain + " /P " + serverPort + " //R \"" + file + "\""
				);
		}

		// ----

		public bool IsEnd()
		{
			return this.ProcMan.IsEnd();
		}

		public void End()
		{
			this.ProcMan.End();
		}

		public bool HasOutput()
		{
			if (this.OutputFile == null)
				return false;

			return File.Exists(this.OutputFile);
		}

		public string[] GetOutput()
		{
			if (this.HasOutput() == false)
				return null;

			return File.ReadAllLines(this.OutputFile, StringTools.ENCODING_SJIS);
		}

		/// <summary>
		/// プロセス終了時に呼ぶこと。
		/// </summary>
		public void Destroy()
		{
			this.ProcMan.End();

			this.WorkDir.Destroy();
			this.WorkDir = null;
		}
	}
}
