using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class HTTPServerTest
	{
		public void Test01()
		{
			HTTPServer hs = new HTTPServer();

			hs.HTTPConnected = (channel) =>
			{
				channel.ResContentType = "text/html; charset=ASCII";
				channel.ResBody = Encoding.ASCII.GetBytes("<html><body><h1>200</h1></body></html>");
			};

			Console.WriteLine("Press ENTER to stop the server");

			hs.Perform();
		}
	}
}
