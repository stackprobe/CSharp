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
		public const string APP_IDENT = "{cad9a811-0d9e-431a-af59-9661429077ed}";
		public const string APP_TITLE = "Test02";

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
			//new HeaderTableTest().Test01();
			//new ArrayUtilsTest().Test01();
			//new CsvFileSorterTest().Test01();
			//new Test0002().Test01();
			//new Test0002().Test02();
			//new Test0002().Test02_B();
			//new Charlotte.wb.t20190313.Tests.EnumerableTrainTest().Test01();
			//new Charlotte.wb.t20190513.Test0001().Test01();
			//new Charlotte.wb.t20190630.Test0001().Test01();
			//new Charlotte.wb.t20190716.Test0001().Test01();
			//new Charlotte.wb.t20190716.Test0001().Test02();
			//new Charlotte.wb.t20190720.Test0001().Test01();
			//new Charlotte.wb.t20190805.Test0001().Test01();
			new Charlotte.wb.t20190827.Test0001().Test01();
		}
	}
}
