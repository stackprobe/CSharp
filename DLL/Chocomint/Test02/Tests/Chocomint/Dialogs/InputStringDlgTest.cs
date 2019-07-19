using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Chocomint.Dialogs;
using System.Windows.Forms;

namespace Test02.Tests.Chocomint.Dialogs
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

		public void Test02()
		{
			using (InputStringDlg f = new InputStringDlg())
			{
				f.Value = "すあま";

				f.PostShown = () =>
				{
					f.Text = "好きな食べ物";
					f.Prompt.Text = "好きな食べ物（３文字まで）";
					f.TextValue.MaxLength = 3;
					f.Validator = v =>
					{
						if (v == "")
							throw new Exception("空っぽ！");

						if (v == "すあま")
							throw new Exception("「すあま」以外で！");
					};
				};

				f.ShowDialog();

				if (f.OkPressed)
				{
					MessageBox.Show("入力された文字列：" + f.Value);
				}
			}
		}
	}
}
