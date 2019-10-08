using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;
using System.Threading;

namespace Test02.Tests.Chocomint.Dialogs
{
	class WaitDlgTest
	{
		public void Test01()
		{
			using (WaitDlg f = new WaitDlg())
			{
				f.ShowDialog();
			}
			PostTestXX();
		}

		private void PostTestXX()
		{
			MessageDlgTools.Information("情報", "WaitDlg.LastCancelled: " + WaitDlg.LastCancelled);
		}

		public void Test02()
		{
			WaitDlgTools.Show("busy", "5 seconds", () => Test02_Routine(5), Test02_Interlude, Test02_Interlude_Cancelled);
			PostTestXX();
		}

		public void Test03()
		{
			WaitDlgTools.Show("busy", "15 seconds", () => Test02_Routine(15), Test02_Interlude, Test02_Interlude_Cancelled);
			PostTestXX();
		}

		public void Test04()
		{
			WaitDlgTools.Show("busy", "60 seconds", () => Test02_Routine(60), Test02_Interlude, Test02_Interlude_Cancelled);
			PostTestXX();
		}

		private double Test02_ProgressRate = 0.0;
		private bool Test02_Cancelled = false;

		private void Test02_Routine(int waitSec)
		{
			for (int sec = 0; sec < waitSec; sec++)
			{
				Test02_ProgressRate = sec * 1.0 / waitSec;

				if (Test02_Cancelled)
					break;

				Thread.Sleep(1000);
			}
		}

		public double Test02_Interlude()
		{
			return Test02_ProgressRate;
		}

		public void Test02_Interlude_Cancelled()
		{
			Test02_Cancelled = true;
		}
	}
}
