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
		public const string APP_IDENT = "{8642103e-0052-4aef-b5c1-93b061dfe8b6}";
		public const string APP_TITLE = "t0001";

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
			Test01();
		}

		private void Test01()
		{
			Test01_a("/A /MS");
			Test01_a("/A /Ch");
			Test01_a("/L /MS");
			Test01_a("/L /Ch");
		}

		private const string ToArrayListTestExe = @"..\..\..\..\ToArrayListTest\ToArrayListTest\bin\Release\ToArrayListTest.exe";

		private void Test01_a(string prm)
		{
			for (int count = 100000000; ; count += count / 2) // *= 1.5
			{
				using (WorkingDir wd = new WorkingDir())
				{
					string successfulFile = wd.GetPath("successful.flg");
					string errorFile = wd.GetPath("error.log");

					ProcessTools.Batch(new string[]
					{
						string.Format("{0} \"{1}\" \"{2}\" {3} {4}", ToArrayListTestExe, successfulFile, errorFile, count, prm),
					},
					ProcMain.SelfDir
					);

					Console.Write(prm + " count=" + count + " --> ");

					if (File.Exists(successfulFile))
					{
						Console.WriteLine("OK");
					}
					else
					{
						Console.WriteLine("NG");
						Console.WriteLine(File.ReadAllText(errorFile, Encoding.UTF8));
						break;
					}
				}
			}
		}
	}
}
