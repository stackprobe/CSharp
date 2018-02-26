using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Gnd
	{
		public static Gnd I;

		public List<string> SendFiles = new List<string>();
		public List<string> RecvFiles = new List<string>();
		public List<string> Commands = new List<string>();
		public string OutLinesFile = null;
	}
}
