﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace Charlotte.Tools
{
	public class SockClient : SockChannel, IDisposable
	{
		public SockClient(string domain, int portNo)
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(domain);
			IPAddress address = GetFairAddress(hostEntry.AddressList);
			IPEndPoint endPoint = new IPEndPoint(address, portNo);

			this.Handler = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
			this.Handler.Connect(endPoint);

			this.PostSetHandler();
		}

		private static IPAddress GetFairAddress(IPAddress[] addresses)
		{
			foreach (IPAddress address in addresses)
			{
				if (address.AddressFamily == AddressFamily.InterNetwork) // ? IPv4
				{
					return address;
				}
			}
			return addresses[0];
		}

		public void Dispose()
		{
			if (this.Handler != null)
			{
				try
				{
					this.Handler.Disconnect(false);
				}
				catch (Exception e)
				{
					Program.WriteLog(e);
				}

				try
				{
					this.Handler.Dispose();
				}
				catch (Exception e)
				{
					Program.WriteLog(e);
				}

				this.Handler = null;
			}
		}
	}
}
