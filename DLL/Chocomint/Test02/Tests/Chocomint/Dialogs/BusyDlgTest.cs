using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;
using System.Threading;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class BusyDlgTest
	{
		public void Test01()
		{
			using (BusyDlg f = new BusyDlg())
			{
				f.ShowDialog();
			}
		}

		public void Test02()
		{
			BusyDlgTools.Show("busy", "5 seconds", () => Thread.Sleep(5000));
		}

		public void Test03()
		{
			BusyDlgTools.Show("busy", "15 seconds", () => Thread.Sleep(15000));
		}

		public void Test04()
		{
			BusyDlgTools.Show("busy", "60 seconds", () => Thread.Sleep(60000));
		}
	}
}
