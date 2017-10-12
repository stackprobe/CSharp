using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte
{
	public class Common
	{
		public static void WaitToBgServiceEndable()
		{
			using (BusyDlg f = new BusyDlg(delegate()
			{
				while (Gnd.bgService.IsEndable() == false)
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
