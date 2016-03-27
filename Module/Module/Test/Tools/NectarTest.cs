using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Threading;

namespace Charlotte.Test.Tools
{
	public class NectarTest
	{
		public static void Test01()
		{
			using (Nectar.Sender sender = new Nectar.Sender("キュア☆ミラクル"))
			using (Nectar.Recver recver = new Nectar.Recver("キュア☆ミラクル"))
			{
				bool[] death = new bool[1];

				Thread th = new Thread((ThreadStart)delegate
				{
					try
					{
						while (death[0] == false)
						{
							byte[] message = recver.Receipt();

							if (message != null)
							{
								DebugTools.WriteLog(Encoding.UTF8.GetString(message));
							}
						}
					}
					catch (Exception e)
					{
						DebugTools.WriteLog("recver_e: " + e);
					}
				});

				th.Start();
				try
				{
					sender.Send(Encoding.UTF8.GetBytes("#"));
					sender.Send(Encoding.UTF8.GetBytes("123"));
					sender.Send(Encoding.UTF8.GetBytes("ABCDEF"));
					sender.Send(Encoding.UTF8.GetBytes("朝比奈みらいちゃんの秘密が茂る宝島(ワンダーランド)を、僕のリンクルステッキで探検&冒険したい！"));

					// この時点で recver の受信が完了してるとは限らない。

					Thread.Sleep(100); // 受信待ち。-- th の recver.Receipt() で２秒待たされる。
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
