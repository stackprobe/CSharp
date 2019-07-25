using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tools;
using Charlotte.Tests.Utils;
using Charlotte.Tests.Labo;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{dcddc7e4-22d8-43f9-b6c1-77daf1a8da17}";
		public const string APP_TITLE = "Cocoa";

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
			//new HeaderTableTest().Test01();
			//new ArrayUtilsTest().Test01();
			//new CsvFileSorterTest().Test01();
			//new Charlotte.wb.t20190313.Tests.EnumerableTrainTest().Test01();
			//new Charlotte.wb.t20190513.Test0001().Test01();
			//new Charlotte.wb.t20190630.Test0001().Test01();
			//new Charlotte.wb.t20190716.Test0001().Test01();
			//new Charlotte.wb.t20190716.Test0001().Test02();
			new Charlotte.wb.t20190720.Test0001().Test01();
		}
	}
}
