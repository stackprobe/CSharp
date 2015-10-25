using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class EventTools
	{
		public static void MainWinToTop()
		{
			for (int c = 4; 0 < c; c--)
			{
				bool flag = c % 2 == 0;

				if (Gnd.I.Sd.MainWinAlwaysTop)
					flag = flag == false;

				EventCenter.I.AddEvent(Consts.EVENT_PREFERENCE, delegate
				{
					Gnd.I.MainWin.TopMost = flag;
				});
			}
		}
	}
}
