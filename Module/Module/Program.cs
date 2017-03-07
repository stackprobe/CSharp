using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Charlotte.Tools;
using Charlotte.Test.Tools;
using Charlotte.Test;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{b85575ee-4b6f-4501-874f-7aeff90c4019}";
		public const string APP_TITLE = "Module";

		static void Main(string[] args)
		{
			try
			{
				main2();
				Console.WriteLine("OK!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private static void main2()
		{
			new Test01().main01();
			//new FileToolsTest().test01();
			//new FileToolsTest().test02();
			//new DateToDayTest().test01();
			//new DateTimeToSecTest().test01();
			//new FaultOperationTest().test01();
			//new CsvFileTest().test01();
		}
	}
}
