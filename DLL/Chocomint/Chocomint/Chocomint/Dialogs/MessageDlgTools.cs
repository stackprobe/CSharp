using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public class MessageDlgTools
	{
		public static void Information(string title, string message, string detailMessage = null, Form parent = null)
		{
			Show(MessageDlg.Mode_e.Information, title, message, detailMessage, parent);
		}

		public static void Error(string title, Exception e, Form parent = null)
		{
			Show(MessageDlg.Mode_e.Error, title, e, parent);
		}

		public static void Warning(string title, Exception e, Form parent = null)
		{
			Show(MessageDlg.Mode_e.Warning, title, e, parent);
		}

		public static void Show(MessageDlg.Mode_e mode, string title, Exception e, Form parent = null)
		{
			Show(mode, title, e.Message, "" + e, parent);
		}

		public static void Show(MessageDlg.Mode_e mode, string title, string message, string detailMessage = null, Form parent = null)
		{
			using (MessageDlg f = new MessageDlg())
			{
				f.Mode = mode;
				f.Message = message;
				f.DetailMessage = detailMessage;

				if (parent != null)
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
