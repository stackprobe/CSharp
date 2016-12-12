﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	public class Nectar2 : IDisposable
	{
		public enum E_INDEX
		{
			E_SEND,
			E_RECV,
			E_BIT_0,
			E_BIT_1,
			E_BIT_2,
			E_BIT_3,
			E_BIT_4,
			E_BIT_5,
			E_BIT_6,
			E_BIT_7,

			E_MAX, // == num of E_*
		}

		private NamedEventData[] _evs;

		public Nectar2(string ident)
		{
			_evs = new NamedEventData[(int)E_INDEX.E_MAX];

			for (int index = 0; index < (int)E_INDEX.E_MAX; index++)
			{
				_evs[index] = new NamedEventData("Nectar2_" + ident + "_" + index);
			}
		}

		public void Set(E_INDEX index)
		{
			_evs[(int)index].Set();
		}

		public bool Get(E_INDEX index, int millis = 0)
		{
			return _evs[(int)index].WaitForMillis(millis);
		}

		public void Dispose()
		{
			if (_evs != null)
			{
				for (int index = 0; index < (int)E_INDEX.E_MAX; index++)
				{
					_evs[index].Dispose();
				}
				_evs = null;
			}
		}

		public class Sender : IDisposable
		{
			private const int MESSAGE_SIZE_MAX = 2000000; // 2 MB
			private const int BUFF_MAX = 100;

			private Nectar2 _n;
			private Thread _th;
			private bool _death;
			private object SYNCROOT = new object();
			private NamedEventPair _evDoSend;
			private Queue<byte[]> _messages = new Queue<byte[]>();

			public Sender(string ident)
			{
				_n = new Nectar2(ident);
				_evDoSend = new NamedEventPair();
				_th = new Thread((ThreadStart)delegate
				{
					while (_death == false)
					{
						byte[] message;

						lock (SYNCROOT)
						{
							message = _messages.Count == 0 ? null : _messages.Dequeue();
						}
						if (message == null)
						{
							_evDoSend.WaitForMillis(2000);
						}
						else
						{
							foreach (byte chr in message)
							{
								for (int bit = 0; bit < 8; bit++)
									if ((chr & (1 << bit)) != 0)
										_n.Set((E_INDEX)((int)E_INDEX.E_BIT_0 + bit));

								_n.Get(E_INDEX.E_RECV); // clear
								_n.Set(E_INDEX.E_SEND);

								if (SyncRecv() == false) // ? 送信タイムアウト
								{
									lock (SYNCROOT)
									{
										_messages.Clear();
									}
									break;
								}
							}
						}
					}
				});
				_th.Start();
			}

			private bool SyncRecv() // ret: ? ! 送信タイムアウト
			{
				for (int c = 0; c < 5 && _death == false; c++)
					if (_n.Get(E_INDEX.E_RECV, 3000))
						return true;

				return false;
			}

			public void Send(byte[] message)
			{
				if (message == null)
					throw new ArgumentNullException();

				if (MESSAGE_SIZE_MAX < message.Length)
					return;

				lock (SYNCROOT)
				{
					if (BUFF_MAX < _messages.Count)
						return;

					_messages.Enqueue(message);
					_evDoSend.Set();
				}
			}

			public void Dispose()
			{
				if (_n != null)
				{
					_death = true;
					_evDoSend.Set();

					_th.Join();
					_th = null;

					_n.Dispose();
					_n = null;

					_evDoSend.Dispose();
					_evDoSend = null;
				}
			}
		}

		public class Recver : IDisposable
		{
			private const int MESSAGE_SIZE_MAX = 2000000; // 2 MB
			private const int BUFF_MAX = 100;

			private Nectar2 _n;
			private Thread _th;
			private bool _death;
			private object SYNCROOT = new object();
			private Queue<byte> _buff = new Queue<byte>();
			private Queue<byte[]> _messages = new Queue<byte[]>();
			private int _delimiter;

			public Recver(string ident, int delimiter = 0x00)
			{
				_n = new Nectar2(ident);
				_delimiter = delimiter;
				_th = new Thread((ThreadStart)delegate
				{
					while (_death == false)
					{
						if (_n.Get(E_INDEX.E_SEND, 2000))
						{
							int chr = 0x00;

							for (int bit = 0; bit < 8; bit++)
								if (_n.Get((E_INDEX)((int)E_INDEX.E_BIT_0 + bit)))
									chr |= 1 << bit;

							_n.Set(E_INDEX.E_RECV);

							if (chr == _delimiter)
							{
								byte[] message = _buff.ToArray();

								lock (SYNCROOT)
								{
									if (_messages.Count < BUFF_MAX)
										_messages.Enqueue(message);
								}
								_buff.Clear();
							}
							else
							{
								if (_buff.Count < MESSAGE_SIZE_MAX)
									_buff.Enqueue((byte)chr);
							}
						}
					}
				});
				_th.Start();
			}

			public byte[] Recv()
			{
				lock (SYNCROOT)
				{
					return _messages.Count == 0 ? null : _messages.Dequeue();
				}
			}

			public void Dispose()
			{
				if (_n != null)
				{
					_death = true;

					_th.Join();
					_th = null;

					_n.Dispose();
					_n = null;
				}
			}
		}
	}
}
