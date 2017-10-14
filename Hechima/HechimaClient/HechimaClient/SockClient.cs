using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net.Sockets;

namespace Charlotte
{
	public class SockClient
	{
		public delegate byte[] Recver_d(Socket sock);

		private string _serverDomain;
		private int _serverPort;
		private byte[] _sendData;
		private byte[] _recvData = null;
		private Recver_d _recver;
		private Thread _th = null;

		public void Send(string serverDomain, int serverPort, byte[] sendData, Recver_d recver)
		{
			if (_th != null)
				throw null;

			// check args {

			if (string.IsNullOrEmpty(serverDomain))
				throw null;

			if (serverPort < 1 || 65535 < serverPort)
				throw null;

			if (sendData == null)
				throw null;

			if (recver == null)
				throw null;

			// }

			_serverDomain = serverDomain;
			_serverPort = serverPort;
			_sendData = sendData;
			_recvData = null;
			_recver = recver;

			_th = new Thread((ThreadStart)delegate
			{
				_recvData = new byte[0]; // TODO
			});
			_th.Start();
		}

		public bool IsFinished() // ret: ? 未実行 || 完了
		{
			if (_th != null && _th.IsAlive == false) // ? 通信完了
				_th = null;

			return _th == null;
		}

		public byte[] GetRecvData()
		{
			if (_th != null) // ? 通信中
				throw null;

			if (_recvData == null) // ? 未受信 || 受信データは無い。
				throw null;

			return _recvData;
		}
	}
}
