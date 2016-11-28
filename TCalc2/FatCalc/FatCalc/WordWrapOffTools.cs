using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Charlotte
{
	public class WordWrapOffTools
	{
		private const int EM_SETWORDBREAKPROC = 0x00D0;
		private delegate int EditWordBreakProc(IntPtr lpch, int ichCurrent, int cch, int code);

		[DllImport("user32.dll")]
		private static extern IntPtr SendMessageW(IntPtr hWndControl, int msgId, IntPtr wParam, EditWordBreakProc lParam);

		private static int EWBP_WordWrapOff(IntPtr lpch, int ichCurrent, int cch, int code)
		{
			return ichCurrent;
		}

		public static void WordWrapOff(TextBox tb)
		{
			// option
			{
				tb.Text = "";
				tb.KeyPress += delegate(object sender, KeyPressEventArgs e)
				{
					if (e.KeyChar == (char)1) // ctrl + a
					{
						tb.SelectAll();
						e.Handled = true;
					}
				};
			}

			EditWordBreakProc ewbp = new EditWordBreakProc(EWBP_WordWrapOff);
			Gnd.Anythings.Add(ewbp); // アンマネージドコードに渡す -> どこからも参照が無いように見える -> GCに解放される。-- なので Gnd で掴んでおく。
			SendMessageW(tb.Handle, EM_SETWORDBREAKPROC, IntPtr.Zero, ewbp);
		}
	}
}
