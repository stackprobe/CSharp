using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Satellite.Tools;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new Program().Main2();
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
			Console.WriteLine("\\e");
			Console.ReadLine();
		}

		private void Main2()
		{
			//Test01();
			Test02();
			//Test03();
		}

		private void Test01()
		{
			Console.WriteLine("ABC".Substring(1, 2));
			Console.WriteLine("ABC".Substring(2, 1));
			Console.WriteLine("ABC".Substring(3, 0));
			//Console.WriteLine("ABC".Substring(3, 1)); // error
			Console.WriteLine("ABC".Substring(1));
			Console.WriteLine("ABC".Substring(2));
			Console.WriteLine("ABC".Substring(3));

			Console.WriteLine(StringTools.Format("AA$BB$CC", "ab", "bc"));

			foreach (string path in FileTools.List("C:/tmp"))
				Console.WriteLine("path: " + path);

			//using (new MutexObject("aaa").Section()) // leak
			//{ }

			using (MutexObject mo = new MutexObject("aaa"))
			using (mo.Section())
			{ }

			//new MutexObject("aaa").Release(); // error

			Console.WriteLine("" + (1 >> 1));
			Console.WriteLine("" + (0 >> 1));
			Console.WriteLine("" + (-1 >> 1));
			Console.WriteLine("" + (0x7fffffff >> 1));

			Console.WriteLine("" + SecurityTools.GetSHA512_128String("ABC"));
		}

		private void Test02()
		{
			new Test02().TestMain();
		}

		private void Test03()
		{
			new Test03().ServerTest();
		}
	}
}
