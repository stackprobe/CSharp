using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class HTTPServerChannel
	{
		public SockChannel Channel;

		public void RecvRequest()
		{
			this.Channel.IdleTimeoutMillis = 2000; // 2 sec

			try
			{
				this.FirstLine = this.RecvLine();
			}
			catch (Exception e)
			{
				throw new Exception("RECV_FIRST_LINE_ERROR", e);
			}

			{
				string[] tokens = this.FirstLine.Split(' ');

				this.Method = tokens[0];
				this.Path = DecodeURL(tokens[1]);
				this.HTTPVersion = tokens[2];
			}

			this.Channel.IdleTimeoutMillis = 180000; // 3 min

			this.RecvHeader();
			this.CheckHeader();

			if (this.Expect100Continue)
			{
				this.SendLine("HTTP/1.1 100 Continue");
				this.SendLine("");
			}
			this.RecvBody();
		}

		private string DecodeURL(string path)
		{
			byte[] src = Encoding.ASCII.GetBytes(path);

			using (MemoryStream dest = new MemoryStream())
			{
				for (int index = 0; index < src.Length; index++)
				{
					if (src[index] == 0x25) // ? '%'
					{
						dest.WriteByte((byte)Convert.ToInt32(Encoding.ASCII.GetString(BinTools.GetSubBytes(src, index + 1, 2)), 16));
						index += 2;
					}
					else
					{
						dest.WriteByte(src[index]);
					}
				}
				return Encoding.UTF8.GetString(dest.ToArray());
			}
		}

		public string FirstLine;
		public string Method;
		public string Path;
		public string HTTPVersion;
		public string Schema;
		public List<string[]> HeaderPairs = new List<string[]>();
		public byte[] Body;

		private const byte CR = 0x0d;
		private const byte LF = 0x0a;

		private string RecvLine()
		{
			using (MemoryStream buff = new MemoryStream())
			{
				for (; ; )
				{
					byte chr = this.Channel.Recv(1)[0];

					if (chr == CR)
						continue;

					if (chr == LF)
						break;

					if (512000 < buff.Length)
						throw new OverflowException();

					buff.WriteByte(chr);
				}
				return Encoding.ASCII.GetString(buff.ToArray());
			}
		}

		private void RecvHeader()
		{
			int headerRoughLength = 0;

			for (; ; )
			{
				string line = this.RecvLine();

				if (line == "")
					break;

				headerRoughLength += line.Length + 10;

				if (512000 < headerRoughLength)
					throw new OverflowException();

				if (line[0] <= ' ')
				{
					this.HeaderPairs[this.HeaderPairs.Count - 1][1] += " " + line.Trim();
				}
				else
				{
					int colon = line.IndexOf(':');

					this.HeaderPairs.Add(new string[]
					{
						line.Substring(0, colon).Trim(),
						line.Substring(colon + 1).Trim(),
					});
				}
			}
		}

		public int ContentLength = 0;
		public bool Chunked = false;
		public string ContentType = null;
		public bool Expect100Continue = false;

		private void CheckHeader()
		{
			foreach (string[] pair in this.HeaderPairs)
			{
				string key = pair[0];
				string value = pair[1];

				if (StringTools.EqualsIgnoreCase(key, "Content-Length"))
				{
					this.ContentLength = int.Parse(value);
				}
				else if (StringTools.EqualsIgnoreCase(key, "Transfer-Encoding") && StringTools.EqualsIgnoreCase(value, "chunked"))
				{
					this.Chunked = true;
				}
				else if (StringTools.EqualsIgnoreCase(key, "Content-Type"))
				{
					this.ContentType = value;
				}
				else if (StringTools.EqualsIgnoreCase(key, "Expect") && StringTools.EqualsIgnoreCase(value, "100-continue"))
				{
					this.Expect100Continue = true;
				}
			}
		}

		public static int BodySizeMax = 300000000; // 300 MB

		private void RecvBody()
		{
			using (ByteArrayLowCostBuffer buff = new ByteArrayLowCostBuffer())
			{
				if (this.Chunked)
				{
					for (; ; )
					{
						string line = this.RecvLine();

						// chunk-extension の削除
						{
							int i = line.IndexOf(';');

							if (i != -1)
								line = line.Substring(0, i);
						}

						int size = Convert.ToInt32(line.Trim(), 16);

						if (size == 0)
							break;

						if (size < 0)
							throw new Exception("不正なチャンクサイズです。" + size);

						if (BodySizeMax - buff.Count < size)
							throw new Exception("ボディサイズが大きすぎます。" + buff.Count + " + " + size);

						buff.Write(this.Channel.Recv(size));
						this.Channel.Recv(2); // CR-LF
					}
				}
				else
				{
					if (this.ContentLength < 0)
						throw new Exception("不正なボディサイズです。" + this.ContentLength);

					if (BodySizeMax < this.ContentLength)
						throw new Exception("ボディサイズが大きすぎます。" + this.ContentLength);

					while (buff.Count < this.ContentLength)
						buff.Write(this.Channel.Recv(Math.Min(4 * 1024 * 1024, this.ContentLength - buff.Count)));
				}
				this.Body = buff.ToByteArray();
			}
		}

		public int ResStatus = 200;
		public string ResServer = null;
		public string ResContentType = null;
		public List<string[]> ResHeaderPairs = new List<string[]>();
		public byte[] ResBody = null;

		public void SendResponse()
		{
			this.SendLine("HTTP/1.1 " + this.ResStatus + " Chocolate Cake");

			if (this.ResServer != null)
				this.SendLine("Server: " + this.ResServer);

			if (this.ResContentType != null)
				this.SendLine("Content-Type: " + this.ResContentType);

			foreach (string[] pair in this.ResHeaderPairs)
				this.SendLine(pair[0] + ": " + pair[1]);

			if (this.ResBody != null)
				this.SendLine("Content-Length: " + this.ResBody.Length);

			this.SendLine("Connection: close");
			this.SendLine("");

			this.Channel.Send(this.ResBody);
		}

		private readonly byte[] CRLF = new byte[] { CR, LF };

		private void SendLine(string line)
		{
			this.Channel.Send(Encoding.ASCII.GetBytes(line));
			this.Channel.Send(CRLF);
		}
	}
}
