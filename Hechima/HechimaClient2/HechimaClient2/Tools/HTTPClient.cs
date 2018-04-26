using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Charlotte.Tools
{
	public class HTTPClient
	{
		private HttpWebRequest _hwr;

		public HTTPClient(string url)
		{
			_hwr = (HttpWebRequest)HttpWebRequest.Create(url);

			this.connectionTimeoutMillis = 20000;
			this.setProxyNone();
		}

		/// <summary>
		/// 接続を試みてから、応答ヘッダを受信し終えるまでのタイムアウト
		/// ミリ秒
		/// </summary>
		public int connectionTimeoutMillis
		{
			set
			{
				_hwr.Timeout = value;
			}
		}

		/// <summary>
		/// 接続を試みてから、全て送受信し終えるまでのタイムアウト
		/// ミリ秒
		/// </summary>
		public int timeoutMillis = 30000;

		/// <summary>
		/// 応答ヘッダを受信し終えてから～全て送受信し終えるまでの間の、無通信タイムアウト
		/// ミリ秒
		/// </summary>
		public int noTrafficTimeoutMillis = 15000;

		// memo
		//
		// 応答ヘッダ受信を a ミリ秒, 応答ボディ受信を b ～ (b + c) ミリ秒, 最長無通信時間を c ミリ秒
		// cto, to, ntto == a, (a + b), c
		//
		// to は、最悪 ntto 延長する。
		// cto < to にしておくこと。(to <= cto のとき、接続は cto 待ち、応答ボディは受信できるか不定とする)

		public int resBodySizeMax = 20000000; // 20 MB

		public enum Version_e
		{
			HTTP_10,
			HTTP_11,
		};

		public void setVersion(Version_e version)
		{
			switch (version)
			{
				case Version_e.HTTP_10:
					_hwr.ProtocolVersion = HttpVersion.Version10;
					break;

				case Version_e.HTTP_11:
					_hwr.ProtocolVersion = HttpVersion.Version11;
					break;

				default:
					throw null;
			}
		}

		public void setAuthorization(string user, string password)
		{
			string plain = user + ":" + password;
			string enc = System.Convert.ToBase64String(Encoding.UTF8.GetBytes(plain));
			this.addHeader("Authorization", "Basic " + enc);
		}

		public void addHeader(string name, string value)
		{
			if (StringTools.equalsIgnoreCase(name, "Content-Type"))
			{
				_hwr.ContentType = value;
				return;
			}
			if (StringTools.equalsIgnoreCase(name, "User-Agent"))
			{
				_hwr.UserAgent = value;
				return;
			}
			_hwr.Headers.Add(name, value);
		}

		public void setProxyNone()
		{
			_hwr.Proxy = null;
			//Hwr.Proxy = GlobalProxySelection.GetEmptyWebProxy(); // 古い実装
		}

		public void setIEProxy()
		{
			_hwr.Proxy = WebRequest.GetSystemWebProxy();
		}

		public void setProxy(string host, int port)
		{
			_hwr.Proxy = new WebProxy(host, port);
		}

		public void head()
		{
			send(null, "HEAD");
		}

		public void get()
		{
			send(null);
		}

		public void post(byte[] body)
		{
			send(body);
		}

		public void send(byte[] body)
		{
			send(body, body == null ? "GET" : "POST");
		}

		public void send(byte[] body, string method)
		{
			DateTime startedTime = DateTime.Now;
			TimeSpan timeoutSpan = TimeSpan.FromMilliseconds(timeoutMillis);

			_hwr.Method = method;

			if (body != null)
			{
				_hwr.ContentLength = body.Length;

				using (Stream w = _hwr.GetRequestStream())
				{
					w.Write(body, 0, body.Length);
					w.Flush();
				}
			}
			using (WebResponse res = _hwr.GetResponse())
			{
				resHeaders = DictionaryTools.createIgnoreCase<string>();

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

						resHeaders.Add(name, res.Headers[name]);
					}
				}

				// body
				{
					int totalSize = 0;

					using (Stream r = res.GetResponseStream())
					using (MemoryStream w = new MemoryStream())
					{
						r.ReadTimeout = noTrafficTimeoutMillis; // この時間経過すると r.Read() が例外を投げる。

						byte[] buff = new byte[20000000]; // 20 MB

						for (; ; )
						{
							int readSize = r.Read(buff, 0, buff.Length);

							if (readSize <= 0)
								break;

							if (timeoutSpan < DateTime.Now - startedTime)
								throw new Exception("Response timed out");

							totalSize += readSize;

							if (resBodySizeMax < totalSize)
								throw new Exception("Response body too large.");

							w.Write(buff, 0, readSize);
						}
						resBody = w.ToArray();
					}
				}
			}
		}

		public Dictionary<string, string> resHeaders;
		public byte[] resBody;
	}
}
