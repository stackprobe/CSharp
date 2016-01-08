using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Charlotte.Tools
{
	public class HttpClient
	{
		private WebRequest Hwr;

		public HttpClient(string url)
		{
			Hwr = HttpWebRequest.Create(url);
		}
	}
}
