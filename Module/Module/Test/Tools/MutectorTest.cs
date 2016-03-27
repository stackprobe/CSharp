using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Test.Tools
{
	public class MutectorTest
	{
		public static void Test01()
		{
			using (Mutector.Sender sender = new Mutector.Sender("キュア☆マジカル"))
			using (Mutector.Recver recver = new Mutector.Recver("キュア☆マジカル"))
			{
				bool[] death = new bool[1];

				recver.SetInterlude(delegate
				{
					return death[0] == false;
				});

				recver.SetRecved(delegate(byte[] message)
				{
					DebugTools.WriteLog(Encoding.UTF8.GetString(message));
				});

				Thread th = new Thread((ThreadStart)delegate
				{
					try
					{
						recver.Perform();
					}
					catch (Exception e)
					{
						DebugTools.WriteLog("recver_e: " + e);
					}
				});

				th.Start();
				try
				{
					Thread.Sleep(100); // recver の開始待ち。

					sender.Send(Encoding.UTF8.GetBytes("#"));
					sender.Send(Encoding.UTF8.GetBytes("123"));
					sender.Send(Encoding.UTF8.GetBytes("ABCDEF"));
					sender.Send(Encoding.UTF8.GetBytes("リズ先生の秘密が茂る宝島(ワンダーランド)を、僕のリンクルステッキで探検&冒険したい！"));
				}
				finally
				{
					death[0] = true;
					th.Join();
				}
			}
		}
	}
}
