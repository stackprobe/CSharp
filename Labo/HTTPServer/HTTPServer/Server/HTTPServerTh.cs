using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Server
{
	public class HTTPServerTh : SockServerTh
	{
		public Action<HTTPServerChannel> HTTPConnected = (channel) => { };

		// <---- prm

		private static object SYNCROOT = new object();

		public HTTPServerTh()
		{
			PortNo = 80;
			Connected = (channel) =>
			{
				HTTPServerChannel hsChannel = new HTTPServerChannel();

				hsChannel.Channel = channel;
				hsChannel.RecvRequest();

				lock (SYNCROOT)
				{
					HTTPConnected(hsChannel);
				}

				hsChannel.SendResponse();
			};
		}
	}
}
