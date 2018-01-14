using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests
{
	public class Test01
	{
		public void Main01()
		{
			foreach (char chr in StringTools.HALF)
			{
				Console.Write("[" + ((int)chr).ToString("x2") + "]");
			}
			Console.WriteLine("");

			foreach (char chr in "\t\r\n ")
			{
				Console.Write("[" + ((int)chr).ToString("x2") + "]");
			}
			Console.WriteLine("");

			{
				char c = (char)0;

				Console.WriteLine("" + (int)c);
				c--;
				Console.WriteLine("" + (int)c);
			}
		}
	}
}
