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
		public const string APP_TITLE = "CCCC";

		static void Main(string[] args)
		{
			Common.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);
		}

		private void Main2(ArgsReader ar)
		{
			Gnd.I = new Gnd();

			Gnd.I.Load(Gnd.I.SettingFile);

			MessageBox.Show(APP_TITLE);

			Gnd.I.Save(Gnd.I.SettingFile);
		}
	}
}
