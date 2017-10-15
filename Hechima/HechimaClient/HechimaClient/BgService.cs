using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class BgService
	{
		private SockClient _sockClient = new SockClient();
		private CrypTunnelProc _crypTunnelProc = new CrypTunnelProc();
		private bool _waked = false;

		public void Perform()
		{
			if (_waked == false)
			{
				_crypTunnelProc.Wake();
				_waked = true;
			}

			// TODO
		}

		/// <summary>
		/// 終了をリクエストする。
		/// 今プロセスを終了しても良いか返す。
		/// </summary>
		/// <returns>良い</returns>
		public bool End()
		{
			_waked = false;
			return _sockClient.IsFinished() && _crypTunnelProc.End();
		}
	}
}
