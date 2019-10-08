using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Windows.Forms;

namespace Charlotte.Chocomint.Dialogs
{
	public static class WaitDlgTools
	{
		private static VisitorCounter WaitDlgVCnt = new VisitorCounter();

		public static void Show(string title, string message, Action routine, Func<double> interlude, Action interlude_cancelled, bool hasParent = false)
		{
			if (WaitDlgVCnt.HasVisitor())
			{
				routine();
				return;
			}

			using (WaitDlgVCnt.Section())
			using (WaitDlg f = new WaitDlg())
			using (ThreadEx th = new ThreadEx(routine))
			{
				f.Th = th;
				f.Interlude = interlude;
				f.Interlude_Cancelled = interlude_cancelled;

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
					f.Message.Text = message;
				};

				f.ShowDialog();

				th.RelayThrow();
			}
		}
	}
}
