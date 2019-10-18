using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{27d946c9-0067-422a-89da-5f4e7bc482e7}";
		public const string APP_TITLE = "LinarCacheTrim";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private string LinarCacheDir = @"C:\tmp\LinarCache";

		private void Main2(ArgsReader ar)
		{
		readArgs:
			if (ar.ArgIs("/D"))
			{
				LinarCacheDir = ar.NextArg();
				goto readArgs;
			}
			if (ar.HasArgs())
				throw new Exception("不明なコマンド引数");

			Main3();
		}

		private void Main3()
		{
			// TODO
		}
	}
}
