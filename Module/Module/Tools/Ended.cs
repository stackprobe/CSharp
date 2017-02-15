using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class Ended : Cancelled
	{
		public static readonly Ended i = new Ended();
	}
}
