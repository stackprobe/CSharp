using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Tests.Tools
{
	public class FilingCase3Test
	{
		public void Test01()
		{
			using (FilingCase3 client = new FilingCase3())
			{
				client.Delete("test-root");

				Console.WriteLine("*1");
				foreach (string file in client.List("test-root"))
				{
					Console.WriteLine(file);
				}
				Console.WriteLine("*2");
				client.Post(@"test-root\test-file", Encoding.ASCII.GetBytes("test-data"));

				Console.WriteLine("*3");
				foreach (string file in client.List("test-root"))
				{
					Console.WriteLine(file);
				}
				Console.WriteLine("*4");
				Console.WriteLine(Encoding.ASCII.GetString(client.Get(@"test-root\test-file")));
			}
		}

		public void Test02()
		{
			using (FilingCase3 client = new FilingCase3())
			{
				client.Delete("test-root");

				Thread.Sleep(30000);

				client.Delete("test-root");
			}
		}
	}
}
