using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.ScrMusHook;
using Charlotte.ScrMusHook;
using System.Threading;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{89e57647-e4ad-4643-8bfb-f8f70be1d0d1}";
		public const string APP_TITLE = "ScrMusHook";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
		}

		private void Main2(ArgsReader ar)
		{
			//new ScrMusHook0001Test().Test01(); // not work orz
		}
	}
}
