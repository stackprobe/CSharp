using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace IntageTS
{
	public class SSRBClient
	{
		private string Domain;
		private int PortNo;

		public SSRBClient(string domain, int portNo)
		{
			this.Domain = domain;
			this.PortNo = portNo;
		}

		private List<string> SendFiles = new List<string>();
		private List<string> RecvFiles = new List<string>();
		private string[] Commands;
		private string[] OutLines;

		public void AddSendFile(string file)
		{
			file = Path.GetFullPath(file);

			if (File.Exists(file) == false)
				throw new Exception("送信ファイル\n" + file + "\nが存在しません。");

			this.SendFiles.Add(file);
		}

		public void AddRecvFile(string file)
		{
			file = Path.GetFullPath(file);

			this.RecvFiles.Add(file);
		}

		public void SetCommands(string[] commands)
		{
			this.Commands = commands;
		}

		public string[] GetOutLines()
		{
			return this.OutLines;
		}

		public string GetOutText()
		{
			return string.Join("\r\n", this.OutLines);
		}

		private Connection Conn = null;

		public void Perform()
		{
			IPHostEntry hostEntry = Dns.GetHostEntry(this.Domain);
			IPEndPoint endPoint = new IPEndPoint(hostEntry.AddressList[0], this.PortNo);

			using (Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp))
			{
				socket.Connect(endPoint);
				socket.Blocking = false;

				this.Conn = new Connection(socket, 2000);

				this.Conn.Send(Encoding.ASCII.GetBytes("SSRB/0.0"));

				this.SendUInt((uint)this.SendFiles.Count);

				foreach (string file in this.SendFiles)
				{
					this.SendLine(Path.GetFileName(file));
					this.SendData(File.ReadAllBytes(file));
				}
				this.SendUInt((uint)this.RecvFiles.Count);

				foreach (string file in this.RecvFiles)
				{
					this.SendLine(Path.GetFileName(file));
				}
				this.SendUInt((uint)this.Commands.Length);

				foreach (string command in this.Commands)
				{
					this.SendLine(command);
				}
				this.RecvUInt(); // Skip

				foreach (string file in this.RecvFiles)
				{
					this.RecvLine(); // Skip

					File.WriteAllBytes(file, this.RecvData());
				}
				this.OutLines = new string[(int)this.RecvUInt()];

				for (int index = 0; index < this.OutLines.Length; index++)
				{
					this.OutLines[index] = this.RecvLine();
				}

				this.Conn = null;

				socket.Disconnect(false);
			}
		}

		private void SendLine(string line)
		{
			this.SendData(Consts.ENCODING_SJIS.GetBytes(line));
		}

		private void SendData(byte[] data)
		{
			this.SendUInt((uint)data.Length);
			this.Conn.Send(data);
		}

		private void SendUInt(uint value)
		{
			this.Conn.Send(new byte[]
			{
				(byte)(value >> 24),
				(byte)(value >> 16),
				(byte)(value >> 8),
				(byte)(value >> 0),
			});
		}

		private string RecvLine()
		{
			return Consts.ENCODING_SJIS.GetString(this.RecvData());
		}

		private byte[] RecvData()
		{
			return this.Conn.Recv((int)this.RecvUInt());
		}

		private uint RecvUInt()
		{
			byte[] data = this.Conn.Recv(4);

			return
				((uint)data[0] << 24) |
				((uint)data[1] << 16) |
				((uint)data[2] << 8) |
				((uint)data[3] << 0);
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
