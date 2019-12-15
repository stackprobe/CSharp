using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

// ^ sync @ SerialComm_SerialPortTerminal

namespace Charlotte
{
	// sync > @ SerialComm_SerialPortTerminal

	public class SerialPortTerminal : IDisposable
	{
		private const int BAUD_RATE_1 = 9600;
		private const int BAUD_RATE_2 = 19200;
		private const int BAUD_RATE_3 = 38400;
		private const int BAUD_RATE_4 = 57600;
		private const int BAUD_RATE_5 = 115200;

		private SerialPort Port;

		public SerialPortTerminal(string[] args)
			: this(new Queue<string>(args))
		{ }

		public SerialPortTerminal(Queue<string> argq)
		{
			int baudRate = BAUD_RATE_1;
			int portNo = 1;

			while (1 <= argq.Count)
			{
				string arg = argq.Dequeue().ToUpper();

				if (arg == ";")
					break;

				if (arg == "/B")
				{
					switch (int.Parse(argq.Dequeue()))
					{
						case 1: baudRate = BAUD_RATE_1; break;
						case 2: baudRate = BAUD_RATE_2; break;
						case 3: baudRate = BAUD_RATE_3; break;
						case 4: baudRate = BAUD_RATE_4; break;
						case 5: baudRate = BAUD_RATE_5; break;

						default:
							throw new ArgumentException();
					}
					continue;
				}
				if (arg == "/C")
				{
					portNo = int.Parse(argq.Dequeue());

					if (portNo < 1 || 9 < portNo)
						throw new ArgumentException();

					continue;
				}
				throw new ArgumentException("不明な引数");
			}

			this.Port = new SerialPort();
			this.Port.BaudRate = baudRate;
			this.Port.Parity = Parity.None;
			this.Port.DataBits = 8;
			this.Port.StopBits = StopBits.One;
			this.Port.Handshake = Handshake.None;
			this.Port.PortName = "COM" + portNo;

			this.Port.ReadTimeout = 2000;
			this.Port.WriteTimeout = 2000;

			this.Port.Open();
		}

		public void Dispose()
		{
			if (this.Port != null)
			{
				this.Port.Dispose();
				this.Port = null;
			}
		}

		private const int RW_SIZE_MAX = 128 * 1024 * 1024;

		public void WriteLine(string line)
		{
			this.Write(Encoding.UTF8.GetBytes(line));
		}

		public void Write(byte[] data, int offset = 0)
		{
			this.Write(data, offset, data.Length - offset);
		}

		public void Write(byte[] data, int offset, int size)
		{
			if (data == null)
				throw new ArgumentException();

			if (offset < 0 || data.Length < offset)
				throw new ArgumentException();

			if (size < 0 || data.Length - offset < size)
				throw new ArgumentException();

			if (RW_SIZE_MAX < size)
				throw new ArgumentException();

			WriteUInt((uint)size);
			WriteBytes(data, 0, size);

			using (SHA512 sha512 = SHA512.Create())
			{
				WriteBytes(sha512.ComputeHash(data, 0, size), 0, 16);
			}
		}

		private void WriteUInt(uint value)
		{
			WriteBytes(
				new byte[]
				{
					(byte)((value >>  0) & 0xff),
					(byte)((value >>  8) & 0xff),
					(byte)((value >> 16) & 0xff),
					(byte)((value >> 24) & 0xff),
				},
				0,
				4
				);
		}

		private void WriteBytes(byte[] data, int offset, int size)
		{
			this.Port.Write(data, offset, size);
		}

		public string ReadLine()
		{
			return Encoding.UTF8.GetString(Read());
		}

		public byte[] Read()
		{
			int size = (int)ReadUInt();

			if (size < 0 || RW_SIZE_MAX < size)
				throw new Exception();

			byte[] data = ReadBytes(size);

			using (SHA512 sha512 = SHA512.Create())
			{
				byte[] hash1 = ReadBytes(16);
				byte[] hash2 = sha512.ComputeHash(data);

				if (hash1.SequenceEqual(hash2) == false)
					throw new Exception();
			}
			return data;
		}

		private uint ReadUInt()
		{
			byte[] data = ReadBytes(4);

			return
				((uint)data[0] << 0) |
				((uint)data[1] << 8) |
				((uint)data[2] << 16) |
				((uint)data[3] << 24);
		}

		private byte[] ReadBytes(int size)
		{
			byte[] data = new byte[size];

			for (int offset = 0; offset < size; )
			{
				int readSize = this.Port.Read(data, offset, size - offset);

				if (readSize < 0 || size - offset < readSize)
					throw new Exception();

				offset += readSize;
			}
			return data;
		}
	}

	// < sync
}
