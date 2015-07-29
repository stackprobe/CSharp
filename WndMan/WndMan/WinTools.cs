using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndMan
{
	public class WinTools
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct RECT
		{
			public int L;
			public int T;
			public int R;
			public int B;
		}

		[DllImport("user32.dll")]
		private static extern bool GetWindowRect(IntPtr hWnd, out RECT rect);

		public static RECT GetRect(IntPtr hWnd)
		{
			RECT rect;

			if (GetWindowRect(hWnd, out rect) == false) // ? 失敗
			{
				rect.L = -1;
				rect.T = -1;
				rect.R = -1;
				rect.B = -1;
			}
			return rect;
		}

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private static extern bool SetForegroundWindow(IntPtr hWnd);

		public static void ToTop(IntPtr hWnd)
		{
			SetForegroundWindow(hWnd);
		}
	}
}
