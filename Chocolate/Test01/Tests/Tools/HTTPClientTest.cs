using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

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
	}
}
