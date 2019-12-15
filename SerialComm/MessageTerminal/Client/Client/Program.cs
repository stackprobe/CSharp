using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace Charlotte
{
	class Program
	{
		private static SerialPortTerminal SerialPortTerminal;

		static void Main(string[] args)
		{
			SerialPortTerminal = new SerialPortTerminal(args);

			// TODO
		}
	}
}
