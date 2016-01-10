using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Charlotte.Tools
{
	public class HttpClient
	{
		private WebRequest Hwr;

		public HttpClient(string url)
		{
			Hwr = HttpWebRequest.Create(url);
			Hwr.Timeout = 20000;
		}

		public void SetContentType(string contentType)
		{
			Hwr.ContentType = contentType;
		}

		public void AddHeader(string name, string value)
		{
			Hwr.Headers.Add(name, value);
		}

		public void SendBody(byte[] body)
		{
			if (body == null)
			{
				Hwr.Method = "GET";
			}
			else
			{
				Hwr.Method = "POST";
				Hwr.GetRequestStream().Write(body, 0, body.Length);
				Hwr.GetRequestStream().Close();
			}
			WebResponse res = Hwr.GetResponse();
			ResHeaders = new Dictionary<string, string>();

			foreach (string name in res.Headers.Keys)
				ResHeaders.Add(name, res.Headers[name]);

			ResBody = StreamTools.ReadToEnd(res.GetResponseStream());
		}

		private Dictionary<string, string> ResHeaders;
		private byte[] ResBody;

		public Dictionary<string, string> GetResHeaders()
		{
			return ResHeaders;
		}

		public byte[] GetResBody()
		{
			return ResBody;
		}
	}
}
