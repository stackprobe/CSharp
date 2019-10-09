using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte.Chocomint.Dialogs
{
	public static class LongMessageDlgTools
	{
		public static void Information(string title, string message, I2Size size, bool hasParent = false)
		{
			Show(LongMessageDlg.Mode_e.Information, title, message, size, hasParent);
		}

		public static void Error(string title, string message, I2Size size, bool hasParent = false)
		{
			Show(LongMessageDlg.Mode_e.Error, title, message, size, hasParent);
		}

		public static void Warning(string title, string message, I2Size size, bool hasParent = false)
		{
			Show(LongMessageDlg.Mode_e.Warning, title, message, size, hasParent);
		}

		public static void Show(LongMessageDlg.Mode_e mode, string title, string message, I2Size size, bool hasParent = false)
		{
			using (LongMessageDlg f = new LongMessageDlg())
			{
				f.Mode = mode;
				f.Message = message;
				f.DlgSize = size;

				if (hasParent)
					f.StartPosition = FormStartPosition.CenterParent;

				f.PostShown = () =>
				{
					f.Text = title;
				};

				f.ShowDialog();
			}
		}
	}
}
