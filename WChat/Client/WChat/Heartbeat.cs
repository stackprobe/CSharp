using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Heartbeat
	{
		public MemberListMan I;

		public Heartbeat(MemberListMan instance)
		{
			this.I = instance;

#if false // test
			// test
			{
				this.MemberDataList.Add(new MemberData()
				{
					Ident = "123456789",
					Message = new Message()
					{
						UserName = "123456789",
						FileSvEnabled = false,
						FileSvHomeDir = @"C:\123\456\789",
					},
				});
				this.MemberDataList.Add(new MemberData()
				{
					Ident = "123456789_2",
					Message = new Message()
					{
						UserName = "123456789_2",
						FileSvEnabled = true,
						FileSvHomeDir = @"C:\123\456\789_2",
					},
				});
				this.MemberDataList.Add(new MemberData()
				{
					Ident = "123456789_3",
					Message = new Message()
					{
						UserName = "123456789_3",
						FileSvEnabled = false,
						FileSvHomeDir = @"C:\123\456\789_3",
					},
				});
			}
#endif
		}

		// ---- メンバーリスト ----

		private List<MemberData> MemberDataList = new List<MemberData>();

		public class MemberData
		{
			public string Ident;
			public Message Message;

			public static bool IsSame(MemberData a, MemberData b)
			{
				return
					a.Ident == b.Ident &&
					Message.IsSame(a.Message, b.Message);
			}
		}

		public class Message
		{
			public string UserName;
			public string TrackName;
			public bool FileSvEnabled;
			public string FileSvHomeDir;

			public string GetString()
			{
				return AttachString.I.Untokenize(
					this.UserName,
					this.TrackName,
					this.FileSvEnabled ? StringTools.S_TRUE : StringTools.S_FALSE,
					this.FileSvHomeDir
					);
			}

			public void SetString(string str)
			{
				List<string> l = AttachString.I.Tokenize(str);
				int c = 0;

				this.UserName = l[c++];
				this.TrackName = l[c++];
				this.FileSvEnabled = l[c++] == StringTools.S_TRUE;
				this.FileSvHomeDir = l[c++];
			}

			public static Message FromString(string str)
			{
				Message ret = new Message();
				ret.SetString(str);
				return ret;
			}

			public static Message GetSelf()
			{
				return new Message()
				{
					UserName = AppTools.GetUserName4Disp(),
					TrackName = Gnd.I.Sd.TrackName,
					FileSvEnabled = Gnd.I.Sd.FileSvEnabled,
					FileSvHomeDir = Gnd.I.Sd.FileSvHomeDir,
				};
			}

			public static bool IsSame(Message a, Message b)
			{
				return
					a.UserName == b.UserName &&
					a.TrackName == b.TrackName &&
					a.FileSvEnabled == b.FileSvEnabled &&
					a.FileSvHomeDir == b.FileSvHomeDir;
			}
		}

		public List<MemberData> GetMemberDataList(string[] lines)
		{
			List<MemberData> ret = new List<MemberData>();

			for (int index = 0; index < lines.Length; )
			{
				string identLine = lines[index++];
				string messageLine = lines[index++];

				ret.Add(new MemberData()
				{
					Ident = identLine,
					Message = Message.FromString(messageLine),
				});
			}
			return ret;
		}

		private bool IsSame(List<MemberData> list1, List<MemberData> list2)
		{
			return ArrayTools.IsSame(list1, list2, MemberData.IsSame);
		}

		private void DoDraw()
		{
			this.I.I.Items.Clear();

			foreach (MemberData md in this.MemberDataList)
			{
				this.I.I.Items.Add(md.Message.UserName);
			}
		}

		// ---- tools ----

		public MemberData GetSelected()
		{
			int index = this.I.I.SelectedIndex;

			if (index == -1)
				return null;

			return this.MemberDataList[index];
		}

		// ----

		private const int TIMER_END = 600; // 1 min
		private int TimerCount; // -1 == スケジュールされている。
		private bool 即実行Flag;

		/// <summary>
		/// 100ms毎に実行する。
		/// </summary>
		public void EachTimer()
		{
			if (this.TimerCount == -1)
				return;

			if (this.即実行Flag)
			{
				this.即実行Flag = false;
				this.DoHeartbeat();
				return;
			}
			if (this.TimerCount < TIMER_END)
			{
				this.TimerCount++;
				return;
			}
			this.DoHeartbeat();
		}

		/// <summary>
		/// ハートビート即実行
		/// 必要であれば画面更新
		/// </summary>
		public void DoHeartbeat()
		{
			this.TimerCount = -1;

			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				// ChatMan 使用中の可能性があるのでイベントで実行すること。
				Gnd.I.ChatMan.HeartbeatCommand(
					Gnd.I.Sd.Ident,
					Message.GetSelf().GetString()
					);
			});
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.ChatMan_End);
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				string[] lines = Gnd.I.ChatMan.GetOutput();

				if (lines == null)
					return;

				List<MemberData> listNew = this.GetMemberDataList(lines);

				if (this.IsSame(listNew, this.MemberDataList))
					return;

				this.MemberDataList = listNew;
				this.DoDraw();
			});
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				this.TimerCount = 0;
			});
		}

		/// <summary>
		/// 設定が更新された。或いはアプリ起動時
		/// 再描画する。
		/// </summary>
		public void SettingChanged()
		{
			this.I.RefreshUi();
			this.DoHeartbeat();
		}

		private long LastHLLS = -1;

		public void HeartbeatLoginLogoutSerialListener(long serial)
		{
			if (this.LastHLLS == serial)
				return;

			this.LastHLLS = serial;
			this.即実行Flag = true;
		}

		public string GetStatus()
		{
			const int L = 100;
			int i = (int)(this.LastHLLS % L);
			i += L;
			i %= L;
			return StringTools.ZPad(i, 2);
		}
	}
}
