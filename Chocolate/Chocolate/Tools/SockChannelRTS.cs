using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;

namespace Charlotte.Tools
{
	public class SockChannelRTS<T>
	{
		public SockChannelRTS_Consts.Phase_e Phase;
		public Socket Handler;
		public T Session;
		public byte[] SendData;
		public int SendOffset;
	}
}
