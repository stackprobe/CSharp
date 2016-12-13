using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Test.Tools
{
	class Nectar2Test
	{
		public static void Test01()
		{
			using (Nectar2.Sender sender = new Nectar2.Sender("CURE-MIRACLE"))
			using (Nectar2.Recver recver = new Nectar2.Recver("CURE-MIRACLE", Encoding.ASCII.GetBytes("\n")[0]))
			{
				sender.Send(Encoding.ASCII.GetBytes("[CURE UP]\n[RA PA PA!]\n"));

				DebugTools.WriteLog("1: " + Encoding.ASCII.GetString(RecvWait(recver)));
				DebugTools.WriteLog("2: " + Encoding.ASCII.GetString(RecvWait(recver)));
			}
		}

		private static byte[] RecvWait(Nectar2.Recver recver)
		{
			for (int c = 0; c < 100; c++)
			{
				byte[] message = recver.Recv();

				if (message != null)
					return message;

				DebugTools.WriteLog("待つンゴ: " + c);
				Thread.Sleep(c);
			}
			throw new Exception("時間掛かり過ぎンゴ");
		}
	}
}
