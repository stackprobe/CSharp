using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Tests;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{3ef0127e-8a83-4299-8af9-1f7afeacf106}";
		public const string APP_TITLE = "Knapsack";

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

		private void Main2(ArgsReader ar)
		{
			new Test0001().Test01();
		}
	}
}
