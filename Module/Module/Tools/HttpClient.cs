﻿using System;
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