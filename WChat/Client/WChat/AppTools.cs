using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;

namespace Charlotte
{
	public class AppTools
	{
		public static void Browse(string domain = "localhost", int portNo = 80, string path = "")
		{
			string portPart;

			if(portNo == 80)
				portPart = "";
			else
				portPart = ":" + portNo;

			BrowseUrl("http://" + domain + portPart + "/" + path);
		}

		public static void BrowseUrl(string url)
		{
			try
			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = url;

				Process.Start(psi);
			}
			catch (Exception e)
			{
				SystemTools.WriteLog(e);
			}
		}

		public static string GetUrlPath(string trackName, string path, bool dirFlag)
		{
			path = path.Replace('\\', '/');

			if (path.EndsWith("/"))
				path.Substring(0, path.Length - 1);

			path = StringTools.EncodeUrl(path);

			if (dirFlag)
				path += '/';

			return trackName + ":CLIENT/" + path;
		}
	}
}
