﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndTest
{
	public class SendMessageTools
	{
		[DllImport("user32.dll")]
		private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

		private const int BM_CLICK = 0x00F5;

		public static void LeftClick(IntPtr hWnd)
		{
			SendMessage(hWnd, BM_CLICK, IntPtr.Zero, IntPtr.Zero);
		}
	}
}
