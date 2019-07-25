﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public class BusyDlgTools
	{
		public static void Show(string title, string message, Action routine, bool hasParent = false)
		{
			using (BusyDlg f = new BusyDlg())
			using (ThreadEx th = new ThreadEx(routine))
			{
				f.Th = th;

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
