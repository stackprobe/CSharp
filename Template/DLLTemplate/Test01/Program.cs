using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test01.Tests.DDDDTMPL;

namespace Test01
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
			new DDDDTMPL0001Test().Test01(); // -- 0001
		}
	}
}
