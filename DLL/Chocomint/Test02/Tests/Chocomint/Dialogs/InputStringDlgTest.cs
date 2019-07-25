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
					f.Validator = ret =>
					{
						if (ret == "")
							throw new Exception("空っぽ！");

						if (ret == "すあま")
							throw new Exception("「すあま」以外で！");

						return ret;
					};
				};

				f.ShowDialog();

				if (f.OkPressed)
				{
					MessageBox.Show("入力された文字列：" + f.Value);
				}
			}
		}

		public void Test03()
		{
			using (InputStringDlg f = new InputStringDlg())
			{
				f.Value = "-2100000000";

				f.PostShown = () =>
				{
					f.Text = "数値入力";
					f.Prompt.Text = "数値を入力して下さい。";
					f.TextValue.MaxLength = 11;
					f.TextValue.TextAlign = HorizontalAlignment.Right;
					f.Validator = ret => "" + int.Parse(ret);
				};

				f.ShowDialog();
			}
		}

		public void Test04()
		{
			MessageDlgTools.Information("info", "ret=" + InputStringDlgTools.Double("input double", "input double :"));
		}
	}
}
