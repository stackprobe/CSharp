using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using Charlotte.Tools;

namespace Charlotte.ScrMusHook
{
	public class ScrMusHook0001 // -- 0001
	{
		public delegate int HOOKPROC_d(int code, IntPtr wParam, IntPtr lParam);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern IntPtr SetWindowsHookEx(int idHook, HOOKPROC_d hookProc, IntPtr hmod, uint dwThreadId);

		[DllImport("user32.dll", SetLastError = true)]
		public static extern bool UnhookWindowsHookEx(IntPtr hhk);

		[DllImport("user32.dll")]
		public static extern int CallNextHookEx(IntPtr hhk, int code, IntPtr wParam, IntPtr lParam);

		[DllImport("kernel32.dll")]
		public static extern uint GetLastError();

		[DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
		public static extern IntPtr LoadLibrary(string lpFileName);

		[StructLayout(LayoutKind.Sequential)]
		public struct CWPSTRUCT
		{
			public IntPtr lParam;
			public IntPtr wParam;
			public int message;
			public IntPtr hwnd;
		}

		public const int WH_CALLWNDPROC = 4;
		public const int WH_CALLWNDPROCRET = 12;
		public const int WH_GETMESSAGE = 3;
		public const int WH_SYSMSGFILTER = 6;

		public IntPtr H_CWP;
		public IntPtr H_CWPR;
		public IntPtr H_GM;
		public IntPtr H_SMF;

		private object SYNCROOT = new object();
		private Queue<string> Messages = new Queue<string>();

		public string NextMessage()
		{
			lock (SYNCROOT)
			{
				if (1 <= Messages.Count)
				{
					return Messages.Dequeue();
				}
			}
			return null;
		}

		public int F_Common(int code, IntPtr wParam, IntPtr lParam)
		{
			lock (SYNCROOT)
			{
				Messages.Enqueue(code + ", " + wParam + ", " + lParam);
			}
			return CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
		}

		public int F_CWP(int code, IntPtr wParam, IntPtr lParam)
		{
			return F_Common(code, wParam, lParam);
		}

		public int F_CWPR(int code, IntPtr wParam, IntPtr lParam)
		{
			return F_Common(code, wParam, lParam);
		}

		public int F_GM(int code, IntPtr wParam, IntPtr lParam)
		{
			return F_Common(code, wParam, lParam);
		}

		public int F_SMF(int code, IntPtr wParam, IntPtr lParam)
		{
			return F_Common(code, wParam, lParam);
		}

		public void Hook()
		{
			//Module[] modules = Assembly.GetExecutingAssembly().GetModules();
			//IntPtr hm = Marshal.GetHINSTANCE(modules[0]);
			IntPtr hm = LoadLibrary("user32.dll");
			uint tid = 0;

			H_CWP = SetWindowsHookEx(WH_CALLWNDPROC, F_CWP, hm, tid);
			uint errorCode1 = GetLastError();
			H_CWPR = SetWindowsHookEx(WH_CALLWNDPROCRET, F_CWPR, hm, tid);
			uint errorCode2 = GetLastError();
			H_GM = SetWindowsHookEx(WH_GETMESSAGE, F_GM, hm, tid);
			uint errorCode3 = GetLastError();
			H_SMF = SetWindowsHookEx(WH_SYSMSGFILTER, F_SMF, hm, tid);
			uint errorCode4 = GetLastError();

			lock (SYNCROOT)
			{
				Messages.Enqueue("hm = " + hm);

				Messages.Enqueue("h1 = " + H_CWP);
				Messages.Enqueue("h2 = " + H_CWPR);
				Messages.Enqueue("h3 = " + H_GM);
				Messages.Enqueue("h4 = " + H_SMF);

				Messages.Enqueue("ec1 = " + errorCode1);
				Messages.Enqueue("ec2 = " + errorCode2);
				Messages.Enqueue("ec3 = " + errorCode3);
				Messages.Enqueue("ec4 = " + errorCode4);
			}
		}

		public void Unhook()
		{
			UnhookHandle(H_CWP);
			UnhookHandle(H_CWPR);
			UnhookHandle(H_GM);
			UnhookHandle(H_SMF);
		}

		private void UnhookHandle(IntPtr hhk)
		{
			if (hhk != IntPtr.Zero)
			{
				UnhookWindowsHookEx(hhk);
			}
		}
	}
}
