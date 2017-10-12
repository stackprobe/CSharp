using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

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

		// ----

		public void Load()
		{
			// TODO
		}

		public void Save()
		{
			// TODO
		}
	}
}
