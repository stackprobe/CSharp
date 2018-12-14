using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class XmlNodeTest
	{
		public void Test01()
		{
			XmlNode root = XmlNode.LoadFile(@"C:\var\test.xml");

			Console.WriteLine(root.Ref("Header/reservation/m").Value);
			Console.WriteLine(root.Ref("Header/reservation/m____xxxx").Value);
		}
	}
}
