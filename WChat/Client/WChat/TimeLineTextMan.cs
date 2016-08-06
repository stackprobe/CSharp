using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	/// <summary>
	/// MainWin.TimeLineText の Manager
	/// </summary>
	public class TimeLineTextMan
	{
		public RtbMan I;

		public TimeLineTextMan(RtbMan instance)
		{
			this.I = instance;
		}

		public void RefreshUi()
		{
			this.I.I.BackColor = Gnd.I.Sd.TimeLineTextBackColor;
			this.I.Set行間を詰める(Gnd.I.Sd.TimeLineText行間を詰める);
			this.I.I.WordWrap = Gnd.I.Sd.TimeLineWordWrap;
		}

		private string SavedRtf;

		public void SaveRtf()
		{
			this.SavedRtf = this.I.I.Rtf;
		}

		public string GetSavedRtf()
		{
			return this.SavedRtf;
		}

		public void Add(List<TimeLine.RemarkData> listAdd)
		{
			if (listAdd.Count == 0)
				return;

			this.I.Add(this.GetTokens(listAdd));
			this.I.ScrollToBottom();
		}

		public void Join(string rtf, List<TimeLine.RemarkData> listAdd)
		{
#if true
			//using (new FreezeUi(Gnd.I.MainWin)) // チラつきは減るけど、フォーカスがどっか行っちゃうorz
			{
				this.I.I.Rtf = rtf;
				this.I.ScrollToBottom();
				this.Add(listAdd);
			}
#else // old -- 最下までスクロールが上手くいかない。
			this.I.Join(rtf, this.GetTokens(listAdd));
			this.I.ScrollToBottom();
#endif
		}

		private List<RtbMan.Token> GetTokens(List<TimeLine.RemarkData> src)
		{
			List<RtbMan.Token> dest = new List<RtbMan.Token>();

			foreach (TimeLine.RemarkData remarkData in src)
				this.AddTokens(remarkData, dest);

			return dest;
		}

		private void AddTokens(TimeLine.RemarkData src, List<RtbMan.Token> dest)
		{
			dest.Add(new RtbMan.Token(
				new Font(Consts.DEFAULT_FONT_NAME, 10f),
				Color.Black,
				Gnd.I.Sd.TimeLineText空行を挟む ? "\n\n" : "\n"
				));
			dest.Add(new RtbMan.Token(
				Gnd.I.Sd.TimeLineTextStampFont,
				this.GetStampForTimeLine(src.Stamp)
				));
			dest.Add(new RtbMan.Token(
				new Font(Consts.DEFAULT_FONT_NAME, 10f),
				Color.Black,
				" "
				));
			dest.Add(new RtbMan.Token(
				Gnd.I.Sd.TimeLineTextUserNameFont,
				src.Message.UserName
				));
			dest.Add(new RtbMan.Token(
				new Font(Consts.DEFAULT_FONT_NAME, 10f),
				Color.Black,
				"\n"
				));

			foreach (string remarkPart in this.GetRemarkParts(this.MessageFltr(src.Message.RemarkText)))
			{
				dest.Add(new RtbMan.Token(
					Gnd.I.Sd.TimeLineTextMessageFont,
					remarkPart
					));
			}

			foreach(string linkPath in src.Message.LinkPaths)
			{
				dest.Add(new RtbMan.Token(
					new Font(Consts.DEFAULT_FONT_NAME, 10f),
					Color.Black,
					Gnd.I.Sd.TimeLineText字下げする ? "\n　" : "\n"
					));
				dest.Add(new RtbMan.Token(
					Gnd.I.Sd.TimeLineTextMessageFont,
					this.GetUrl(linkPath)
					));
			}
		}

		private string GetStampForTimeLine(string stamp)
		{
			return TimeData.Parse(stamp).GetString(this.GetStampFormat());
		}

		private string GetStampFormat()
		{
			return new string[]
			{
				"Y/M/D (W) h:m:s",
				"Y/M/D h:m:s",
				"YMDhms",
				"Y年M月D日 (W) h時m分s秒",
				"Y年M月D日 h時m分s秒",
				"P",
				"<P-Day>",
			}
			[Gnd.I.Sd.TimeLineTextTimeFormat];
		}

		private const int REMARK_PART_LEN_MAX = 300;

		private List<string> GetRemarkParts(string remarkText)
		{
			if (REMARK_PART_LEN_MAX < remarkText.Length)
			{
				List<string> lines = StringTools.Tokenize(remarkText, "\n");

				for (int index = 1; index < lines.Count; index++)
					lines[index] = "\n" + lines[index];

				for (int c = 0; c < 20; c++) // XXX -- てきとう -- 連続する改行を制限した方がいい気がする。
				{
					for (int index = 1; index < lines.Count; index++)
					{
						if (
							c < 10 ?
							lines[index - 1].EndsWith("\n") :
							lines[index - 1].Length + lines[index].Length <= REMARK_PART_LEN_MAX
							)
						{
							lines[index - 1] += lines[index];
							lines[index] = null;
							index++;
						}
					}
					ArrayTools.RemoveNull(lines);
				}
				return lines;
			}
			else
			{
				List<string> ret = new List<string>();
				ret.Add(remarkText);
				return ret;
			}
		}

		private string MessageFltr(string message)
		{
			message = JString.ToJString(message, true, true, false, true);
			message = UtfStringFltr.Deserialize(message);
			message = message.Trim();

			if (Gnd.I.Sd.TimeLineText字下げする)
			{
				List<string> lines = StringTools.Tokenize(message, "\n");

				for (int index = 0; index < lines.Count; index++)
					lines[index] = "　" + lines[index];

				message = string.Join("\n", lines);
			}
			return message;
		}

		private string GetUrl(string linkPath)
		{
			int portNo = Gnd.I.Sd.NamedTrackHttpPort;
			string portNoPart;

			if (portNo != 80)
				portNoPart = ":" + portNo;
			else
				portNoPart = "";

			string modePart = "";

			if (linkPath.EndsWith("/") == false) // ? ファイル
			{
				switch (Gnd.I.Sd.TimeLineTextファイルDLモード)
				{
					case (int)Consts.FileDLMode_e.デフォルト:
						break;
					case (int)Consts.FileDLMode_e.ダウンロード:
						modePart = "?mode=download";
						break;
					case (int)Consts.FileDLMode_e.リンクページを開く:
						modePart = "?mode=html";
						break;
				}
			}
			return "http://" + "localhost" + portNoPart + "/" + linkPath + modePart;
		}
	}
}
