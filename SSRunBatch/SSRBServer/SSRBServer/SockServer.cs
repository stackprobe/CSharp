using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using System.Net;

namespace Charlotte
{
	public class SockServer
	{
		public delegate void Transmit_d(Connection connection);

		private int PortNo;
		private Transmit_d Transmit;
		private int RSTimeoutMillis;
		private Thread PerformTh;
		private bool StopFlag = false;
		private Exception LastEx = null;

		public SockServer(int portNo, Transmit_d transmit, int recvSendTimeoutMillis = 2000)
		{
			this.PortNo = portNo;
			this.Transmit = transmit;
			this.RSTimeoutMillis = recvSendTimeoutMillis;

			this.PerformTh = new Thread((ThreadStart)delegate
			{
				try
				{
					this.Perform();
				}
				catch (Exception e)
				{
					this.LastEx = e;

					Logger.WriteLine(e); // app 固有
				}
			});

			this.PerformTh.Start();
		}

		public void Stop()
		{
			this.StopFlag = true;
		}

		public bool IsRunning()
		{
			if (this.PerformTh != null && this.PerformTh.Join(0))
				this.PerformTh = null;

			return this.PerformTh != null;
		}

		public void Stop_B()
		{
			this.Stop();

			// 終了待ち
			{
				int millis = 0;

				while (this.IsRunning())
				{
					if (millis < 2000)
						millis++;

					Thread.Sleep(millis);
				}
			}
		}

		public Exception GetLastException()
		{
			if (this.IsRunning()) // forbidden
				throw null;

			return this.LastEx;
		}

		private void Perform()
		{
			using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				IPEndPoint endPoint = new IPEndPoint(0L, this.PortNo);

				listener.Bind(endPoint);
				listener.Listen(30);
				listener.Blocking = false;

				int connectWaitMillis = 0;

				while (this.StopFlag == false)
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

						handler.Blocking = false;

						try
						{
							Logger.WriteLine("Start"); // app 固有

							this.Transmit(new Connection(handler, this.RSTimeoutMillis));

							Logger.WriteLine("OK!"); // app 固有
						}
						catch (Exception e)
						{
							this.LastEx = e;

							Logger.WriteLine(e); // app 固有
						}

						try
						{
							handler.Shutdown(SocketShutdown.Both);
						}
						catch
						{ }

						try
						{
							handler.Close();
						}
						catch
						{ }
					}
				}
			}
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

		public class Connection
		{
			private Socket Handler;
			private int RSTimeoutMillis;

			public Connection(Socket handler, int recvSendTimeoutMillis)
			{
				this.Handler = handler;
				this.RSTimeoutMillis = recvSendTimeoutMillis;
			}

			public byte[] Recv(int size)
			{
				byte[] data = new byte[size];

				this.Recv(data);

				return data;
			}

			public void Recv(byte[] data, int offset = 0)
			{
				this.Recv(data, offset, data.Length - offset);
			}

			public void Recv(byte[] data, int offset, int size)
			{
				while (1 <= size)
				{
					int recvSize = this.TryRecv(data, offset, size);

					size -= recvSize;
					offset += recvSize;
				}
			}

			public int TryRecv(byte[] data, int offset, int size)
			{
				int millis = 0;
				int millisElapsed = 0;

				for (; ; )
				{
					int recvSize;

					try
					{
						recvSize = this.Handler.Receive(data, offset, size, SocketFlags.None);
					}
					catch (SocketException e)
					{
						if (e.ErrorCode != 10035)
						{
							throw new Exception("受信エラー", e);
						}
						recvSize = 0;
					}
					if (1 <= recvSize)
					{
						return recvSize;
					}
					if (this.RSTimeoutMillis <= millisElapsed)
					{
						throw new Exception("受信タイムアウト");
					}
					Thread.Sleep(millis);

					millisElapsed += millis;

					if (millis < 100)
						millis++;
				}
			}

			public void Send(byte[] data, int offset = 0)
			{
				this.Send(data, offset, data.Length - offset);
			}

			public void Send(byte[] data, int offset, int size)
			{
				while (1 <= size)
				{
					int sentSize = this.TrySend(data, offset, size);

					size -= sentSize;
					offset += sentSize;
				}
			}

			private int TrySend(byte[] data, int offset, int size)
			{
				int millis = 0;
				int millisElapsed = 0;

				for (; ; )
				{
					int sentSize;

					try
					{
						sentSize = this.Handler.Send(data, offset, size, SocketFlags.None);
					}
					catch (SocketException e)
					{
						if (e.ErrorCode != 10035)
						{
							throw new Exception("送信エラー", e);
						}
						sentSize = 0;
					}
					if (1 <= sentSize)
					{
						return sentSize;
					}
					if (this.RSTimeoutMillis <= millisElapsed)
					{
						throw new Exception("送信タイムアウト");
					}
					Thread.Sleep(millis);

					millisElapsed += millis;

					if (millis < 100)
						millis++;
				}
			}
		}
	}
}
