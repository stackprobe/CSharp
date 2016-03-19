using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class Nectar : IDisposable
	{
		private const string COMMON_ID = "{91ed4458-fe67-4093-a430-9dbf09db9904}"; // shared_uuid

		private MutexData _mtx;
		private NamedEventData _evData;
		private NamedEventData _evCtrl;
		private NamedEventData _evSync;
		private NamedEventData _evSend;
		private NamedEventData _evPost;
		private int _timeoutMillis;

		public int RecvSizeMax = 20000000;

		public Nectar(string name, int timeoutMillis = 5000)
		{
			string ident = COMMON_ID + "_" + SecurityTools.GetSHA512_128String(StringTools.ENCODING_SJIS.GetBytes(name));

			_mtx = new MutexData(ident + "_Mutex");

			using (new MutexData.Section(_mtx))
			{
				_evData = new NamedEventData(ident + "_Data");
				_evCtrl = new NamedEventData(ident + "_Ctrl");
				_evSync = new NamedEventData(ident + "_Sync");
				_evSend = new NamedEventData(ident + "_Send");
				_evPost = new NamedEventData(ident + "_Post");
			}
			_timeoutMillis = timeoutMillis;
		}

		/// <summary>
		/// タイムアウト又は送信に失敗すると例外を投げる。
		/// </summary>
		/// <param name="message"></param>
		public void Send(byte[] message)
		{
			using (new MutexData.Section(_mtx))
			{
				_evData.WaitForMillis(0); // clear
				_evCtrl.WaitForMillis(0); // clear
				_evSync.WaitForMillis(0); // clear
				_evSend.WaitForMillis(0); // clear
				_evPost.WaitForMillis(0); // clear
			}
			this.SendBit(false, true);

			for (int index = 0; index < message.Length; index++)
			{
				for (int bit = 1 << 7; bit != 0; bit >>= 1)
				{
					this.SendBit((message[index] & bit) != 0, false);
				}
			}
			this.SendBit(true, true);
		}

		private void SendBit(bool data, bool ctrl)
		{
			using (new MutexData.Section(_mtx))
			{
				if (data)
				{
					_evData.Set();
				}
				if (ctrl)
				{
					_evCtrl.Set();
				}
				_evSync.Set();
				_evSend.Set();
			}
			if (_evPost.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("送信タイムアウト");
			}
		}

		private int _chr = 0;
		private int _bIndex = 0;
		private ByteBuffer _buff = new ByteBuffer();

		/// <summary>
		/// タイムアウト又は受信に失敗すると例外を投げる。
		/// </summary>
		/// <returns></returns>
		public byte[] Recv()
		{
			for (; ; )
			{
				int ret = this.RecvBit();

				if (ret == 2)
				{
					_chr = 0;
					_bIndex = 0;
					_buff.Clear();
				}
				else if (ret == 3)
				{
					return _buff.Join();
				}
				else
				{
					_chr <<= 1;
					_chr |= ret;
					_bIndex++;

					if (_bIndex == 8)
					{
						if (this.RecvSizeMax <= _buff.Length)
						{
							throw new Exception("受信サイズ超過");
						}
						_buff.Add((byte)_chr);
						_chr = 0;
						_bIndex = 0;
					}
				}
			}
		}

		private int RecvBit()
		{
			if (_evSync.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("受信タイムアウト");
			}
			int ret = 0;

			using (new MutexData.Section(_mtx))
			{
				if (_evSend.WaitForMillis(0) == false)
				{
					throw new Exception("受信失敗");
				}
				if (_evData.WaitForMillis(0))
				{
					ret |= 1;
				}
				if (_evCtrl.WaitForMillis(0))
				{
					ret |= 2;
				}
				_evPost.Set();
			}
			return ret;
		}

		public void Dispose()
		{
			using (new MutexData.Section(_mtx))
			{
				_evData.Dispose();
				_evCtrl.Dispose();
				_evSync.Dispose();
				_evSend.Dispose();
				_evPost.Dispose();
			}
			_mtx.Dispose();
		}

		public class Sender : IDisposable
		{
			private Nectar _n;

			public Sender(string name)
			{
				_n = new Nectar(name, 30000); // 30 秒 -- 受信側が存在すること前提なので、長め。
			}

			/// <summary>
			/// タイムアウト又は送信に失敗すると例外を投げる。
			/// </summary>
			/// <param name="message"></param>
			public void Send(byte[] message)
			{
				_n.Send(message);
			}

			public void Dispose()
			{
				_n.Dispose();
			}
		}

		public class Recver : IDisposable
		{
			private Nectar _n;

			public Recver(string name)
			{
				_n = new Nectar(name, 2000); // 2 秒 -- タイムアウトしても送信中のメッセージは維持される。interrupt 確保のため、短め。
			}

			public void SetRecvSizeMax(int recvSizeMax)
			{
				_n.RecvSizeMax = recvSizeMax;
			}

			/// <summary>
			/// クライアント応答用？
			/// </summary>
			/// <param name="count"></param>
			/// <returns>null == タイムアウト又は受信に失敗</returns>
			public byte[] Recv(int count = 30) // def 30 -> 60 秒 -- 相手側の処理時間 + 応答が必ずあることが前提なので、長め。
			{
				for (int c = 0; c < count; c++)
				{
					byte[] message = this.Receipt();

					if (message != null)
					{
						return message;
					}
				}
				return null;
			}

			/// <summary>
			/// サーバー用？
			/// 受信を待機する場合、時間を空けずに繰り返し呼び出すこと。
			/// </summary>
			/// <returns></returns>
			public byte[] Receipt()
			{
				try
				{
					return _n.Recv();
				}
				catch
				{ }

				return null;
			}

			public void Dispose()
			{
				_n.Dispose();
			}
		}
	}
}
