using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class FilingCase3Test
	{
		public void Test01()
		{
			using (FilingCase3 client = new FilingCase3())
			{
				client.Post("R-001", Encoding.ASCII.GetBytes("SIO"));
				client.Post("R-002", Encoding.ASCII.GetBytes("MISO"));
				client.Post("R-003", Encoding.ASCII.GetBytes("SYOYU"));

				if (Encoding.ASCII.GetString(client.GetPost("R-001", Encoding.ASCII.GetBytes("_syoyu"))) != "SIO")
					throw null;

				if (Encoding.ASCII.GetString(client.GetPost("R-002", Encoding.ASCII.GetBytes("_miso"))) != "MISO")
					throw null;

				if (Encoding.ASCII.GetString(client.GetPost("R-003", Encoding.ASCII.GetBytes("_sio"))) != "SYOYU")
					throw null;

				if (Encoding.ASCII.GetString(client.Get("R-001")) != "_syoyu")
					throw null;

				if (Encoding.ASCII.GetString(client.Get("R-002")) != "_miso")
					throw null;

				if (Encoding.ASCII.GetString(client.Get("R-003")) != "_sio")
					throw null;
			}
		}
	}
}
