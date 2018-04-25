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
		public string Password = "aa9999x22x";

		public bool BouyomiChanEnabled = false;
		public string BouyomiChanDomain = "localhost";
		public int BouyomiChanPort = 50001;

		public enum MessageTextEnterMode_e
		{
			Enterで送信_CtrlEnterで改行,
			CtrlEnterで送信_Enterで改行,
		};

		public MessageTextEnterMode_e MessageTextEnterMode = MessageTextEnterMode_e.Enterで送信_CtrlEnterで改行;

		/// <summary>
		/// R == 改行
		/// S == 日時
		/// B == ブランク
		/// Z == 全角スペース
		/// I == ident
		/// M == メッセージ
		/// </summary>
		public string RemarkFormat = "RSBIRZMR";

		public string RemarksTextDefaultFontFamily = "メイリオ";
		public int RemarksTextDefaultFontSize = 8;//10;
		public Color RemarksTextDefaultFontColor = Color.FromArgb(0, 48, 0);
		public Color RemarksTextBackColor = Color.White;
		public Color MessageTextForeColor = Color.Black;
		public Color MessageTextBackColor = Color.White;

		public int MainWin_L;
		public int MainWin_T;
		public int MainWin_W = -1; // -1 == MainWin_LTWH 未設定
		public int MainWin_H;

		public string UserName = "名無しさん" + SecurityTools.getCRandUInt();
		public string UserTrip = StringTools.toHex(SecurityTools.getCRand(16));

		public bool TripEnabled = false;
		public bool ShowRemarkStampDate = false;

		public int BouyomiChanSpeed = (BouyomiChan.SPEED_MIN + BouyomiChan.SPEED_MAX) / 2;
		public int BouyomiChanTone = (BouyomiChan.TONE_MIN + BouyomiChan.TONE_MAX) / 2;
		public int BouyomiChanVolume = (BouyomiChan.VOLUME_MIN + BouyomiChan.VOLUME_MAX) / 2;
		public bool BouyomiChanSpeedUseDef = true;
		public bool BouyomiChanToneUseDef = true;
		public bool BouyomiChanVolumeUseDef = true;
		public int BouyomiChanVoice = BouyomiChan.VOICE_MIN;

		public int BouyomiChanSnipLen = 100;
		public string BouyomiChanSnippedTrailer = "以下略";
		public bool BouyomiChanIgnoreSelfRemark = false;

		public bool IPDisabledWhenTripDisabled = false;

		public bool OnlineDlgEnabled = false;
		public Color OnlineForeColor = Color.DarkGreen;
		public Color OnlineBackColor = Color.LightGreen;

		public int OnlineDlg_L;
		public int OnlineDlg_T;
		public int OnlineDlg_W = -1; // -1 == OnlineDlg_LTWH 未設定
		public int OnlineDlg_H;
		public bool OnlineDlg_Minimized = false;

		public bool Flat_RemarksText = false;
		public bool Flat_MessageText = false;
		public bool Flat_OnlineText = false;

		public bool TaskBarFlashEnabled = false;

		// ---- ロード・セーブ

		private string GetProgDataDir()
		{
			string dir = Path.Combine(FileTools.getProgramData(), @"cerulean charlotte\\HechimaClient2");

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

			// ---- S/L Items ----

			this.ServerDomain = lines[c++];
			this.ServerPort = int.Parse(lines[c++]);
			this.crypTunnelPort = int.Parse(lines[c++]);
			this.Password = lines[c++];

			this.BouyomiChanEnabled = int.Parse(lines[c++]) != 0;
			this.BouyomiChanDomain = lines[c++];
			this.BouyomiChanPort = int.Parse(lines[c++]);

			this.MessageTextEnterMode = (MessageTextEnterMode_e)int.Parse(lines[c++]);

			this.RemarkFormat = lines[c++];

			this.RemarksTextDefaultFontFamily = lines[c++];
			this.RemarksTextDefaultFontSize = int.Parse(lines[c++]);
			this.RemarksTextDefaultFontColor = Color.FromArgb(int.Parse(lines[c++]));
			this.RemarksTextBackColor = Color.FromArgb(int.Parse(lines[c++]));
			this.MessageTextForeColor = Color.FromArgb(int.Parse(lines[c++]));
			this.MessageTextBackColor = Color.FromArgb(int.Parse(lines[c++]));

			this.MainWin_L = int.Parse(lines[c++]);
			this.MainWin_T = int.Parse(lines[c++]);
			this.MainWin_W = int.Parse(lines[c++]);
			this.MainWin_H = int.Parse(lines[c++]);

			this.UserName = lines[c++];
			this.UserTrip = lines[c++];

			this.TripEnabled = int.Parse(lines[c++]) != 0;
			this.ShowRemarkStampDate = int.Parse(lines[c++]) != 0;

			this.BouyomiChanSpeed = int.Parse(lines[c++]);
			this.BouyomiChanTone = int.Parse(lines[c++]);
			this.BouyomiChanVolume = int.Parse(lines[c++]);
			this.BouyomiChanSpeedUseDef = int.Parse(lines[c++]) != 0;
			this.BouyomiChanToneUseDef = int.Parse(lines[c++]) != 0;
			this.BouyomiChanVolumeUseDef = int.Parse(lines[c++]) != 0;
			this.BouyomiChanVoice = int.Parse(lines[c++]);

			this.BouyomiChanSnipLen = int.Parse(lines[c++]);
			this.BouyomiChanSnippedTrailer = lines[c++];
			this.BouyomiChanIgnoreSelfRemark = int.Parse(lines[c++]) != 0;

			this.IPDisabledWhenTripDisabled = int.Parse(lines[c++]) != 0;

			this.OnlineDlgEnabled = int.Parse(lines[c++]) != 0;
			this.OnlineForeColor = Color.FromArgb(int.Parse(lines[c++]));
			this.OnlineBackColor = Color.FromArgb(int.Parse(lines[c++]));

			this.OnlineDlg_L = int.Parse(lines[c++]);
			this.OnlineDlg_T = int.Parse(lines[c++]);
			this.OnlineDlg_W = int.Parse(lines[c++]);
			this.OnlineDlg_H = int.Parse(lines[c++]);
			this.OnlineDlg_Minimized = int.Parse(lines[c++]) != 0;

			this.Flat_RemarksText = int.Parse(lines[c++]) != 0;
			this.Flat_MessageText = int.Parse(lines[c++]) != 0;
			this.Flat_OnlineText = int.Parse(lines[c++]) != 0;

			this.TaskBarFlashEnabled = int.Parse(lines[c++]) != 0;

			// 新しい項目、ここへ追加..

			// ----
		}

		// 既存の項目(行)の間又は前に新しい項目を挿入すると、既存の Setting.dat がぶっ壊れるので、新しい項目は最後に追加すること！

		public void Save()
		{
			List<string> lines = new List<string>();

			// ---- S/L Items ----

			lines.Add(this.ServerDomain);
			lines.Add("" + this.ServerPort);
			lines.Add("" + this.crypTunnelPort);
			lines.Add(this.Password);

			lines.Add("" + (this.BouyomiChanEnabled ? 1 : 0));
			lines.Add(this.BouyomiChanDomain);
			lines.Add("" + this.BouyomiChanPort);

			lines.Add("" + (int)this.MessageTextEnterMode);

			lines.Add(this.RemarkFormat);

			lines.Add(this.RemarksTextDefaultFontFamily);
			lines.Add("" + this.RemarksTextDefaultFontSize);
			lines.Add("" + this.RemarksTextDefaultFontColor.ToArgb());
			lines.Add("" + this.RemarksTextBackColor.ToArgb());
			lines.Add("" + this.MessageTextForeColor.ToArgb());
			lines.Add("" + this.MessageTextBackColor.ToArgb());

			lines.Add("" + this.MainWin_L);
			lines.Add("" + this.MainWin_T);
			lines.Add("" + this.MainWin_W);
			lines.Add("" + this.MainWin_H);

			lines.Add(this.UserName);
			lines.Add(this.UserTrip);

			lines.Add("" + (this.TripEnabled ? 1 : 0));
			lines.Add("" + (this.ShowRemarkStampDate ? 1 : 0));

			lines.Add("" + this.BouyomiChanSpeed);
			lines.Add("" + this.BouyomiChanTone);
			lines.Add("" + this.BouyomiChanVolume);
			lines.Add("" + (this.BouyomiChanSpeedUseDef ? 1 : 0));
			lines.Add("" + (this.BouyomiChanToneUseDef ? 1 : 0));
			lines.Add("" + (this.BouyomiChanVolumeUseDef ? 1 : 0));
			lines.Add("" + this.BouyomiChanVoice);

			lines.Add("" + this.BouyomiChanSnipLen);
			lines.Add(this.BouyomiChanSnippedTrailer);
			lines.Add("" + (this.BouyomiChanIgnoreSelfRemark ? 1 : 0));

			lines.Add("" + (this.IPDisabledWhenTripDisabled ? 1 : 0));

			lines.Add("" + (this.OnlineDlgEnabled ? 1 : 0));
			lines.Add("" + this.OnlineForeColor.ToArgb());
			lines.Add("" + this.OnlineBackColor.ToArgb());

			lines.Add("" + this.OnlineDlg_L);
			lines.Add("" + this.OnlineDlg_T);
			lines.Add("" + this.OnlineDlg_W);
			lines.Add("" + this.OnlineDlg_H);
			lines.Add("" + (this.OnlineDlg_Minimized ? 1 : 0));

			lines.Add("" + (this.Flat_RemarksText ? 1 : 0));
			lines.Add("" + (this.Flat_MessageText ? 1 : 0));
			lines.Add("" + (this.Flat_OnlineText ? 1 : 0));

			lines.Add("" + (this.TaskBarFlashEnabled ? 1 : 0));

			// 新しい項目、ここへ追加..

			// ----

			File.WriteAllLines(GetSaveFile(), lines, StringTools.ENCODING_SJIS);
		}
	}
}
