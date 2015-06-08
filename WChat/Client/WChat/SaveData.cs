using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Drawing;

namespace Charlotte
{
	/// <summary>
	/// 初期値とコンストラクタでセットされた値が「デフォルト設定」になる。
	/// </summary>
	public class SaveData
	{
		public SaveData()
		{
			// noop
		}

		public void PostLoad()
		{
			if (this.FileSvHomeDir == null || Directory.Exists(this.FileSvHomeDir) == false)
				this.FileSvHomeDir = Directory.GetCurrentDirectory();

			if (this.Ident == null)
				this.Ident = StringTools.MakeUUID();

			if (this.TrackName == null)
				this.TrackName = StringTools.MakePassword_du();

			if (this.UserName == null)
				this.UserName = "名無しさん" + SystemTools.GetCryptoRand(100000);

			{
				SockTest st = new SockTest();

				this.NamedTrackHttpPort = st.PortNoFltr(this.NamedTrackHttpPort);
				this.FileSvRecvPort = st.PortNoFltr(this.FileSvRecvPort);
				this.NamedTrackPort = st.PortNoFltr(this.NamedTrackPort);
			}
		}

		public int MainWin_L;
		public int MainWin_T;
		public int MainWin_W; // 0 == 未設定
		public int MainWin_H;

		public int MemberList_W = -1; // -1 == 固定しない
		public int RemarkText_H = -1; // -1 == 固定しない
		public int BtnSend_W = -1; // -1 == 固定しない

		public bool HideMemberList;
		public bool HideBtnSend;

		public string ServerDomain = "localhost";
		public int ServerPort = 59999;
		public int FileSvPort = 60001; // revServer.exeのポート, このPC -> 鯖 (2)
		public int NamedTrackHttpPort = 80; // ブラウザ -> このPC (1)

		public bool FileSvEnabled;
		public int FileSvRecvPort = 60002; // NT (<-) revClient -> このPC (4)
		public int NamedTrackPort = 60003; // 鯖 (<-) NT <- revClient (3)
		public string FileSvHomeDir;

		public Color MemberListBackColor = DataConv.GetColor("fdfeec");//Color.White;
		public FontMan MemberListFont = new FontMan(Color.Black);

		public Color TimeLineTextBackColor = Color.White;
		public FontMan TimeLineTextStampFont = new FontMan(8, DataConv.GetColor("008040"));//new FontMan(Color.Black);
		public FontMan TimeLineTextUserNameFont = new FontMan(12, DataConv.GetColor("800040"));//new FontMan(Color.Black);
		public FontMan TimeLineTextMessageFont = new FontMan(Color.Black);
		public int TimeLineTextLenMax = 300000;
		public int TimeLineTextShortenPct = 80;
		public bool TimeLineText行間を詰める;
		public bool TimeLineText空行を挟む;
		public bool TimeLineText字下げする;
		public int TimeLineTextTimeFormat; // TimeFormat_e
		public int TimeLineTextファイルDLモード; // FileDLMode_e
		public int TimeLineTextPathClickMode; // PathClickMode_e

		public Color RemarkTextBackColor = DataConv.GetColor("eeffee");//Color.White;
		public FontMan RemarkTextFont = new FontMan(Color.Black);
		public bool CtrlEnterで改行_Enterで送信;

		public bool BouyomichanEnabled;
		public string BouyomichanDomain = "localhost";
		public int BouyomichanPort = 50001;

		public string Ident;
		public string TrackName;
		public string UserName;

		// ---- hidden ----

		public bool ServerTimeDiffを取得しない;

		// ----

		public void Load()
		{
			try
			{
				if (File.Exists(SystemTools.GetSaveDataFile()) == false) // ? ファイル未作成
					return;

				MapData<string, string> md = StringTools.ToMapData(
					File.ReadAllLines(SystemTools.GetSaveDataFile(), StringTools.ENCODING_SJIS).ToList()
					);

				foreach (FieldInfo fi in ReflecTools.GetFields(this))
				{
					string value = md.Get(fi.Name, null);

					if (value == null)
						continue;

					ReflecTools.SetValue(fi, this, DataConv.GetObject(fi, value));
				}
			}
			catch (Exception e) // ? ファイルの破損
			{
				SystemTools.WriteLog(e);
			}
		}

		public void Save()
		{
			MapData<string, string> md = new MapData<string, string>();

			foreach (FieldInfo fi in ReflecTools.GetFields(this))
			{
				md.Put(
					fi.Name,
					DataConv.GetString(
						fi,
						ReflecTools.GetValue(fi, this)
						)
					);
			}
			File.WriteAllLines(
				SystemTools.GetSaveDataFile(),
				StringTools.ToList(md),
				StringTools.ENCODING_SJIS
				);
		}
	}
}
