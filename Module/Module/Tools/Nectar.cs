using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Charlotte.Tools
{
	/// <summary>
	/// XXX botsu
	/// </summary>
	public class Nectar : IDisposable
	{
		private string _ident;
		private NamedEventData _evReady;
		private NamedEventData _evData;
		private NamedEventData _evSync;
		private int _timeoutMillis;

		public Nectar(string name, int timeoutMillis = 2000)
		{
			_ident = "{4498e8a7-3511-4512-8ac3-f58c27da720c}_" + SecurityTools.GetSHA512_128String(Encoding.UTF8.GetBytes(name));

			_evReady = new NamedEventData(_ident + "_Ready");
			_evData = new NamedEventData(_ident + "_Data");
			_evSync = new NamedEventData(_ident + "_Sync");

			_timeoutMillis = timeoutMillis;
		}

		public string GetIdent()
		{
			return _ident;
		}

		/// <summary>
		/// タイムアウトすると例外を投げる。
		/// </summary>
		/// <param name="data"></param>
		public void Send(byte[] data)
		{
			for (int index = 0; index < data.Length; index++)
			{
				for (int bit = 1 << 7; bit != 0; bit >>= 1)
				{
					this.SendBit((data[index] & bit) != 0);
				}
			}
		}

		private void SendBit(bool flag)
		{
			if (_evReady.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("送信タイムアウト");
			}
			if (flag)
			{
				_evData.Set();
			}
			_evSync.Set();
		}

		public byte[] Recv(int size)
		{
			byte[] buff = new byte[size];
			Recv(buff);
			return buff;
		}

		public void Recv(byte[] buff)
		{
			Recv(buff, 0, buff.Length);
		}

		/// <summary>
		/// タイムアウトすると例外を投げる。
		/// </summary>
		/// <param name="buff"></param>
		/// <param name="startPos"></param>
		/// <param name="size"></param>
		public void Recv(byte[] buff, int startPos, int size)
		{
			for (int index = 0; index < size; index++)
			{
				buff[startPos + index] = this.RecvChar();
			}
		}

		private byte RecvChar()
		{
			int chr = 0;

			for (int bit = 0; bit < 8; bit++)
			{
				chr <<= 1;
				chr |= this.RecvBit() ? 1 : 0;
			}
			return (byte)chr;
		}

		private bool RecvBit()
		{
			_evReady.Set();

			if (_evSync.WaitForMillis(_timeoutMillis) == false)
			{
				throw new Exception("受信タイムアウト");
			}
			return _evData.WaitForMillis(0);
		}

		public void Dispose()
		{
			_evReady.Dispose();
			_evData.Dispose();
			_evSync.Dispose();
		}
	}
}
