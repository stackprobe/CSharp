using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace Charlotte.Tools
{
	public class HTTPClient
	{
		private HttpWebRequest Inner;

		public HTTPClient(string url)
		{
			if (InitOnceDone == false)
			{
				InitOnce();
				InitOnceDone = true;
			}

			this.Inner = (HttpWebRequest)HttpWebRequest.Create(url);
			this.Inner.ServicePoint.Expect100Continue = false;
			this.ConnectionTimeoutMillis = 20000;
			this.SetProxyNone();
		}

		private static bool InitOnceDone;

		private static void InitOnce()
		{
			// どんな証明書も許可する。
			ServicePointManager.ServerCertificateValidationCallback =
				(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true;

			// TLS 1.2
			ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
		}

		/// <summary>
		/// 接続を試みてから、応答ヘッダを受信し終えるまでのタイムアウト
		/// ミリ秒
		/// </summary>
		public int ConnectionTimeoutMillis
		{
			set { this.Inner.Timeout = value; }
		}

		/// <summary>
		/// 接続を試みてから、全て送受信し終えるまでのタイムアウト
		/// ミリ秒
		/// </summary>
		public int TimeoutMillis = 30000;

		/// <summary>
		/// 応答ヘッダを受信し終えてから～全て送受信し終えるまでの間の、無通信タイムアウト
		/// ミリ秒
		/// </summary>
		public int NoTrafficTimeoutMillis = 15000;

		// memo
		//
		// 応答ヘッダ受信を a ミリ秒, 応答ボディ受信を b ～ (b + c) ミリ秒, 最長無通信時間を c ミリ秒
		// cto, to, ntto == a, (a + b), c
		//
		// to は、最悪 ntto 延長する。
		// cto < to にしておくこと。(to <= cto のとき、接続は cto 待ち、応答ボディは受信できるか不定とする)

		public int ResBodySizeMax = 20000000; // 20 MB

		public enum Version_e
		{
			v10,
			v11,
		};

		public void SetVersion(Version_e version)
		{
			switch (version)
			{
				case Version_e.v10:
					this.Inner.ProtocolVersion = HttpVersion.Version10;
					break;

				case Version_e.v11:
					this.Inner.ProtocolVersion = HttpVersion.Version11;
					break;

				default:
					throw null;
			}
		}

		public void SetAuthorization(string user, string password)
		{
			String plain = user + ":" + password;
			String enc = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
			this.AddHeader("Authorization", "Basic " + enc);
		}

		public void AddHeader(string name, string value)
		{
			if (StringTools.EqualsIgnoreCase(name, "Content-Type"))
			{
				this.Inner.ContentType = value;
				return;
			}
			if (StringTools.EqualsIgnoreCase(name, "User-Agent"))
			{
				this.Inner.UserAgent = value;
				return;
			}
			this.Inner.Headers.Add(name, value);
		}

		public void SetProxyNone()
		{
			this.Inner.Proxy = null;
			//Hwr.Proxy = GlobalProxySelection.GetEmptyWebProxy(); // 古い実装
		}

		public void SetIEProxy()
		{
			this.Inner.Proxy = WebRequest.GetSystemWebProxy();
		}

		public void SetProxy(string host, int port)
		{
			this.Inner.Proxy = new WebProxy(host, port);
		}

		public void Head()
		{
			this.Send(null, "HEAD");
		}

		public void Get()
		{
			this.Send(null);
		}

		public void Post(byte[] body)
		{
			this.Send(body);
		}

		public void Send(byte[] body)
		{
			this.Send(body, body == null ? "GET" : "POST");
		}

		public void Send(byte[] body, string method)
		{
			DateTime startedTime = DateTime.Now;
			TimeSpan timeoutSpan = TimeSpan.FromMilliseconds(TimeoutMillis);

			this.Inner.Method = method;

			if (body != null)
			{
				this.Inner.ContentLength = body.Length;

				using (Stream w = this.Inner.GetRequestStream())
				{
					w.Write(body, 0, body.Length);
					w.Flush();
				}
			}
			using (WebResponse res = this.Inner.GetResponse())
			{
				ResHeaders = DictionaryTools.CreateIgnoreCase<string>();

				// header
				{
					const int LEN_MAX = 500000; // 500 KB
					const int WEIGHT = 10;
					int totalLen = 0;

					foreach (string name in res.Headers.Keys)
					{
						if (LEN_MAX < name.Length)
							throw new Exception("Response header too large. n");

						totalLen += name.Length + WEIGHT;

						if (LEN_MAX < totalLen)
							throw new Exception("Response header too large. t1");

						string value = res.Headers[name];

						if (LEN_MAX < value.Length)
							throw new Exception("Response header too large. v");

						totalLen += value.Length + WEIGHT;

						if (LEN_MAX < totalLen)
							throw new Exception("Response header too large. t2");

						ResHeaders.Add(name, res.Headers[name]);
					}
				}

				// body
				{
					int totalSize = 0;

					using (Stream r = res.GetResponseStream())
					using (MemoryStream w = new MemoryStream())
					{
						r.ReadTimeout = this.NoTrafficTimeoutMillis; // この時間経過すると r.Read() が例外を投げる。

						byte[] buff = new byte[20000000]; // 20 MB

						for (; ; )
						{
							int readSize = r.Read(buff, 0, buff.Length);

							if (readSize <= 0)
								break;

							if (timeoutSpan < DateTime.Now - startedTime)
								throw new Exception("Response timed out");

							totalSize += readSize;

							if (ResBodySizeMax < totalSize)
								throw new Exception("Response body too large.");

							w.Write(buff, 0, readSize);
						}
						ResBody = w.ToArray();
					}
				}
			}
		}

		public Dictionary<string, string> ResHeaders;
		public byte[] ResBody;
	}
}
