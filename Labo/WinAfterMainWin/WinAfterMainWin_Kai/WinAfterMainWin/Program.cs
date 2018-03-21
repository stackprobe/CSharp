using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace WinAfterMainWin
{
	static class Program
	{
		/// <summary>
		/// アプリケーションのメイン エントリ ポイントです。
		/// </summary>
		[STAThread]
		static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new PseudoMainWin(() =>
			{
				// Before MainWin

				using (MainWin f = new MainWin())
				{
					f.ShowDialog();
				}

				// After MainWin

				using (AfterWin f = new AfterWin())
				{
					f.ShowDialog();
				}
			}
			));
		}
	}
}
