using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Chocomint.Dialogs
{
	public class MessageDlgTools
	{
		public static void Information(string title, string message, string detailMessage = null)
		{
			Show(MessageDlg.Mode_e.Information, title, message, detailMessage);
		}

		public static void Error(string title, Exception e)
		{
			Show(MessageDlg.Mode_e.Error, title, e);
		}

		public static void Warning(string title, Exception e)
		{
			Show(MessageDlg.Mode_e.Warning, title, e);
		}

		public static void Show(MessageDlg.Mode_e mode, string title, Exception e)
		{
			Show(mode, title, e.Message, "" + e);
		}

		public static void Show(MessageDlg.Mode_e mode, string title, string message, string detailMessage = null)
		{
			using (MessageDlg f = new MessageDlg())
			{
				f.Mode = mode;
				f.Message = message;
				f.DetailMessage = detailMessage;

				f.PostShown = () =>
				{
					f.Text = title;
				};

				f.ShowDialog();
			}
		}
	}
}
