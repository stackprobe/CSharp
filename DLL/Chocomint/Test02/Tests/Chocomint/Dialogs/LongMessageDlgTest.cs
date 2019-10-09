using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class LongMessageDlgTest
	{
		public void Test01()
		{
			using (LongMessageDlg f = new LongMessageDlg())
			{
				f.ShowDialog();
			}
		}

		public void Test02()
		{
			LongMessageDlgTools.Warning(
				"Test01_Warning",
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef\r\n" +
				"0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef 0123456789abcdef",
				new I2Size(790, 350)
				);
		}
	}
}
