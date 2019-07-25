using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputTrackBarDlgToolsTest
	{
		public void Test01()
		{
			MessageDlgTools.Information("info", "ret=" + InputTrackBarDlgTools.Show("input value", "input value :"));
		}
	}
}
