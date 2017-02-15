using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class Cancelled : FaultOperation
	{
		public static readonly Cancelled i = new Cancelled();
	}
}
