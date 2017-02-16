using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Tools
{
	public class FaultOperation : Exception
	{
		public static readonly FaultOperation i = new FaultOperation();

		public FaultOperation()
			: this("失敗しました。")
		{ }

		public FaultOperation(string message)
			: base(message)
		{ }

		public static void caught(Exception e, string title = Program.APP_TITLE)
		{
			if (e is Completed)
			{
				MessageBox.Show(
					"完了しました。",
					title + " / 完了",
					MessageBoxButtons.OK,
					MessageBoxIcon.Information
					);
			}
			else if (e is Ended)
			{
				// noop
			}
			else if (e is Cancelled)
			{
				MessageBox.Show(
					"中止しました。",
					title + " / 中止",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
			else if (e is FaultOperation)
			{
				MessageBox.Show(
					getMessage(e),
					title + " / 失敗",
					MessageBoxButtons.OK,
					MessageBoxIcon.Warning
					);
			}
			else
			{
				MessageBox.Show(
					getMessage(e) + "\n----\n" + e,
					title + " / エラー",
					MessageBoxButtons.OK,
					MessageBoxIcon.Error
					);
			}
		}

		public static string getMessage(Exception e)
		{
			List<string> lines = new List<string>();

			while (e != null)
			{
				lines.Add(e.Message);
				e = e.InnerException;
			}
			return string.Join("\n", lines);
		}
	}
}
