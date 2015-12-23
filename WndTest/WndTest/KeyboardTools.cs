using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace WndTest
{
	public class KeyboardTools
	{
		[DllImport("user32.dll")]
		private static extern int GetAsyncKeyState(long vKey);

		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true, CallingConvention = CallingConvention.Winapi)]
		private static extern short GetKeyState(int keyCode);
	}
}
