using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.Modules.Tests
{
	public class AsyncFileWriterTest
	{
		public void Test01()
		{
			using (AsyncFileWriter writer = new AsyncFileWriter())
			{
				for (int i = 0; i < 10; i++)
				{
					writer.OpenFile(@"C:\temp\AsyncFileWriterTest_" + i + ".txt");

					for (int c = 0x30; c <= 0x39; c++)
					{
						writer.Write(new byte[] { (byte)c });
					}
				}
			}
		}
	}
}
