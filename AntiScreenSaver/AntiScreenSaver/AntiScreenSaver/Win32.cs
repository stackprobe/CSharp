using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace Charlotte
{
	public class Win32
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
	}
}
