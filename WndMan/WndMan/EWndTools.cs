using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndMan
{
	public class EWndTools
	{
		public class ControlInfo
		{
			public string Title;
			public string ClassName;
			public WinTools.RECT Rect;
			public IntPtr HWnd;
		}

		public delegate void FoundControl_d(ControlInfo ci);
		public delegate void EndControl_d();

		private static FoundControl_d D_FoundControl;
		private static EndControl_d D_EndControl;

		public static void FindWindows(FoundControl_d d_foundControl, EndControl_d d_endControl)
		{
			D_FoundControl = d_foundControl;
			D_EndControl = d_endControl;

			EnumWindows(EnumWindowsCallBack, IntPtr.Zero);

			D_FoundControl = null;
			D_EndControl = null;
		}

		private delegate bool EnumWindowsCallBack_d(IntPtr hWnd, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private extern static bool EnumWindows(EnumWindowsCallBack_d d_enumWindowsCallBack, IntPtr lParam);

		[DllImport("user32.dll")]
		[return: MarshalAs(UnmanagedType.Bool)]
		private extern static bool EnumChildWindows(IntPtr hWnd, EnumWindowsCallBack_d d_enumChildWindowsCallBack, IntPtr lParam);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowText(IntPtr hWnd, StringBuilder lPString, int nMaxCount);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetWindowTextLength(IntPtr hWnd);

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
		private static extern int GetClassName(IntPtr hWnd, StringBuilder lPClassName, int nMaxCount);

		private static bool EnumWindowsCallBack(IntPtr hWnd, IntPtr lParam)
		{
			ControlInfo ci = new ControlInfo();

			ci.Title = "";

			int titleLen = GetWindowTextLength(hWnd);

			if (1 <= titleLen)
			{
				StringBuilder buff = new StringBuilder(titleLen + 1);

				GetWindowText(hWnd, buff, buff.Capacity);
				ci.Title = buff.ToString();
			}

			{
				StringBuilder buff = new StringBuilder(256);

				GetClassName(hWnd, buff, buff.Capacity);
				ci.ClassName = buff.ToString();
			}

			ci.Title = ci.Title.Trim();
			ci.ClassName = ci.ClassName.Trim();
			ci.Rect = WinTools.GetRect(hWnd);
			ci.HWnd = hWnd;

			D_FoundControl(ci);

			EnumChildWindows(hWnd, EnumWindowsCallBack, IntPtr.Zero);

			D_EndControl();
			return true;
		}
	}
}
