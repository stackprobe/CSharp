using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte.Tools
{
	public class Logger
	{
		private const string IDENT = "{3e4cd185-a754-4784-ae22-009dea5556a6}"; // shared_uuid

		public static void WriteLine(string line)
		{
			Write(line + "\n");
		}

		public static void Write(string str)
		{
			Send(StringTools.ENCODING_SJIS.GetBytes(str));
		}

#if DEBUG
		private static object SYNCROOT = new object();
		private static Thread _th = null;
		private static Queue<byte[]> _q = new Queue<byte[]>();

		public static void Send(byte[] block)
		{
			lock (SYNCROOT)
			{
				_q.Enqueue(block);

				if (_th == null)
				{
					_th = new Thread((ThreadStart)delegate
					{
						using (Mutector.Sender sender = new Mutector.Sender(IDENT))
						{
							for (; ; )
							{
								byte[] message;

								lock (SYNCROOT)
								{
									if (_q.Count == 0)
									{
										sender.Dispose();
										_th = null;
										break;
									}
									message = _q.Dequeue();
								}
								sender.Send(message);
							}
						}
					});
					_th.Start();
				}
			}
		}
#else
		public static void Send(byte[] block)
		{ }
#endif
	}
}
