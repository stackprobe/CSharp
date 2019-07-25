using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public class MessageDlgTools
	{
		public static void Information(string title, string message, bool hasParent = false, string detailMessage = null)
		{
			Show(MessageDlg.Mode_e.Information, title, message, hasParent, detailMessage);
		}

		public static void Error(string title, Exception e, bool hasParent = false)
		{
			Show(MessageDlg.Mode_e.Error, title, e, hasParent);
		}

		public static void Warning(string title, Exception e, bool hasParent = false)
		{
			Show(MessageDlg.Mode_e.Warning, title, e, hasParent);
		}

		public static void Show(MessageDlg.Mode_e mode, string title, Exception e, bool hasParent = false)
		{
			Show(mode, title, e.Message, hasParent, "" + e);
		}

		public static void Show(MessageDlg.Mode_e mode, string title, string message, bool hasParent = false, string detailMessage = null)
		{
			using (MessageDlg f = new MessageDlg())
			{
				f.Mode = mode;
				f.Message = message;
				f.DetailMessage = detailMessage;

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
				};

				f.ShowDialog();
			}
		}
	}
}
