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

		public int ResBodySizeMax = 20000000; // 20 MB

		public void SetContentType(string contentType)
		{
			Hwr.ContentType = contentType;
		}

		public void AddHeader(string name, string value)
		{
			Hwr.Headers.Add(name, value);
		}

		public void SetProxy(string host, int port)
		{
			Hwr.Proxy = new WebProxy(host, port);
		}

		public void SetIEProxy()
		{
			Hwr.Proxy = WebRequest.GetSystemWebProxy();
		}

		public void Head()
		{
			Send(null, "HEAD");
		}

		public void Get()
		{
			Send(null);
		}

		public void Post(byte[] body)
		{
			Send(body);
		}

		public void Send(byte[] body)
		{
			Send(body, body == null ? "GET" : "POST");
		}

		public void Send(byte[] body, string method)
		{
			Hwr.Method = method;

			if (body != null)
			{
				Stream w = Hwr.GetRequestStream();
				w.Write(body, 0, body.Length);
				w.Close();
			}
			WebResponse res = Hwr.GetResponse();
			ResHeaders = DictionaryTools.CreateIgnoreCase<string>();

			foreach (string name in res.Headers.Keys)
				ResHeaders.Add(name, res.Headers[name]);

			ResBody = FileTools.ReadToEnd(res.GetResponseStream(), false, this.ResBodySizeMax);
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
