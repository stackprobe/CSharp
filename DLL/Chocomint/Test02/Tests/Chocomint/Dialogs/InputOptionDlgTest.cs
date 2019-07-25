using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputOptionDlgTest
	{
		public void Test01()
		{
			using (InputOptionDlg f = new InputOptionDlg())
			{
				f.ShowDialog();
			}
		}

		public void Test02()
		{
			using (InputOptionDlg f = new InputOptionDlg())
			{
				f.Message = "いろはにほへと？";
				f.Options = "いろはにほへと".Select(chr => "" + chr).ToArray();

				f.ShowDialog();

				MessageDlgTools.Information("情報", "選択された項目：" + f.SelectedIndex);
			}
		}
	}
}
