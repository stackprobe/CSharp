using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class EnumeratorCartridgeTest
	{
		public void Test01()
		{
			EnumeratorCartridge<string> ec = new EnumeratorCartridge<string>("A:B:C".Split(':'));

			foreach (string element in ec.Iterate())
			{
				Console.WriteLine(element);
			}
		}
	}
}
