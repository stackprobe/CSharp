using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.Chocomint;
using Charlotte.Tests.Chocomint.Dialogs;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{7868743f-bdb2-4a61-a2cb-4b091d815059}";
		public const string APP_TITLE = "Chocomint";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
		}

		private void Main2(ArgsReader ar)
		{
			//new InputStringDlgTest().Test01();
			//new MessageDlgTest().Test01();
			//new MessageDlgTest().Test02();
			new MessageDlgTest().Test03();
		}
	}
}
