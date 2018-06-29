using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class BatchClientTest
	{
		public void Test01()
		{
			// localhost で SSRBServer を立てる。

			FileTools.Delete(@"C:\temp\a");
			FileTools.CreateDir(@"C:\temp\a");
			FileTools.CreateDir(@"C:\temp\a\b");

			File.WriteAllText(@"C:\temp\a\a0001.txt", "aaaaaa", Encoding.ASCII);
			File.WriteAllText(@"C:\temp\a\a0002.txt", "aaabbb", Encoding.ASCII);
			File.WriteAllText(@"C:\temp\a\a0003.txt", "bbbbbb", Encoding.ASCII);

			new BatchClient()
			{
				SendFiles = new string[]
				{
					@"C:\temp\a\a0001.txt",
					@"C:\temp\a\a0002.txt",
					@"C:\temp\a\a0003.txt",
				},
				RecvFiles = new string[]
				{
					@"C:\temp\a\b\a0003.txt",
					@"C:\temp\a\b\b0001.txt",
					@"C:\temp\a\b\b0002.txt",
					@"C:\temp\a\b\out.txt",
				},
				Commands = new string[]
				{
					"DIR > out.txt",
					"COPY a0001.txt b0001.txt >> out.txt",
					"COPY a0002.txt b0002.txt >> out.txt",
					"DIR >> out.txt",
				},
			}
			.Perform();
		}
	}
}
