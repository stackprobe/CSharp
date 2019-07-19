using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;

namespace Test02.Tests.Chocomint.Dialogs
{
	public class InputComboDlgTest
	{
		public void Test01()
		{
			using (InputComboDlg f = new InputComboDlg())
			{
				f.ShowDialog();
			}
		}

		public void Test02()
		{
			using (InputComboDlg f = new InputComboDlg())
			{
				f.Value = 2;

				f.AddItem(1, "スアマ");
				f.AddItem(2, "すあま");
				f.AddItem(3, "SuAMa");

				f.ShowDialog();

				if (f.OkPressed)
				{
					MessageDlgTools.Information("情報", "選択された項目：" + f.Value);
				}
			}
		}
	}
}
