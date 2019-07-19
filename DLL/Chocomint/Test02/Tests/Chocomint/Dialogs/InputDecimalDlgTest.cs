using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputDecimalDlgTest
	{
		public void Test01()
		{
			using (InputDecimalDlg f = new InputDecimalDlg())
			{
				f.ShowDialog();

				if (f.OkPressed)
				{
					MessageDlgTools.Show(MessageDlg.Mode_e.Information, "情報", "入力された値：" + f.Value);
				}
			}
		}
	}
}
