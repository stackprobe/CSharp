﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tests.DDDDTMPL;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{a00374e8-1bfb-415e-96d6-b764926d897f}";
		public const string APP_TITLE = "DDDDTMPL";

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