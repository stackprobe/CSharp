using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Charlotte.Tests.Chocomint.Dialogs
{
	public class InputStringDlgTest
	{
		public void Test01()
		{
			using (InputStringDlg f = new InputStringDlg())
			{
				f.ShowDialog();
			}
		}
	}
}
