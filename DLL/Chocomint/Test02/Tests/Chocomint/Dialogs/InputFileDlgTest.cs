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

		public void Test02_Load()
		{
			string ret = InputFileDlgTools.Load("Test02_Title_Load", "Test02_Prompt");

			MessageDlgTools.Information("情報", "ret: " + ret);
		}

		public void Test02_Save()
		{
			string ret = InputFileDlgTools.Save("Test02_Title_Save", "Test02_Prompt");

			MessageDlgTools.Information("情報", "ret: " + ret);
		}
	}
}
