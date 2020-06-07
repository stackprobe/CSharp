using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Charlotte
{
	public static class Win32
	{
		[StructLayout(LayoutKind.Sequential)]
		public class Rect
		{
			public int left;
			public int top;
			public int right;
			public int bottom;
		}

		[DllImport("user32.dll")]
		public extern static int ClipCursor(Rect rect);

		[DllImport("user32.dll")]
		public static extern bool SetCursorPos(int x, int y);

		[FlagsAttribute]
		public enum ExecutionState : uint // DWORD -> unsigned long @ WinNT.h -> WinDef.h
		{
			ES_SYSTEM_REQUIRED = 1,
			ES_DISPLAY_REQUIRED = 2,
			ES_USER_PRESENT = 4,
			ES_AWAYMODE_REQUIRED = 0x40,
			ES_CONTINUOUS = 0x80000000,
		}

		[DllImport("kernel32.dll")]
		public static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);
	}
}
