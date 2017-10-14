using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class Common
	{
		public static void WaitToBgServiceDisposable()
		{
			using (BusyDlg f = new BusyDlg(delegate()
			{
				while (Gnd.bgService.IsDisposable() == false)
				{
					Thread.Sleep(100);
					Gnd.bgService.Perform();
				}
			}
			))
			{
				f.ShowDialog();
			}
		}
	}
}
