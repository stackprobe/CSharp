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

			hs.HTTPConnected = channel =>
			{
				channel.ResContentType = "text/html; charset=ASCII";
				channel.ResBody_B = Encoding.ASCII.GetBytes("<html><body><h1>200</h1></body></html>");
			};

			Console.WriteLine("Press ENTER to stop the server");

			hs.Perform();
		}

		private StringBuilder _buff;

		public void Test02()
		{
			HTTPServer hs = new HTTPServer();

			hs.HTTPConnected = channel =>
			{
				//channel.ResStatus = 200;
				channel.ResContentType = "text/html; charset=UTF-8";

				_buff = new StringBuilder();

				_buff.Append("<html>");
				_buff.Append("<body>");
				_buff.Append("<table border=\"1\">");

				AddTr("method", channel.Method);
				AddTr("path", channel.Path);
				AddTr("httpVersion", channel.HTTPVersion);

				foreach (string[] pair in channel.HeaderPairs)
				{
					AddTr("header_" + pair[0], pair[1]);
				}
				AddTr("body-length", "" + channel.Body.Length);

				_buff.Append("</table>");
				_buff.Append("</body>");
				_buff.Append("</html>");

				channel.ResBody_B = Encoding.UTF8.GetBytes(_buff.ToString());
			};

			Console.WriteLine("Press ENTER to stop the server");

			hs.Perform();
		}

		private void AddTr(string name, string value)
		{
			_buff.Append("<tr>");
			_buff.Append("<th>");
			_buff.Append(name);
			_buff.Append("</th>");
			_buff.Append("<td>");
			_buff.Append(value);
			_buff.Append("</td>");
			_buff.Append("</tr>");
		}
	}
}
