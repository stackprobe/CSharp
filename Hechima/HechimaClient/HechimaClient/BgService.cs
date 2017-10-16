using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.Net.Sockets;
using Charlotte.Tools;

namespace Charlotte
{
	public class BgService
	{
		public Queue<string> SendingMessages = new Queue<string>();
		public Queue<Remark> RecvedRemarks = new Queue<Remark>();
		public long KnownStamp = 0L;

		private bool _waked = false;
		private SockClient _sockClient = new SockClient();
		private CrypTunnelProc _crypTunnelProc = new CrypTunnelProc();

		private int _freezeCount = 10;
		private int _recvFreezeCount = 30;//50;
		private int _recvFreezeCountBgn = 30;//50;

		public void Perform()
		{
			if (_waked == false)
			{
				// Wakeを何度も呼び出すと、ポート被ってエラーった時、何度もエラーdlgが表示されてしまう。
				_crypTunnelProc.Wake();
				_waked = true;
			}
			if (0 < _freezeCount)
			{
				_freezeCount--;
				return;
			}
			if (_sockClient.IsFinished() == false)
				return;

			if (1 <= this.RecvedRemarks.Count) // ? 受信データがまだ処理されていない。-- this.KnownStamp の更新待ちのため。
				return;

			{
				byte[] recvedData = _sockClient.GetRecvData();

				_sockClient.ClearRecvData();

				// zantei >
				// 連投した自分の発言が数秒間消えたように見える問題対策
				if (1 <= this.SendingMessages.Count)
					recvedData = null;
				// < zantei

				if (recvedData != null)
				{
					new RecvedDataToRecvedRemarks(recvedData, this.RecvedRemarks).Perform();

					// this.KnownStamp は呼び出し側で更新してもらうことにした。

					_recvFreezeCount = 30;// 50;
					_recvFreezeCountBgn = 30;// 50;

					return;
				}
			}

			if (1 <= this.SendingMessages.Count)
			{
				string message = this.SendingMessages.Dequeue();

				message = Common.ToFairMessage(message);

				if (message == "") // 空の発言は送信しない。
					return;

				byte[] sendData = StringTools.ENCODING_SJIS.GetBytes("REMARK\r\n" + Gnd.UserRealName + "\r\n" + message + "\r\n");

				_sockClient.Send("localhost", Gnd.setting.crypTunnelPort, sendData, delegate(NetworkStream ns)
				{
					return null;
				});
				_freezeCount = 10;
				_recvFreezeCount = 30;// 50; // 連投すると GET-REMARKS は先送りされ続ける。
				return;
			}
			if (0 < _recvFreezeCount)
			{
				_recvFreezeCount--;
				return;
			}

			{
				byte[] sendData = StringTools.ENCODING_SJIS.GetBytes("GET-REMARKS\r\n" + Gnd.UserRealName + "\r\n" + this.KnownStamp + "\r\n");

				_sockClient.Send("localhost", Gnd.setting.crypTunnelPort, sendData, delegate(NetworkStream ns)
				{
					List<byte> recvedData = null;
					List<byte> buff = new List<byte>();

					for (; ; )
					{
						int chr = ns.ReadByte();

						if (chr == -1)
							break;

						if (chr == 0xff) // ? Ender
						{
							if (buff.Count == 0) // ? Ender x2
								break;

							if (recvedData == null)
								recvedData = new List<byte>();

							recvedData.AddRange(buff);
							buff.Clear();
						}
						else
							buff.Add((byte)chr);
					}
					if (recvedData == null)
						return null;

					return recvedData.ToArray();
				});
			}

			_recvFreezeCount = _recvFreezeCountBgn;

			if (_recvFreezeCountBgn < 150)
				_recvFreezeCountBgn++;
		}

		private class RecvedDataToRecvedRemarks
		{
			private byte[] _recvedData;
			private Queue<Remark> _dest;

			public RecvedDataToRecvedRemarks(byte[] recvedData, Queue<Remark> dest)
			{
				_recvedData = recvedData;
				_dest = dest;
			}

			private int _rPos;

			private byte[] ReadLine()
			{
				List<byte> buff = new List<byte>();

				for (; ; )
				{
					byte chr = _recvedData[_rPos++];

					if (chr == 0x0d) // CR
						continue;

					if (chr == 0x0a) // LF
						break;

					buff.Add(chr);
				}
				return buff.ToArray();
			}

			public void Perform()
			{
				while (_rPos < _recvedData.Length)
				{
					Remark remark = new Remark();

					remark.Stamp = long.Parse(JString.toJString(ReadLine(), false, false, false, false));
					remark.Ident = JString.toJString(ReadLine(), true, false, false, true).Trim();
					remark.Message = JString.toJString(ReadLine(), true, false, false, true).Trim();

					_dest.Enqueue(remark);
				}
			}
		}

		/// <summary>
		/// 終了をリクエストする。
		/// trueを返すまで何度も呼び出すこと。
		/// </summary>
		/// <returns>終了した</returns>
		public bool End()
		{
			_waked = false;
			return _sockClient.IsFinished() && _crypTunnelProc.End();
		}
	}
}
