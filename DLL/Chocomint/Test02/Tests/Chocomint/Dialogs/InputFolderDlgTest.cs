using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputFolderDlgTest
	{
		public void Test01()
		{
			using (InputFolderDlg f = new InputFolderDlg())
			{
				f.ShowDialog();
			}
		}
	}
}
