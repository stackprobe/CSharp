using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;

namespace Charlotte.Tools
{
	public class SockServer
	{
		public int PortNo = 59999;
		public int Backlog = 100;
		public Action<SockChannel> Connected = (channel) => { };

		// <---- prm

		private Thread Th = null;

		public void Start()
		{
			Th = new Thread(() =>
			{
				try
				{
					using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
					{
						IPEndPoint endPoint = new IPEndPoint(0L, this.PortNo);

						listener.Bind(endPoint);
						listener.Listen(this.Backlog);
						listener.Blocking = false;

						int connectWaitMillis = 0;

						while (this.StopFlag == false)
						{
							try
							{
								Socket handler = this.Connect(listener);

								if (handler == null)
								{
									if (connectWaitMillis < 100)
										connectWaitMillis++;

									Thread.Sleep(connectWaitMillis);
								}
								else
								{
									connectWaitMillis = 0;

									try
									{
										SockChannel channel = new SockChannel();

										channel.Handler = handler;
										channel.PostSetHandler();

										this.Connected(channel);
									}
									catch (Exception e)
									{
										ProcMain.WriteLog(e);
									}

									try
									{
										handler.Shutdown(SocketShutdown.Both);
									}
									catch (Exception e)
									{
										ProcMain.WriteLog(e);
									}

									try
									{
										handler.Close();
									}
									catch (Exception e)
									{
										ProcMain.WriteLog(e);
									}
								}
							}
							catch (Exception e)
							{
								ProcMain.WriteLog(e);

								ProcMain.WriteLog("5秒間待機します。"); // ここへの到達は想定外 | ノーウェイトでループしないように。
								Thread.Sleep(5000);
								ProcMain.WriteLog("5秒間待機しました。");
							}

							GC.Collect();
						}
					}
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}
			});

			Th.Start();
		}

		private Socket Connect(Socket listener)
		{
			try
			{
				return listener.Accept();
			}
			catch (SocketException e)
			{
				if (e.ErrorCode != 10035)
				{
					throw new Exception("接続エラー", e);
				}
				return null;
			}
		}

		public bool IsRunning(int millis = 0)
		{
			if (Th != null && Th.Join(millis))
				Th = null;

			return Th != null;
		}

		private bool StopFlag = false;

		public void Stop()
		{
			StopFlag = true;
		}

		public void Stop_B()
		{
			Stop();

			// 終了待ち
			{
				int millis = 0;

				while (this.IsRunning(millis))
					if (millis < 2000)
						millis++;
			}
		}
	}
}
