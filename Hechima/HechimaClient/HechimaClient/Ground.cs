using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing;

namespace Charlotte
{
	public class Gnd
	{
		public static Conf conf = new Conf();
		public static Setting setting = new Setting();
		public static BgService bgService = new BgService();

		// ----

		public static void ImportSetting()
		{
			UserRealName =
				setting.UserName +
				Consts.DELIM_NAME_TRIP +
				Common.ToTrip(setting.UserTrip);
		}

		public static string UserRealName = "名無しさん12345" + Consts.DELIM_NAME_TRIP + "Trip123";

		// ----

		public static OnlineDlg onlineDlg = null;

		public static void OpenOnlineDlg()
		{
			if (Gnd.onlineDlg == null && Gnd.setting.OnlineDlgEnabled)
			{
				Gnd.onlineDlg = new OnlineDlg();
				Gnd.onlineDlg.Show();
			}
		}

		public static void CloseOnlineDlg()
		{
			if (Gnd.onlineDlg != null)
			{
				Gnd.onlineDlg.Close();
				Gnd.onlineDlg.Dispose();
				Gnd.onlineDlg = null;
			}
		}

		public static int NetErrorLevel = 0;
	}
}
