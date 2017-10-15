using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class CrypTunnelProc
	{
		private Process Proc = null;

		/// <summary>
		/// 停止していれば起動する。
		/// </summary>
		public void Wake()
		{
			// TODO
		}

		/// <summary>
		/// 起動中であれば停止する。
		/// </summary>
		/// <returns>停止している</returns>
		public bool End()
		{
			return true; // TODO
		}
	}
}
