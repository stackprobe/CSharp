using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace ConsoleInputTest
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
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
		}

		private void Main2()
		{
			for (; ; )
			{
				ConsoleKeyInfo cki = Console.ReadKey(true);

				object oCki = DebugTools.toListOrMap(cki, 1);
				//object oCki = DebugTools.toListOrMap(cki, 2);
				//object oCki = DebugTools.toListOrMap(cki);

				Console.WriteLine(JsonTools.encode(oCki));
			}
		}
	}
}
