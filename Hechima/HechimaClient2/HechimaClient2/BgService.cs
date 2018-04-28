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
		public Queue<byte[]> BouyomiChanSendDataBuff = new Queue<byte[]>();
		public long KnownStamp = 0L;
		public List<string> RecvedOnlineLines = null;

		private bool _waked = false;
		private SockClient _sockClient = new SockClient();
		private CrypTunnelProc _crypTunnelProc = new CrypTunnelProc();

		private int _freezeCount = 10;
		private int _recvFreezeCount = 30;//50;
		private int _recvFreezeCountBgn = 30;//50;
		private int _recvOnlineFreezeCount = 0;
		private List<string> _recvedOnlineLines = null; // 受信スレッドからも触るので注意！
		private bool _sentGetRemarks = false;

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

			if (1 <= this.BouyomiChanSendDataBuff.Count)
			{
				// zantei -- BCPort問題
				if (
					Gnd.setting.BouyomiChanPort == Gnd.setting.crypTunnelPort ||
					Gnd.setting.BouyomiChanPort == Gnd.setting.ServerPort
					)
					throw new Exception("Error_BCPort");

				_sockClient.Send(
					Gnd.setting.BouyomiChanDomain,
					Gnd.setting.BouyomiChanPort,
					this.BouyomiChanSendDataBuff.Dequeue(),
					delegate(NetworkStream ns)
					{
						return null;
					}
					);

				return;
			}
			if (1 <= this.RecvedRemarks.Count) // ? 受信データがまだ処理されていない。-- this.KnownStamp の更新待ちのため。
				return;

			{
				byte[] recvedData = _sockClient.GetRecvData();

				_sockClient.ClearRecvData();

				if (_sentGetRemarks)
				{
					_sentGetRemarks = false;

					if (recvedData == null)
						Gnd.NetErrorLevel += 2; // 失敗
					else
						Gnd.NetErrorLevel--; // 成功

					Gnd.NetErrorLevel = IntTools.toRange(Gnd.NetErrorLevel, 0, 10);
				}

				// zantei >
				// 連投した自分の発言が数秒間消えたように見える問題対策
				if (1 <= this.SendingMessages.Count)
					recvedData = null;
				// < zantei

				if (recvedData != null && 1 <= recvedData.Length)
				{
					new RecvedDataToRecvedRemarks(recvedData, this.RecvedRemarks).Perform();

					// this.KnownStamp は呼び出し側で更新してもらうことにした。

					_recvFreezeCount = 20;// 30;// 50;
					_recvFreezeCountBgn = 20;// 30;// 50;

					return;
				}
			}

			// online >

			if (_recvedOnlineLines != null)
			{
				this.RecvedOnlineLines = _recvedOnlineLines;
				_recvedOnlineLines = null;

				try // update Gnd.RecentlyIdents
				{
					while (30 < Gnd.RecentlyIdents.Count) // anti overflow
						Gnd.RecentlyIdents.RemoveAt(Gnd.RecentlyIdents.Count - 1);

					foreach (string line in this.RecvedOnlineLines)
					{
						string ident = line.Substring(line.IndexOf(' ') + 1);

						if (Gnd.RecentlyIdents.Contains(ident) == false)
							Gnd.RecentlyIdents.Insert(0, ident);
					}
				}
				catch (Exception e)
				{
					Gnd.Logger.writeLine(e);
				}

				return;
			}
			if (Gnd.onlineDlg != null && --_recvOnlineFreezeCount < 0)
			{
				byte[] sendData = StringTools.ENCODING_SJIS.GetBytes(Gnd.CliVerifyPtn + "GET-MEMBERS\r\n");

				_sockClient.Send("localhost", Gnd.setting.crypTunnelPort, sendData, delegate(NetworkStream ns)
				{
					List<byte> buff = new List<byte>();

					for (; ; )
					{
						int chr = ns.ReadByte();

						if (chr == -1)
							throw null;

						if (chr == 0x0d) // CR
							continue;

						if (chr == 0x0a) // LF
						{
							string line = StringTools.ENCODING_SJIS.GetString(buff.ToArray());

							buff.Clear();

							if (line == "") // Ender
								break;

							if (_recvedOnlineLines == null)
								_recvedOnlineLines = new List<string>();

							_recvedOnlineLines.Add(line);
						}
						else
							buff.Add((byte)chr);
					}
					return null;
				});
				_freezeCount = 10;
				_recvOnlineFreezeCount = 300;
				return;
			}

			// < online

			if (1 <= this.SendingMessages.Count)
			{
				string message = this.SendingMessages.Dequeue();

				message = Common.ToFairMessage(message);

				if (message == "") // 空の発言は送信しない。
					return;

				byte[] sendData = StringTools.ENCODING_SJIS.GetBytes(Gnd.CliVerifyPtn + "REMARK\r\n" + Gnd.UserRealName + "\r\n" + message + "\r\n");

				_sockClient.Send("localhost", Gnd.setting.crypTunnelPort, sendData, delegate(NetworkStream ns)
				{
					return null;
				});
				_freezeCount = 10;
				_recvFreezeCount = 1;// 30;// 50; // 連投すると GET-REMARKS は先送りされ続ける。
				return;
			}
			if (0 < _recvFreezeCount)
			{
				_recvFreezeCount--;
				return;
			}

			{
				byte[] sendData = StringTools.ENCODING_SJIS.GetBytes(Gnd.CliVerifyPtn + "GET-REMARKS\r\n" + Gnd.UserRealName + "\r\n" + this.KnownStamp + "\r\n");

				_sockClient.Send("localhost", Gnd.setting.crypTunnelPort, sendData, delegate(NetworkStream ns)
				{
					List<byte> recvedData = new List<byte>();
					List<byte> buff = new List<byte>();

					for (; ; )
					{
						int chr = ns.ReadByte();

						if (chr == -1)
							throw null;

						if (chr == 0xff) // ? Ender
						{
							if (buff.Count == 0) // ? Ender x2
								break;

							recvedData.AddRange(buff);
							buff.Clear();
						}
						else
							buff.Add((byte)chr);
					}
					return recvedData.ToArray();
				});
			}

			_freezeCount = 10;
			_recvFreezeCount = _recvFreezeCountBgn;

			if (_recvFreezeCountBgn < 150)
				_recvFreezeCountBgn++;

			_sentGetRemarks = true;
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

		public void ReqDoRecvOnline()
		{
			_recvOnlineFreezeCount = 0;
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
