using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class SSRBClient
	{
		public string Domain = "localhost";
		public int PortNo = 55985;
		public string[] SendFiles = new string[0];
		public string[] RecvFiles = new string[0];
		public string[] Commands = new string[] { "DIR" };

		// 引数ここまで

		public string[] OutLines;

		// 応答ここまで

		private SockClient Connection;

		public void Perform()
		{
			foreach (string file in this.SendFiles) // 入力ファイルの読み込みテスト
			{
#if false
				using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
				{
					reader.ReadByte();
					reader.ReadByte();
					reader.ReadByte();
				}
#else
				File.ReadAllBytes(file);
#endif
			}
			foreach (string file in this.RecvFiles) // 出力ファイルの掃除
			{
				//File.Delete(file); // 入力ファイルを出力ファイルにしているかもしれないので、削除しない！
			}
			using (SockClient connection = new SockClient(this.Domain, this.PortNo))
			{
				connection.RSTimeoutMillis = 2000; // 2 sec

				connection.Send(Encoding.ASCII.GetBytes("SSRB/0.0"));

				connection.RSTimeoutMillis = 30000; // 30 sec

				this.Connection = connection;

				this.SendUInt((uint)this.SendFiles.Length);

				foreach (string file in this.SendFiles)
				{
					this.SendLine(Path.GetFileName(file));
					this.SendData(File.ReadAllBytes(file));
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
				if (this.RecvFiles.Length != this.RecvUInt())
				{
					throw new Exception("応答ファイル数の不一致");
				}
				foreach (string file in this.RecvFiles)
				{
					if (Path.GetFileName(file) != this.RecvLine())
					{
						throw new Exception("応答ファイル名の不一致");
					}
					File.WriteAllBytes(file, this.RecvData());
				}
				this.OutLines = new string[(int)this.RecvUInt()];

				for (int index = 0; index < this.OutLines.Length; index++)
				{
					this.OutLines[index] = this.RecvLine();
				}
			}
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
