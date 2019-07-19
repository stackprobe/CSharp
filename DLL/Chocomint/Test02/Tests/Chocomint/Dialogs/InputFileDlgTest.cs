using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputFileDlgTest
	{
		public void Test01()
		{
			using (InputFileDlg f = new InputFileDlg())
			{
				f.ShowDialog();
			}
		}
	}
}
