using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class BgService
	{
		public List<string> SendingMessages = new List<string>();
		public List<Remark> RecvedRemarks = new List<Remark>();
		public long KnownStamp = 0L;

		private bool _waked = false;
		private SockClient _sockClient = new SockClient();
		private CrypTunnelProc _crypTunnelProc = new CrypTunnelProc();

		private int _sendFreezeCount = 50;
		private int _sendFreezeCountMax = 50;
		private int _recvFreezeCount = 50;
		private int _recvFreezeCountMax = 50;

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
