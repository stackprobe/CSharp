using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndMan
{
	public class GetTextTools
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, int wParam, StringBuilder lParam);

		private const int WM_GETTEXT = 0x000D;

		public static string GetText(IntPtr controlHWnd)
		{
			StringBuilder buff = new StringBuilder(1024);

			SendMessage(controlHWnd, WM_GETTEXT, buff.Capacity, buff);
			return buff.ToString();
		}
	}
}
