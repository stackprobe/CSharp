using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HTTPServer : SockServer
	{
		public Action<HTTPServerChannel> HTTPConnected = (channel) => { };

		// <---- prm

		public HTTPServer()
		{
			PortNo = 80;
			Connected = (channel) => HTTPConnected(new HTTPServerChannel() { Channel = channel });
		}
	}
}
