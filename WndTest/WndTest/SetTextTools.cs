using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndTest
{
	public class SetTextTools
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, string lParam);

		private const int WM_SETTEXT = 0x000C;

		public static void SetText(IntPtr hWnd, string text)
		{
			SendMessage(hWnd, WM_SETTEXT, IntPtr.Zero, text);
		}
	}
}
