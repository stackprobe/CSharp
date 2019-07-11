using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Charlotte
{
	public class KeysMon
	{
		private bool[] LastStat = null;

		public void DoCheck()
		{
			bool[] stat = KeysStat.GetStat();

			if (this.LastStat != null && IsSameStat(this.LastStat, stat) == false)
				this.Touched = true;

			this.LastStat = stat;
		}

		private bool IsSameStat(bool[] a, bool[] b)
		{
			for (int vk = 0; vk <= 255; vk++)
				if (a[vk] != b[vk])
					return false;

			return true;
		}

		private bool Touched = false;

		public bool IsTouched()
		{
			if (this.Touched)
			{
				this.Touched = false;
				return true;
			}
			return false;
		}

		public class KeysStat
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
}
