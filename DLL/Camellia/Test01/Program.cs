using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.Camellias;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{668f00a4-f9f2-40ff-8939-62d1915b887f}";
		public const string APP_TITLE = "Camellia";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
		}

		private void Main2(ArgsReader ar)
		{
			//new CamelliaTest().Test01();
			//new CamelliaRingCBCTest().Test01();
			new CamelliaRingCipherTest().Test01();
		}
	}
}
