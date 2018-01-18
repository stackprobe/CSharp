using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace IntageTS
{
	public class SSRBClientWrap
	{
		private Setting Setting;

		public SSRBClientWrap(Setting setting)
		{
			this.Setting = setting;
		}

		private SSRBClient CreateClient()
		{
			return new SSRBClient(this.Setting.SSRBServerDomain, this.Setting.SSRBServerPortNo);
		}

		public void DoTest()
		{
			SSRBClient client = this.CreateClient();

			client.SetCommands(new string[] { "@ECHO OK" });
			client.Perform();

			string text = client.GetOutText();

			if (text != "OK")
			{
				throw new Exception("SSRBServer から正しい応答を受け取れませんでした。");
			}
		}
	}
}
