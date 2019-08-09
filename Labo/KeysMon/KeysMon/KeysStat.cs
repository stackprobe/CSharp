using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace KeysMon
{
	public static class KeysStat
	{
		[DllImport("user32.dll")]
		static extern short GetAsyncKeyState(Keys vKey); 

		public static bool[] GetStat()
		{
			bool[] dest = new bool[256];

			for (int vk = 0; vk <= 255; vk++)
			{
				dest[vk] = GetAsyncKeyState((Keys)vk) != 0;
			}
			return dest;
		}
	}
}
