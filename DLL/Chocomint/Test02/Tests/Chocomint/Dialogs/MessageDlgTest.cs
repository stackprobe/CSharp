using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class MessageDlgTest
	{
		public void Test01()
		{
			using (MessageDlg f = new MessageDlg())
			{
				f.ShowDialog();
			}
		}

		public void Test02()
		{
			using (MessageDlg f = new MessageDlg())
			{
				f.Message =
					"メッセージ１行目 0123456789 abcdef\n" +
					"メッセージ２行目 0123456789 abcdef\n" +
					"メッセージ３行目 0123456789 abcdef";

				f.PostShown = () =>
				{
					f.Text = "タイトル 0123456789 abcdef";
				};

				f.ShowDialog();
			}
		}

		public void Test03()
		{
			Exception e = new Exception();

			using (MessageDlg f = new MessageDlg())
			{
				f.Message = e.Message;
				f.DetailMessage = "" + e;

				f.ShowDialog();
			}
		}
	}
}
