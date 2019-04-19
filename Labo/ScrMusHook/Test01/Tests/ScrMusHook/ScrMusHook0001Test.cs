using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.ScrMusHook;

namespace Charlotte.Tests.ScrMusHook
{
	public class ScrMusHook0001Test // -- 0001
	{
		public void Test01()
		{
			string message = "ScrMusHook9999";

			if (new ScrMusHook0001().Echo(message) != message)
				throw null;

			Console.WriteLine("OK!");
		}
	}
}
