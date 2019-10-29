using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing.Imaging;

namespace Charlotte.Tests.Tools
{
	public class Canvas2Test
	{
		public void Test01()
		{
			new Canvas2(@"C:\var\PaceMap.bmp").Save(@"C:\temp\PaceMap.jpg", ImageFormat.Jpeg, 100);
		}
	}
}
