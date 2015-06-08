using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ObjectList = Charlotte.Satellite.ObjectList;
using ObjectMap = Charlotte.Satellite.ObjectMap;
using Satellizer = Charlotte.Satellite.Satellizer;

namespace Charlotte
{
	public class Test03
	{
		public void ServerTest()
		{
			Satellizer.Listen("Test03", "ServerSide", 2000, new TestListener());
		}

		private class TestListener : Satellizer.Server
		{
			private bool DeadFlag;

			public bool Interlude()
			{
				return this.DeadFlag == false;
			}

			public void ServiceTh(Satellizer stllzr)
			{
				Console.WriteLine("ServiceTh_ST");

				while (stllzr.IsOtherSideDisconnected() == false)
				{
					Console.WriteLine("recv_Go");

					object recvObj = stllzr.Recv(2000);

					Console.WriteLine("recvObj: " + recvObj);

					if (recvObj == null)
						continue;

					ObjectMap om = (ObjectMap)recvObj;

					Console.WriteLine("COMMAND: " + om["COMMAND"]);

					if ((string)om["COMMAND"] == "CLOSE")
						break;

					if ((string)om["COMMAND"] == "DEAD")
						this.DeadFlag = true;

					if ((string)om["COMMAND"] == "ECHO")
					{
						string message = (string)om["MESSAGE"];

						ObjectList ol = new ObjectList();

						ol.Add(message);
						ol.Add(message.ToUpper());
						ol.Add(message.ToLower());

						Console.WriteLine("ECHO_SEND: " + ol);

						stllzr.Send(ol);
					}
				}
				Console.WriteLine("ServiceTh_ED");
			}
		}
	}
}
