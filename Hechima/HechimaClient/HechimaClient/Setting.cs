using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;
using System.IO;
using System.Windows.Forms;

namespace Charlotte
{
	public class Setting
	{
		public string ServerDomain = "localhost";
		public int ServerPort = 52255;
		public int crypTunnelPort = 52525;
		public string Password = "aa9999[x22]";

		public bool BouyomiChanEnabled = false;
		public string BouyomiChanDomain = "localhost";
		public int BouyomiChanPort = 8080;

		public enum MessageTextEnterMode_e
		{
			CtrlEnterで送信_Enterで改行,
			Enterで送信_CtrlEnterで改行,
		};

		public MessageTextEnterMode_e MessageTextEnterMode = MessageTextEnterMode_e.CtrlEnterで送信_Enterで改行;

		/// <summary>
		/// R == 改行
		/// S == 日時
		/// B == ブランク
		/// I == ident
		/// M == メッセージ
		/// </summary>
		public string RemarkFormat = "RSBIRRMR";

		public string RemarksTextFontFamily = "メイリオ";
		public int RemarksTextFontSize = 10;
		public Color RemarksTextForeColor = Color.Black;
		public Color RemarksTextBackColor = Color.White;
		public Color MessageTextForeColor = Color.Black;
		public Color MessageTextBackColor = Color.White;

		public int MainWin_L;
		public int MainWin_T;
		public int MainWin_W = -1; // -1 == MainWin_LTWH 未設定
		public int MainWin_H;

		// ---- ロード・セーブ

		private string GetProgDataDir()
		{
			string dir = Path.Combine(FileTools.getProgramData(), @"cerulean charlotte\\HechimaClient");

			if (Directory.Exists(dir) == false)
				Directory.CreateDirectory(dir);

			return dir;
		}

		private string GetSaveFile()
		{
			return Path.Combine(GetProgDataDir(), "Setting.dat");
			//return Path.Combine(Program.selfDir, Path.GetFileNameWithoutExtension(Program.selfFile) + ".dat");
		}

		public void Load()
		{
			try
			{
				Load_Main();
			}
			catch
			{ }
		}

		private void Load_Main()
		{
			string[] lines = File.ReadAllLines(GetSaveFile(), StringTools.ENCODING_SJIS);
			int c = 0;

			// ----

			this.ServerDomain = lines[c++];
			this.ServerPort = int.Parse(lines[c++]);
			this.crypTunnelPort = int.Parse(lines[c++]);
			this.Password = lines[c++];

			this.BouyomiChanEnabled = int.Parse(lines[c++]) != 0;
			this.BouyomiChanDomain = lines[c++];
			this.BouyomiChanPort = int.Parse(lines[c++]);

			this.MessageTextEnterMode = (MessageTextEnterMode_e)int.Parse(lines[c++]);

			this.RemarkFormat = lines[c++];

			this.RemarksTextFontFamily = lines[c++];
			this.RemarksTextFontSize = int.Parse(lines[c++]);
			this.RemarksTextForeColor = Color.FromArgb(int.Parse(lines[c++]));
			this.RemarksTextBackColor = Color.FromArgb(int.Parse(lines[c++]));
			this.MessageTextForeColor = Color.FromArgb(int.Parse(lines[c++]));
			this.MessageTextBackColor = Color.FromArgb(int.Parse(lines[c++]));

			this.MainWin_L = int.Parse(lines[c++]);
			this.MainWin_T = int.Parse(lines[c++]);
			this.MainWin_W = int.Parse(lines[c++]);
			this.MainWin_H = int.Parse(lines[c++]);

			// 新しい項目、ここへ追加..

			// ----
		}

		public void Save()
		{
			List<string> lines = new List<string>();

			// ----

			lines.Add(this.ServerDomain);
			lines.Add("" + this.ServerPort);
			lines.Add("" + this.crypTunnelPort);
			lines.Add(this.Password);

			lines.Add("" + (this.BouyomiChanEnabled ? 1 : 0));
			lines.Add(this.BouyomiChanDomain);
			lines.Add("" + this.BouyomiChanPort);

			lines.Add("" + (int)this.MessageTextEnterMode);

			lines.Add(this.RemarkFormat);

			lines.Add(this.RemarksTextFontFamily);
			lines.Add("" + this.RemarksTextFontSize);
			lines.Add("" + this.RemarksTextForeColor.ToArgb());
			lines.Add("" + this.RemarksTextBackColor.ToArgb());
			lines.Add("" + this.MessageTextForeColor.ToArgb());
			lines.Add("" + this.MessageTextBackColor.ToArgb());

			lines.Add("" + this.MainWin_L);
			lines.Add("" + this.MainWin_T);
			lines.Add("" + this.MainWin_W);
			lines.Add("" + this.MainWin_H);

			// 新しい項目、ここへ追加..

			// ----

			File.WriteAllLines(GetSaveFile(), lines, StringTools.ENCODING_SJIS);
		}
	}
}
