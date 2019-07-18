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
		public const string APP_IDENT = "{6a706c50-0f4b-465b-9bc4-50e16b0cd468}";
		public const string APP_TITLE = "Sokoban_5_11";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			//new Test0001().Test01();
			//new Test0001().Test02();
			//new Test0001().Test03();
			new Test0001().Test04();
		}
	}
}
