using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace Charlotte
{
	public class SockTest
	{
		public SockTest()
		{ }

		private List<int> UsedPortNoList = new List<int>();

		public int PortNoFltr(int portNo)
		{
			for(int trycnt = 1; trycnt <= 20 && this.IsAvailablePortNo(portNo) == false; trycnt++)
			{
				portNo = (int)SystemTools.GetCryptoRand(50000, 65535);
			}
			this.UsedPortNoList.Add(portNo);
			return portNo;
		}

		private bool IsAvailablePortNo(int portNo)
		{
			if (this.UsedPortNoList.Contains(portNo))
				return false;

			return this.IsBindable(portNo);
		}

		private bool IsBindable(int portNo)
		{
			try
			{
				using (Socket sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
				{
					sock.Bind(new IPEndPoint(IPAddress.Any, portNo));
				}
				return true;
			}
			catch
			{
				return false;
			}
		}
	}
}
