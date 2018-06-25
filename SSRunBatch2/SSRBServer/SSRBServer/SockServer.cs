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

		private readonly object SYNCROOT = new object();
		private int PortNo;
		private Transmit_d Transmit;
		private int RSTimeoutMillis;
		private Thread PerformTh;
		private bool StopFlag = false;
		private Queue<Exception> Exceptions = new Queue<Exception>();

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
					this.AddException(e);
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

		public Exception GetException()
		{
			lock (this.SYNCROOT)
			{
				if (this.Exceptions.Count == 0)
				{
					return null;
				}
				return this.Exceptions.Dequeue();
			}
		}

		private void AddException(Exception e)
		{
			lock (this.SYNCROOT)
			{
				while (30 < this.Exceptions.Count)
				{
					this.Exceptions.Dequeue();
				}
				this.Exceptions.Enqueue(e);
			}
		}

		public static int Backlog = 30;

		private void Perform()
		{
			Utils.PostMessage("PortNo: " + this.PortNo + ", Backlog: " + Backlog);

			using (Socket listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				IPEndPoint endPoint = new IPEndPoint(0L, this.PortNo);

				listener.Bind(endPoint);
				listener.Listen(Backlog);
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

						try
						{
							this.Transmit(new Connection(handler));
						}
						catch (Exception e)
						{
							this.AddException(e);
						}

						try
						{
							handler.Shutdown(SocketShutdown.Both);
						}
						catch (Exception e)
						{
							this.AddException(e);
						}

						try
						{
							handler.Close();
						}
						catch (Exception e)
						{
							this.AddException(e);
						}
					}
					GC.Collect();
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
			public int RSTimeoutMillis = 180000; // 3 min

			private Socket Handler;

			public Connection(Socket handler)
			{
				handler.Blocking = false;

				this.Handler = handler;
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
				int elapsedMillis = 0;

				for (; ; )
				{
					try
					{
						int recvSize = this.Handler.Receive(data, offset, size, SocketFlags.None);

						if (recvSize <= 0)
						{
							throw new Exception("受信エラー(切断)");
						}
						return recvSize;
					}
					catch (SocketException e)
					{
						if (e.ErrorCode != 10035)
						{
							throw new Exception("受信エラー", e);
						}
					}
					if (this.RSTimeoutMillis <= elapsedMillis)
					{
						throw new Exception("受信タイムアウト");
					}
					Thread.Sleep(millis);

					elapsedMillis += millis;

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
					int sentSize = this.TrySend(data, offset, Math.Min(4 * 1024 * 1024, size));

					size -= sentSize;
					offset += sentSize;
				}
			}

			private int TrySend(byte[] data, int offset, int size)
			{
				int millis = 0;
				int elapsedMillis = 0;

				for (; ; )
				{
					try
					{
						int sentSize = this.Handler.Send(data, offset, size, SocketFlags.None);

						if (sentSize <= 0)
						{
							throw new Exception("送信エラー(切断)");
						}
						return sentSize;
					}
					catch (SocketException e)
					{
						if (e.ErrorCode != 10035)
						{
							throw new Exception("送信エラー", e);
						}
					}
					if (this.RSTimeoutMillis <= elapsedMillis)
					{
						throw new Exception("送信タイムアウト");
					}
					Thread.Sleep(millis);

					elapsedMillis += millis;

					if (millis < 100)
						millis++;
				}
			}
		}
	}
}
