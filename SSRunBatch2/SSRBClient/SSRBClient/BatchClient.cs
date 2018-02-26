using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public class BatchClient
	{
		public string Domain;
		public int PortNo;
		public string[] SendFiles;
		public string[] RecvFiles;
		public string[] Commands;
		public string OutLinesFile;

		// 引数ここまで

		private SockClient.Connection Connection;

		public void Perform()
		{
			foreach (string file in this.SendFiles) // 入力ファイルの読み込みテスト
			{
				using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
				{
#if true
					reader.ReadByte();
					reader.ReadByte();
					reader.ReadByte();
#else
					File.ReadAllBytes(file);
#endif
				}
			}
			foreach (string file in this.RecvFiles) // 出力ファイルの掃除
			{
				//File.Delete(file); // 入力ファイルを出力ファイルにしているかもしれないので、削除しない！
			}
			SockClient.Perform(this.Domain, this.PortNo, delegate(SockClient.Connection connection)
			{
				connection.RSTimeoutMillis = 2000; // 2 sec

				connection.Send(Encoding.ASCII.GetBytes("SSRB/0.0"));

				connection.RSTimeoutMillis = 30000; // 30 sec

				this.Connection = connection;

				this.SendUInt((uint)this.SendFiles.Length);

				foreach (string file in this.SendFiles)
				{
					this.SendLine(Path.GetFileName(file));
					this.SendFile(file);
				}
				this.SendUInt((uint)this.RecvFiles.Length);

				foreach (string file in this.RecvFiles)
				{
					this.SendLine(Path.GetFileName(file));
				}
				this.SendUInt((uint)this.Commands.Length);

				foreach (string command in this.Commands)
				{
					this.SendLine(command);
				}
				connection.RSTimeoutMillis = 86400000 * 24; // 24 days   --   int.MaxValue millis == 24.* days

				if (this.RecvFiles.Length != this.RecvUInt())
				{
					throw new Exception("応答ファイル数の不一致");
				}
				connection.RSTimeoutMillis = 30000; // 30 sec

				foreach (string file in this.RecvFiles)
				{
					if (Path.GetFileName(file) != this.RecvLine())
					{
						throw new Exception("応答ファイル名の不一致");
					}
					this.RecvFile(file);
				}

				{
					int outLineCount = (int)this.RecvUInt();

					using (StreamWriter writer = new StreamWriter(this.OutLinesFile, false, StringTools.ENCODING_SJIS))
					{
						for (int index = 0; index < outLineCount; index++)
						{
							writer.WriteLine(this.RecvLine());
						}
					}
				}
			});
		}

		private void SendLine(string line)
		{
			this.SendData(StringTools.ENCODING_SJIS.GetBytes(line));
		}

		private void SendData(byte[] data)
		{
			this.SendUInt((uint)data.Length);
			this.Connection.Send(data);
		}

		private byte[] Buff = new byte[128 * 1024 * 1024];

		private void SendFile(string file)
		{
			long fileSize = new FileInfo(file).Length;

			if ((long)uint.MaxValue < fileSize)
				throw new Exception("");

			this.SendUInt((uint)fileSize);

			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				long offset = 0L;

				while (offset < fileSize)
				{
					int size = (int)Math.Min((long)this.Buff.Length, fileSize - offset);

					if (reader.Read(this.Buff, 0, size) != size)
						throw new Exception();

					this.Connection.Send(this.Buff, 0, size);
					offset += (long)size;
				}
			}
		}

		private void SendUInt(uint value)
		{
			this.Connection.Send(new byte[]
			{
				(byte)(value >> 24),
				(byte)(value >> 16),
				(byte)(value >> 8),
				(byte)(value >> 0),
			});
		}

		private string RecvLine()
		{
			return StringTools.ENCODING_SJIS.GetString(this.RecvData());
		}

		private byte[] RecvData()
		{
			return this.Connection.Recv((int)this.RecvUInt());
		}

		private void RecvFile(string file)
		{
			long fileSize = (long)this.RecvUInt();

			using (FileStream writer = new FileStream(file, FileMode.Create, FileAccess.Write))
			{
				long offset = 0L;

				while (offset < fileSize)
				{
					int size = (int)Math.Min((long)this.Buff.Length, fileSize - offset);
					size = this.Connection.TryRecv(this.Buff, 0, size);
					writer.Write(this.Buff, 0, size);
					offset += (long)size;
				}
			}
		}

		private uint RecvUInt()
		{
			byte[] data = this.Connection.Recv(4);

			return
				((uint)data[0] << 24) |
				((uint)data[1] << 16) |
				((uint)data[2] << 8) |
				((uint)data[3] << 0);
		}
	}
}
