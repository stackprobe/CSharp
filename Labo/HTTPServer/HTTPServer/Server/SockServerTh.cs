using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Charlotte.Tools;

namespace Charlotte.Server
{
	public class SockServerTh
	{
		public int PortNo = 59999;
		public int Backlog = 100;
		public Action<SockChannel> Connected = channel => { };

		// <---- prm

		private Thread Th = null;
		private ThreadEx[] ConnectThs = new ThreadEx[30];
		private int ConnectThCount = 0;

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
								Socket handler = this.ConnectThCount < this.ConnectThs.Length ? this.Connect(listener) : null;

								if (handler == null)
								{
									if (connectWaitMillis < 100)
										connectWaitMillis++;

									Thread.Sleep(connectWaitMillis);
								}
								else
								{
									connectWaitMillis = 0;

									{
										SockChannel channel = new SockChannel();

										channel.Handler = handler;
										handler = null;
										channel.PostSetHandler();

										this.ConnectThs[this.ConnectThCount++] = new ThreadEx(() =>
										{
											try
											{
												this.Connected(channel);
											}
											catch (Exception e)
											{
												ProcMain.WriteLog(e);
											}

											try
											{
												channel.Handler.Shutdown(SocketShutdown.Both);
											}
											catch (Exception e)
											{
												ProcMain.WriteLog(e);
											}

											try
											{
												channel.Handler.Close();
											}
											catch (Exception e)
											{
												ProcMain.WriteLog(e);
											}
										});
									}
								}

								for (int index = 0; index < this.ConnectThCount; index++)
								{
									if (this.ConnectThs[index].IsEnded())
									{
										this.ConnectThs[index--] = this.ConnectThs[--this.ConnectThCount];
										this.ConnectThs[this.ConnectThCount] = null;
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

					while (1 <= this.ConnectThCount)
					{
						this.ConnectThs[--this.ConnectThCount].WaitToEnd();
						this.ConnectThs[this.ConnectThCount] = null;
					}
				}
				catch (Exception e)
				{
					ProcMain.WriteLog(e);
				}
			});

			Th.Start();
		}

		private Socket Connect(Socket listener) // ret: null == 接続タイムアウト
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

		public void Stop_B_ChannelSafe()
		{
			Stop();

			while (this.IsRunning(2000))
			{ }
		}

		public void Stop_B()
		{
			SockChannel.StopFlag = true;
			Stop_B_ChannelSafe();
			SockChannel.StopFlag = false;
		}
	}
}
