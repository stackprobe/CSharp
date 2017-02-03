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

		static void Main(string[] args)
		{
			try
			{
				Main2();
				Console.WriteLine("OK!");
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("Press ENTER to exit.");
			Console.ReadLine();
		}

		private static void Main2()
		{
			new Test01().Main01();
			//new FileToolsTest().Test01();
		}
	}
}
