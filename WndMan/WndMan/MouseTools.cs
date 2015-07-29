﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Threading;

namespace WndMan
{
	public class MouseTools
	{
		public struct POINT
		{
			public int X;
			public int Y;
		}

		[DllImport("user32.dll")]
		private extern static bool GetCursorPos(out POINT lpPoint);

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern void SetCursorPos(int x, int y);

		[DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
		private static extern void mouse_event(int dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

		private const int MOUSEEVENTF_LEFTDOWN = 0x02;
		private const int MOUSEEVENTF_LEFTUP = 0x04;
		private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
		private const int MOUSEEVENTF_RIGHTUP = 0x10;

		public static POINT GetCursor()
		{
			POINT pt;

			if (GetCursorPos(out pt) == false) // ? 失敗
			{
				pt.X = 0;
				pt.Y = 0;
			}
			return pt;
		}

		public static void SetCursor(POINT pt)
		{
			SetCursorPos(pt.X, pt.Y);
		}

		public static void LeftDown()
		{
			mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, 0);
		}

		public static void LeftUp()
		{
			mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, 0);
		}

		public static void RightDown()
		{
			mouse_event(MOUSEEVENTF_RIGHTDOWN, 0, 0, 0, 0);
		}

		public static void RightUp()
		{
			mouse_event(MOUSEEVENTF_RIGHTUP, 0, 0, 0, 0);
		}

		private const int CLICK_BEFORE_MILLIS = 10;
		private const int CLICK_MOUSE_DOWN_MILLIS = 30;
		private const int CLICK_AFTER_MILLIS = 10;

		public static void LeftDblClick()
		{
			LeftClick();
			LeftClick();
		}

		public static void LeftClick_KeepPos(POINT pt)
		{
			POINT bk = GetCursor();

			SetCursor(pt);
			LeftClick();
			SetCursor(bk);
		}

		public static void LeftClick()
		{
			LeftClick(CLICK_BEFORE_MILLIS, CLICK_MOUSE_DOWN_MILLIS, CLICK_AFTER_MILLIS);
		}

		public static void LeftClick(int beforeMillis, int downMillis, int afterMillis)
		{
			Thread.Sleep(beforeMillis);
			LeftDown();
			Thread.Sleep(downMillis);
			LeftUp();
			Thread.Sleep(afterMillis);
		}
	}
}
