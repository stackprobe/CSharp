﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.DDDDTMNS;

namespace Charlotte.Tests.DDDDTMNS
{
	public class DDDDTMPL0001Test // -- 0001
	{
		public void Test01()
		{
			string message = "DDDDTMPL9999";

			if (new DDDDTMPL0001().Echo(message) != message)
				throw null;

			Console.WriteLine("OK!");
		}
	}
}
