using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Server;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{11c382a8-f5f7-4524-ad19-dfcfeae1ec53}";
		public const string APP_TITLE = "HTTPServer";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
#endif
		}

		private void Main2(ArgsReader ar)
		{
			HTTPServerTh server = new HTTPServerTh()
			{
				HTTPConnected = channel =>
				{
					channel.ResContentType = "text/html; charset=UTF-8";
					channel.ResBody = Encoding.UTF8.GetBytes("<html><body>It's works.</body></html>");
				},
			};

			server.Start();

			Console.WriteLine("Press ENTER to stop the server.");
			Console.ReadLine();

			server.Stop_B();
		}
	}
}
