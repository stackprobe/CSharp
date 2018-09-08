using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Tests.Tools
{
	public class HTTPClientTest
	{
		public void Test01()
		{
			HTTPClient hc = new HTTPClient("http://www.example.com/index.html");

			hc.Get();

			foreach (KeyValuePair<string, string> pair in hc.ResHeaders)
			{
				Console.WriteLine(pair.Key + ": " + pair.Value);
			}
			Console.WriteLine("Body: " + Encoding.ASCII.GetString(hc.ResBody));
		}

		public void Test02()
		{
			for (int c = 0; c < 10; c++)
			{
				HTTPClient hc = new HTTPClient("https://github.com/stackprobe");
				//HTTPClient hc = new HTTPClient("https://example.com/");

				hc.Get();

				foreach (KeyValuePair<string, string> pair in hc.ResHeaders)
				{
					Console.WriteLine(pair.Key + ": " + pair.Value);
				}
				Console.WriteLine("Body: " + Encoding.ASCII.GetString(hc.ResBody));

				Thread.Sleep(500);
			}
		}
	}
}
