using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class RelayException : Exception
	{
		public RelayException(Exception e)
			: base("Relay", e)
		{ }
	}
}
