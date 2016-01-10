using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools.Test
{
	public class HttpClientTest
	{
		public static void Test1()
		{
			GetTest("http://localhost/");
			GetTest("https://www.google.co.jp/");
			GetTest("http://localhost/Echo/Echo.cgi", Encoding.ASCII.GetBytes("Test Body"));
		}

		private static void GetTest(string url, byte[] body = null)
		{
			HttpClient hc = new HttpClient(url);
			hc.AddHeader("X-Header", "Value 123 ABC");
			hc.SendBody(body);

			DebugTools.WriteLog(url);

			foreach (string name in hc.GetResHeaders().Keys)
				DebugTools.WriteLog(name + ": " + hc.GetResHeaders()[name]);

			DebugTools.WriteLog(StringTools.ENCODING_SJIS.GetString(hc.GetResBody()));
		}
	}
}
