using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{2b789c10-5722-4218-b0c3-51dfa9b2db01}";
		public const string APP_TITLE = "CCCCTMPL";

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
			MessageBox.Show(APP_TITLE); // ---- 0001
		}
	}
}
