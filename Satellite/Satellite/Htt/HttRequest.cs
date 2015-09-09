using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Satellite;
using Charlotte.Satellite.Tools;
using System.IO;

namespace Charlotte.Htt
{
	public class HttRequest
	{
		private string _clientIPAddress;
		private string _method;
		private string _urlString;
		private string _httpVersion;
		private Dictionary<string, string> _headerFields;
		private string _headerPartFile;
		private string _bodyPartFile;

		public HttRequest(ObjectList rawData)
		{
			int c = 1;

			_clientIPAddress = Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]);
			_method = Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]);
			_urlString = Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]);
			_httpVersion = Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]);

			{
				int count = int.Parse(Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]));

				_headerFields = new Dictionary<string, string>();

				for (int index = 0; index < count; index++)
				{
					String key = Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]);
					String value = Encoding.ASCII.GetString((byte[])rawData.GetList()[c++]);

					_headerFields.Add(key, value);
				}
			}

			_headerPartFile = StringTools.ENCODING_SJIS.GetString((byte[])rawData.GetList()[c++]);
			_bodyPartFile = StringTools.ENCODING_SJIS.GetString((byte[])rawData.GetList()[c++]);
		}

		public String GetClientIPAddress()
		{
			return _clientIPAddress;
		}

		public String GetMethod()
		{
			return _method;
		}

		public String GetUrlString()
		{
			return _urlString;
		}

		public Uri GetUrl()
		{
			return new Uri(_urlString);
		}

		public String GetHTTPVersion()
		{
			return _httpVersion;
		}

		public Dictionary<string, string> GetHeaderFields()
		{
			return _headerFields;
		}

		public string GetHeaderPartFile()
		{
			return _headerPartFile;
		}

		public byte[] GetHeaderPart()
		{
			return File.ReadAllBytes(_headerPartFile);
		}

		public string GetBodyPartFile()
		{
			return _bodyPartFile;
		}

		public byte[] GetBodyPart()
		{
			return File.ReadAllBytes(_bodyPartFile);
		}
	}
}
