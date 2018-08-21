using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Windows.Forms;
using Charlotte.Tests.Tools;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			//new WorkingDirTest().Test01();
			//new HandleSectionTest().Test01();
			new HandleSectionToolsTest().Test01();

#if DEBUG
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
#endif
		}
	}
}
