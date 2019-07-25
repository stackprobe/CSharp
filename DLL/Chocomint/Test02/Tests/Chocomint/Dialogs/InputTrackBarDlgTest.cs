using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputTrackBarDlgTest
	{
		public void Test01()
		{
			using (InputTrackBarDlg f = new InputTrackBarDlg())
			{
				f.ShowDialog();
			}
		}
	}
}
