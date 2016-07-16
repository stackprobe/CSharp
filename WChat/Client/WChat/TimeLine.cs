using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class TimeLine
	{
		public TimeLineTextMan I;

		public TimeLine(TimeLineTextMan instance)
		{
			this.I = instance;
		}

		// ---- 発言リスト ----

		public List<RemarkData> RemarkDataList = new List<RemarkData>();

		public class RemarkData
		{
			public string Stamp; // TimeData.ToCompact()
			public Message Message;

			public RemarkData GetClone()
			{
				return new RemarkData()
				{
					Stamp = this.Stamp,
					Message = this.Message.GetClone(),
				};
			}

			public static bool IsSame(RemarkData a, RemarkData b)
			{
				return
					a.Stamp == b.Stamp &&
					Message.IsSame(a.Message, b.Message);
			}
		}

		public class Message
		{
			public string UserName;
			public string RemarkText;
			public List<string> LinkPaths; // {} == リンク無し

			public string GetString()
			{
				return AttachString.I.Untokenize(
					this.UserName,
					this.RemarkText,
					AttachString.I.Untokenize(this.LinkPaths.ToArray())
					);
			}

			public void SetString(string str)
			{
				List<string> l = AttachString.I.Tokenize(str);
				int c = 0;

				this.UserName = l[c++];
				this.RemarkText = l[c++];
				this.LinkPaths = AttachString.I.Tokenize(l[c++]);
			}

			public static Message FromString(string str)
			{
				Message ret = new Message();
				ret.SetString(str);
				return ret;
			}

			public Message GetClone()
			{
				return new Message()
				{
					UserName = this.UserName,
					RemarkText = this.RemarkText,
					LinkPaths = ArrayTools.GetClone(this.LinkPaths),
				};
			}

			public static bool IsSame(Message a, Message b)
			{
				return
					a.UserName == b.UserName &&
					a.RemarkText == b.RemarkText &&
					StringTools.IsSame(a.LinkPaths, b.LinkPaths);
			}
		}

		private string GetLastStamp(string defval)
		{
			int index = this.RemarkDataList.Count - 1 - this.KariRemarkCount;

			if (index < 0)
				return defval;

			return this.RemarkDataList[index].Stamp;
		}

		private int ProcessExtraData(string[] lines)
		{
			int index = 0;

			Gnd.I.Heartbeat.HeartbeatLoginLogoutSerialListener(long.Parse(lines[index++]));

			return index;
		}

		private List<RemarkData> GetRemarkDataList(string[] lines)
		{
			List<RemarkData> ret = new List<RemarkData>();

			for (int index = this.ProcessExtraData(lines); index < lines.Length; )
			{
				string stampLine = lines[index++];
				string messageLine = lines[index++];

				ret.Add(new RemarkData()
				{
					Stamp = stampLine,
					Message = Message.FromString(messageLine),
				});
			}
			return ret;
		}

		private RemarkData GetRemarkDataByRemarked(string remarkText)
		{
			return new RemarkData()
			{
				Stamp = new TimeData(TimeData.Now().T + Gnd.I.ServerTimeDiff).ToCompact(),
				Message = new Message()
				{
					UserName = AppTools.GetUserName4Disp(),
					RemarkText = remarkText,
					LinkPaths = new List<string>(),
				},
			};
		}

		private RemarkData GetRemarkDataByDropPath(List<string> paths)
		{
			List<string> linkPaths = GetLinkPaths(paths);

			return new RemarkData()
			{
				Stamp = new TimeData(TimeData.Now().T + Gnd.I.ServerTimeDiff).ToCompact(),
				Message = new Message()
				{
					UserName = AppTools.GetUserName4Disp(),
					RemarkText = linkPaths.Count + "件のファイル又はフォルダ",
					LinkPaths = linkPaths,
				},
			};
		}

		private List<string> GetLinkPaths(List<string> paths)
		{
			List<string> linkPaths = new List<string>();

			foreach (string path in paths)
				linkPaths.Add(GetLinkPath(path));

			return linkPaths;
		}

		private string GetLinkPath(string path)
		{
			return AppTools.GetUrlPath(Gnd.I.Sd.TrackName, path, File.Exists(path) == false);
		}

		private List<RemarkData> GetClone(List<RemarkData> src)
		{
			List<RemarkData> dest = new List<RemarkData>();

			foreach (RemarkData remarkData in src)
				dest.Add(remarkData.GetClone());

			return dest;
		}

		private bool IsSameRange(List<RemarkData> list1, int startPos1, List<RemarkData> list2, int startPos2, int count)
		{
			return ArrayTools.IsSameRange(list1, startPos1, list2, startPos2, RemarkData.IsSame, count);
		}

		// ----

		private const int TIMER_END_MIN = 20;
		private const int TIMER_END_ADD = 1;//20;
		private const int TIMER_END_MAX = 200;
		private int TimerCount; // -1 == スケジュールされている。
		private int TimerEnd = TIMER_END_MIN;

		/// <summary>
		/// 100ms毎に実行する。
		/// </summary>
		public void EachTimer()
		{
			if (this.TimerCount == -1)
				return;

			if (this.TimerCount < this.TimerEnd)
			{
				this.TimerCount++;
				return;
			}
			this.TimerEnd = Math.Min(this.TimerEnd + TIMER_END_ADD, TIMER_END_MAX);
			this.DoTimeLine();
		}

		private int SchedulingDTLCount;

		/// <summary>
		/// タイムライン取得（即実行）
		/// 必要であれば画面更新
		/// </summary>
		private void DoTimeLine()
		{
			this.TimerCount = -1;
			this.SchedulingDTLCount++;

			EventCenter.I.AddEvent(Consts.EVENT_BACKGROUND, delegate
			{
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
				{
					// ChatMan 使用中の可能性があるのでイベントで実行すること。
					Gnd.I.ChatMan.TimeLineCommand(
						this.GetLastStamp("0"),
						"9"
						);
				});
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.ChatMan_End);
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
				{
					if (this.SchedulingDTLCount != 1)
						return;

					string[] lines = Gnd.I.ChatMan.GetOutput();

					if (lines == null)
						return;

					List<RemarkData> listAdd = this.GetRemarkDataList(lines);

					if (1 <= listAdd.Count)
						this.TimerEnd = TIMER_END_MIN;

					for (int index = Math.Max(0, listAdd.Count - 20); index < listAdd.Count; index++)
					{
						RemarkData remarkData = listAdd[index];

						if (remarkData.Message.LinkPaths.Count == 0)
							this.DoBouyomichan(remarkData.Message.RemarkText);
					}
					this.AddByDoTimeLine(listAdd);
				});
				EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
				{
					this.TimerCount = 0;
					this.SchedulingDTLCount--;
				});
			});
		}

		private void DoBouyomichan(string text)
		{
			if (Gnd.I.Sd.BouyomichanEnabled == false)
				return;

			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				// ChatMan 使用中の可能性があるのでイベントで実行すること。
				Gnd.I.ChatMan.BouyomichanCommand(
					Gnd.I.Sd.BouyomichanDomain,
					Gnd.I.Sd.BouyomichanPort,
					text,
					65535,
					65535,
					65535,
					0
					);
			});
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.ChatMan_End);
		}

		/// <summary>
		/// 発言する。
		/// </summary>
		/// <param name="remarkText"></param>
		public void DoRemark(string remarkText)
		{
			remarkText = UtfStringFltr.Serialize(remarkText);
			remarkText = JString.ToJString(remarkText, true, true, false, true);
			remarkText = remarkText.Trim();

			if (remarkText == "")
				return;

			this.DoRemark(this.GetRemarkDataByRemarked(remarkText));
		}

		/// <summary>
		/// パスを貼り付ける。
		/// </summary>
		/// <param name="paths"></param>
		public void DoDropPaths(List<string> paths)
		{
			this.DoRemark(this.GetRemarkDataByDropPath(paths));
		}

		private void DoRemark(RemarkData remarkData)
		{
			this.AddByRemark(remarkData);

			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				// ChatMan 使用中の可能性があるから、ここで実行する。
				Gnd.I.ChatMan.RemarkCommand(
					remarkData.Stamp,
					remarkData.Message.GetString()
					);
			});
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.ChatMan_End);

			this.TimerEnd = TIMER_END_MIN;
			this.DoTimeLine();
		}

		/// <summary>
		/// 設定が変更された。或いはアプリ起動時
		/// 再描画する。
		/// </summary>
		public void SettingChanged()
		{
			//using (new FreezeUi(Gnd.I.MainWin)) // フォーカスは飛んでいく
			{
				this.I.I.Clear();
				this.I.RefreshUi();
				this.I.Add(this.RemarkDataList);
			}
		}

		public void Redraw()
		{
			//using (new FreezeUi(Gnd.I.MainWin)) // フォーカスは飛んでいく
			{
				this.I.I.Clear();
				this.I.Add(this.RemarkDataList);
			}
		}

		/// <summary>
		/// オーバーフローしていたら縮める。
		/// </summary>
		public void OverflowCheck()
		{
			bool redrawFlag = false;

			for (int c = 0; c < 20; c++) // XXX
			{
				if (this.I.I.I.Text.Length < Gnd.I.Sd.TimeLineTextLenMax || this.RemarkDataList.Count < 2)
					break;

				int count = this.RemarkDataList.Count;
				count *= Gnd.I.Sd.TimeLineTextShortenPct;
				count /= 100;
				count = IntTools.ToRange(count, 1, this.RemarkDataList.Count - 1); // 少なくとも１件削る && ゼロ件にならないように

				this.RemarkDataList.RemoveRange(0, this.RemarkDataList.Count - count);

				redrawFlag = true;
			}
			if (redrawFlag)
				this.Redraw();
		}

		private int KariRemarkCount;

		private void AddByRemark(RemarkData remarkData)
		{
			if (this.KariRemarkCount == 0)
				this.I.SaveRtf();

			this.KariRemarkCount++;
			this.RemarkDataList.Add(remarkData);
			this.I.Add(new RemarkData[]
			{
				remarkData,
			}
			.ToList());
		}

		private void AddByDoTimeLine(List<RemarkData> listAdd)
		{
			if (this.IsCorrectKariRemark(listAdd))
			{
				ArrayTools.RemoveTopLoop(listAdd, this.KariRemarkCount);
				this.I.Add(listAdd);
			}
			else
			{
				ArrayTools.RemoveTailLoop(this.RemarkDataList, this.KariRemarkCount);
				this.I.Join(this.I.GetSavedRtf(), listAdd);
			}
			this.RemarkDataList.AddRange(listAdd);
			this.KariRemarkCount = 0;

			// ----

			this.OverflowCheck();
		}

		private bool IsCorrectKariRemark(List<RemarkData> listAdd)
		{
			if (listAdd.Count < this.KariRemarkCount)
				return false;

			if (this.IsSameRange(
				this.RemarkDataList,
				this.RemarkDataList.Count - this.KariRemarkCount,
				listAdd,
				0,
				this.KariRemarkCount
				) == false
				)
				return false;

			return true;
		}
	}
}
