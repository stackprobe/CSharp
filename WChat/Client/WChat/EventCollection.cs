using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	/// <summary>
	/// EventCenter.I.AddEvent() 用 Event_d コレクション
	/// </summary>
	public class EventCollection
	{
		// ---- ChatMan ----

		public static void ChatMan_End()
		{
			if (Gnd.I.ChatMan.IsEnd() == false)
			{
				EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, ChatMan_End);
			}
		}

		// ---- FileSvMan ----

		public static void FileSvMan_End()
		{
			FileSvMan_End(0);
		}

		private static void FileSvMan_End(int c)
		{
			if (Gnd.I.FileSvMan.IsEnd(c) == false)
			{
				EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, delegate
				{
					FileSvMan_End(c + 1);
				});
			}
		}

		// ---- NamedTrackHttpMan ----

		public static void NamedTrackHttpMan_End()
		{
			NamedTrackHttpMan_End(0);
		}

		private static void NamedTrackHttpMan_End(int c)
		{
			if (Gnd.I.NamedTrackHttpMan.End_IsEnd(c) == false)
			{
				EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, delegate
				{
					NamedTrackHttpMan_End(c + 1);
				});
			}
		}

		// ---- NamedTrackMan ----

		public static void NamedTrackMan_End()
		{
			NamedTrackMan_End(0);
		}

		private static void NamedTrackMan_End(int c)
		{
			if (Gnd.I.NamedTrackMan.End_IsEnd(c) == false)
			{
				EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, delegate
				{
					NamedTrackMan_End(c + 1);
				});
			}
		}

		// ---- RevClient ----

		public static void RevClientMan_End()
		{
			RevClientMan_End(0);
		}

		private static void RevClientMan_End(int c)
		{
			if (Gnd.I.RevClientMan.End_IsEnd(c) == false)
			{
				EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, delegate
				{
					RevClientMan_End(c + 1);
				});
			}
		}

		// ----
	}
}
