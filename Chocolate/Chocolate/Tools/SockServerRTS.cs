using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Net;
using System.Net.Sockets;
using Charlotte.Tools;

namespace Charlotte.Tools
{
	public class SockServerRTS<T>
	{
		public int PortNo = 59999;
		public int Backlog = 100;
		public int ChannelMax = 30;
		public Func<T> Connected = () => default(T);
		public Func<T, byte[], bool> Recv = (session, recvData) => true;
		public Func<T, bool> Transaction = session => true;
		public Func<T, byte[]> Send = session => null;
		public Action<T> Disconnected = session => { };

		// <---- prm

		private Thread Th = null;
		private List<SockChannelRTS<T>> Channels = new List<SockChannelRTS<T>>();
		private byte[] Buff = new byte[1024 * 1024 * 8];

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

						int serviceWaitMillis = 0;

						while (this.StopFlag == false)
						{
							try
							{
								if (this.Channels.Count < this.ChannelMax)
								{
									Socket handler = this.Connect(listener);

									if (handler != null)
									{
										handler.Blocking = false;

										this.Channels.Add(new SockChannelRTS<T>()
										{
											Phase = SockChannelRTS_Consts.Phase_e.Recv,
											Handler = handler,
											Session = this.Connected(),
										});

										serviceWaitMillis = 0;
									}
								}
								for (int index = 0; index < this.Channels.Count; index++)
								{
									SockChannelRTS<T> channel = this.Channels[index];

									if (channel.Phase == SockChannelRTS_Consts.Phase_e.Recv)
									{
										try
										{
											int recvSize = channel.Handler.Receive(this.Buff, 0, this.Buff.Length, SocketFlags.None);

											if (recvSize <= 0)
												throw new Exception("受信エラー(切断)");

											byte[] recvData = new byte[recvSize];

											Array.Copy(this.Buff, 0, recvData, 0, recvSize);

											if (this.Recv(channel.Session, recvData)) // ? 受信完了 -> 処理へ
											{
												channel.Phase = SockChannelRTS_Consts.Phase_e.Transaction;
												serviceWaitMillis = 0;
											}
										}
										catch (SocketException e)
										{
											if (e.ErrorCode == 10035) // ? 受信タイムアウト
											{
												// noop
											}
											else
											{
												ProcMain.WriteLog(e);

												channel.Phase = SockChannelRTS_Consts.Phase_e.Disconnect;
												serviceWaitMillis = 0;
											}
										}
										catch (Exception e)
										{
											ProcMain.WriteLog(e);

											channel.Phase = SockChannelRTS_Consts.Phase_e.Disconnect;
											serviceWaitMillis = 0;
										}
									}
									else if (channel.Phase == SockChannelRTS_Consts.Phase_e.Transaction)
									{
										try
										{
											if (this.Transaction(channel.Session)) // ? 処理完了 -> 送信へ
											{
												channel.Phase = SockChannelRTS_Consts.Phase_e.Transaction;
												serviceWaitMillis = 0;
											}
										}
										catch (Exception e)
										{
											ProcMain.WriteLog(e);

											channel.Phase = SockChannelRTS_Consts.Phase_e.Disconnect;
											serviceWaitMillis = 0;
										}
									}
									else if (channel.Phase == SockChannelRTS_Consts.Phase_e.Send)
									{
										try
										{
											if (channel.SendData == null)
											{
												channel.SendData = this.Send(channel.Session);

												if (channel.SendData == null) // ? 送信完了 -> 切断へ
													throw new Exception("送信完了");

												channel.SendOffset = 0;
											}
											int sentSize = channel.Handler.Send(
												channel.SendData,
												channel.SendOffset,
												channel.SendData.Length - channel.SendOffset,
												SocketFlags.None
												);

											if (sentSize <= 0)
												throw new Exception("送信エラー(切断)");

											channel.SendOffset += sentSize;

											if (channel.SendData.Length <= channel.SendOffset)
												channel.SendData = null;

											serviceWaitMillis = 0;
										}
										catch (SocketException e)
										{
											if (e.ErrorCode == 10035) // ? 送信タイムアウト
											{
												// noop
											}
											else
											{
												ProcMain.WriteLog(e);

												channel.Phase = SockChannelRTS_Consts.Phase_e.Disconnect;
												serviceWaitMillis = 0;
											}
										}
										catch (Exception e)
										{
											ProcMain.WriteLog(e);

											channel.Phase = SockChannelRTS_Consts.Phase_e.Disconnect;
											serviceWaitMillis = 0;
										}
									}
									else if (channel.Phase == SockChannelRTS_Consts.Phase_e.Disconnect)
									{
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

										try
										{
											this.Disconnected(channel.Session);
										}
										catch (Exception e)
										{
											ProcMain.WriteLog(e);
										}

										this.Channels.RemoveAt(index--);
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

							Thread.Sleep(serviceWaitMillis);

							if (serviceWaitMillis < 100)
								serviceWaitMillis++;
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
