using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using Charlotte.Tests.DDDDTMPL;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{398b42b0-b4de-44b6-8749-1481cc68ba11}";
		public const string APP_TITLE = "DDDDTMPL";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

			Console.WriteLine("Press ENTER.");
			Console.ReadLine();
		}

		private void Main2(ArgsReader ar)
		{
			new DDDDTMPL0001Test().Test01(); // -- 0001
		}
	}
}
