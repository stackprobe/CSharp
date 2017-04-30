using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace Charlotte.Tools
{
	/// <summary>
	/// thread safe
	/// </summary>
	public class Fortewave : IDisposable
	{
		private object SYNCROOT_R = new object();
		private object SYNCROOT_W = new object();
		private PostOfficeBox _rPob;
		private PostOfficeBox _wPob;

		public Fortewave(string ident)
			: this(ident, ident)
		{ }

		public Fortewave(string rIdent, string wIdent)
		{
			_rPob = new PostOfficeBox(rIdent);
			_wPob = new PostOfficeBox(wIdent);
		}

		public void clear()
		{
			lock (SYNCROOT_R)
			{
				_rPob.clear();
			}
			lock (SYNCROOT_W)
			{
				_wPob.clear();
			}
		}

		public void send(object sendObj)
		{
			lock (SYNCROOT_W)
			{
				_wPob.send(new Serializer(sendObj).getBuff());
			}
		}

		public object recv(int millis)
		{
			lock (SYNCROOT_R)
			{
				byte[] recvData = _rPob.recv(millis);

				if (recvData == null)
					return null;

				object recvObj = new Deserializer(recvData).next();
				return recvObj;
			}
		}

		public void pulse()
		{
			lock (SYNCROOT_R)
			{
				_rPob.pulse();
			}
			lock (SYNCROOT_W)
			{
				_wPob.pulse();
			}
		}

		public void Dispose()
		{
			if (_rPob != null)
			{
				_rPob.Dispose();
				_rPob = null;

				_wPob.Dispose();
				_wPob = null;
			}
		}

		//public static List<double> laps = new List<double>(); // test

		private class PostOfficeBox : IDisposable
		{
			private const string IDENT_PREFIX = "Fortewave_{d8600f7d-1ff4-47f3-b1c9-4b5aa15b6461}_"; // shared_uuid@g
			private string _ident;
			private MutexObject _mutex;
			private NamedEventObject _messagePostEvent;
			private string _messageDir;

			public PostOfficeBox(String ident)
			{
				_ident = IDENT_PREFIX + StringTools.toHex(SecurityTools.getSHA512(StringTools.ENCODING_SJIS.GetBytes(ident))).Substring(0, 32);
				_mutex = new MutexObject(_ident + "_m");
				_messagePostEvent = new NamedEventObject(_ident + "_e");
				_messageDir = Path.Combine(Environment.GetEnvironmentVariable("TMP"), _ident);
			}

			public void clear()
			{
				using (MutexObject.section(_mutex))
				{
					FileTools.deletePath(_messageDir);
				}
			}

			public void send(Queue<byte[]> sendData)
			{
				//Stopwatch sw = new Stopwatch(); // test

				//sw.Reset();
				//sw.Start();
				using (MutexObject.section(_mutex))
				{
					//sw.Stop();
					//laps.Add(sw.Elapsed.TotalMilliseconds);
					//sw.Reset();
					//sw.Start();
					getMessageRange();
					//sw.Stop();
					//laps.Add(sw.Elapsed.TotalMilliseconds);
					//sw.Reset();
					//sw.Start();
					tryRenumber();
					//sw.Stop();
					//laps.Add(sw.Elapsed.TotalMilliseconds);
					//sw.Reset();
					//sw.Start();

					if (_gmrLastNo == -1)
					{
						FileTools.deletePath(_messageDir);
						Directory.CreateDirectory(_messageDir);
					}
					//sw.Stop();
					//laps.Add(sw.Elapsed.TotalMilliseconds);
					//sw.Reset();
					//sw.Start();
					using (FileStream wfs = new FileStream(
						Path.Combine(_messageDir, StringTools.zPad(_gmrLastNo + 1, 4)),
						FileMode.Create,
						FileAccess.Write
						))
						foreach (byte[] block in sendData)
							wfs.Write(block, 0, block.Length);
					//sw.Stop();
					//laps.Add(sw.Elapsed.TotalMilliseconds);
					//sw.Reset();
					//sw.Start();
				}
				//sw.Stop();
				//laps.Add(sw.Elapsed.TotalMilliseconds);
				//sw.Reset();
				//sw.Start();
				_messagePostEvent.set();
				//sw.Stop();
				//laps.Add(sw.Elapsed.TotalMilliseconds);
			}

			public byte[] recv(int millis)
			{
				byte[] recvData = tryRecv();

				if (recvData == null)
				{
					_messagePostEvent.waitForMillis(millis);
					recvData = tryRecv();
				}
				return recvData;
			}

			private byte[] tryRecv()
			{
				using (MutexObject.section(_mutex))
				{
					getMessageRange();

					if (_gmrFirstNo != -1)
					{
						String file = Path.Combine(_messageDir, StringTools.zPad(_gmrFirstNo, 4));
						byte[] recvData = File.ReadAllBytes(file);

						FileTools.deletePath(file);

						if (_gmrFirstNo == _gmrLastNo)
							FileTools.deletePath(_messageDir);

						return recvData;
					}
				}
				return null;
			}

			private int _gmrFirstNo;
			private int _gmrLastNo;

			private void getMessageRange()
			{
				_gmrFirstNo = -1;
				_gmrLastNo = -1;

				if (Directory.Exists(_messageDir) == false)
					return;

				foreach (string file in FileTools.lsFiles(_messageDir))
				{
					string lFile = Path.GetFileName(file);
					int no = int.Parse(lFile);

					if (_gmrFirstNo == -1)
						_gmrFirstNo = no;
					else
						_gmrFirstNo = Math.Min(_gmrFirstNo, no);

					_gmrLastNo = Math.Max(_gmrLastNo, no);
				}
			}

			private void tryRenumber()
			{
				if (1000 < _gmrFirstNo)
				{
					renumber();
				}
			}

			private void renumber()
			{
				for (int no = _gmrFirstNo; no <= _gmrLastNo; no++)
				{
					File.Move(
						Path.Combine(_messageDir, StringTools.zPad(no, 4)),
						Path.Combine(_messageDir, StringTools.zPad(no - _gmrFirstNo, 4))
						);
				}
				_gmrLastNo -= _gmrFirstNo;
				_gmrFirstNo = 0;
			}

			public void pulse()
			{
				_messagePostEvent.set();
			}

			public void Dispose()
			{
				if (_mutex != null)
				{
					_mutex.Dispose();
					_mutex = null;

					_messagePostEvent.Dispose();
					_messagePostEvent = null;
				}
			}
		}

		// ---- Serializer, Deserializer ----

		private static readonly byte KIND_NULL = Encoding.ASCII.GetBytes("N")[0];
		private static readonly byte KIND_BYTES = Encoding.ASCII.GetBytes("B")[0];
		private static readonly byte KIND_MAP = Encoding.ASCII.GetBytes("M")[0];
		private static readonly byte KIND_LIST = Encoding.ASCII.GetBytes("L")[0];
		private static readonly byte KIND_STRING = Encoding.ASCII.GetBytes("S")[0];

		// ----

		private class Serializer
		{
			private Queue<byte[]> _buff = new Queue<byte[]>();

			public Serializer()
			{ }

			public Serializer(object obj)
			{
				add(obj);
			}

			public void add(object obj)
			{
				if (obj == null)
				{
					addByte(KIND_NULL);
				}
				else if (obj is byte[])
				{
					addByte(KIND_BYTES);
					addBlock((byte[])obj);
				}
				else if (obj is ObjectMap)
				{
					ObjectMap om = (ObjectMap)obj;

					addByte(KIND_MAP);
					addInt(om.getCount());

					foreach (string key in om.getKeys())
					{
						add(key);
						add(om[key]);
					}
				}
				else if (obj is ObjectList)
				{
					ObjectList ol = (ObjectList)obj;

					addByte(KIND_LIST);
					addInt(ol.getCount());

					foreach (object value in ol.getList())
					{
						add(value);
					}
				}
				else if (obj is string)
				{
					addByte(KIND_STRING);
					addBlock(Encoding.UTF8.GetBytes((string)obj));
				}
				else
				{
					throw new Exception("未対応オブジェクトの型：" + obj.GetType());
				}
			}

			private void addByte(byte chr)
			{
				_buff.Enqueue(new byte[] { chr });
			}

			private void addBlock(byte[] block)
			{
				addInt(block.Length);
				_buff.Enqueue(block);
			}

			private void addInt(int value)
			{
				byte[] block = new byte[4];

				// big endian
				block[0] = (byte)((value >> 24) & 0xff);
				block[1] = (byte)((value >> 16) & 0xff);
				block[2] = (byte)((value >> 8) & 0xff);
				block[3] = (byte)((value >> 0) & 0xff);

				_buff.Enqueue(block);
			}

			public Queue<byte[]> getBuff()
			{
				return _buff;
			}
		}

		private class Deserializer
		{
			private byte[] _data;
			private int _rPos;

			public Deserializer(byte[] data)
			{
				_data = data;
			}

			private byte readByte()
			{
				return _data[_rPos++];
			}

			private int readInt()
			{
				byte b1 = readByte();
				byte b2 = readByte();
				byte b3 = readByte();
				byte b4 = readByte();

				// big endian
				return
					((int)b1 << 24) |
					((int)b2 << 16) |
					((int)b3 << 8) |
					((int)b4 << 0);
			}

			private byte[] readBlock(int size)
			{
				byte[] dest = new byte[size];
				Array.Copy(_data, _rPos, dest, 0, size);
				_rPos += size;
				return dest;
			}

			private byte[] readBlock()
			{
				return readBlock(readInt());
			}

			public object next()
			{
				byte kind = readByte();

				if (kind == KIND_NULL)
				{
					return null;
				}
				if (kind == KIND_BYTES)
				{
					return readBlock();
				}
				if (kind == KIND_MAP)
				{
					ObjectMap om = new ObjectMap();
					int size = readInt();

					for (int index = 0; index < size; index++)
					{
						object key = next();
						object value = next();

						om.add(key, value);
					}
					return om;
				}
				if (kind == KIND_LIST)
				{
					ObjectList ol = new ObjectList();
					int size = readInt();

					for (int index = 0; index < size; index++)
					{
						ol.add(next());
					}
					return ol;
				}
				if (kind == KIND_STRING)
				{
					return Encoding.UTF8.GetString(readBlock());
				}
				throw new Exception("不明なオブジェクトの型：" + kind);
			}
		}
	}
}
