using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	/// <summary>
	/// 削除予定
	/// </summary>
	public class BatchClient
	{
		public string Domain = "localhost";
		public int PortNo = 55985;
		public string[] SendFiles;
		public string[] RecvFiles;
		public string[] Commands;

		// 引数ここまで

		public string[] OutLines;

		// 応答ここまで

		public void Perform()
		{
			using (this.Client = new SockClient())
			{
				this.Client.Connect(this.Domain, this.PortNo);

				this.Client.IdleTimeoutMillis = 2000; // 2 sec

				this.Client.Send(Encoding.ASCII.GetBytes("SSRB/0.0"));

				this.Client.IdleTimeoutMillis = 30000; // 30 sec

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

		private SockClient Client;

		private void SendLine(string line)
		{
			this.SendData(StringTools.ENCODING_SJIS.GetBytes(line));
		}

		private void SendData(byte[] data)
		{
			this.SendUInt((uint)data.Length);
			this.Client.Send(data);
		}

		private void SendUInt(uint value)
		{
			this.Client.Send(new byte[]
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
			return this.Client.Recv((int)this.RecvUInt());
		}

		private uint RecvUInt()
		{
			byte[] data = this.Client.Recv(4);

			return
				((uint)data[0] << 24) |
				((uint)data[1] << 16) |
				((uint)data[2] << 8) |
				((uint)data[3] << 0);
		}
	}
}
