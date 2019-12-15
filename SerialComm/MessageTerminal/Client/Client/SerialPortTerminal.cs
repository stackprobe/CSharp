using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;

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
	}

	// < sync
}
