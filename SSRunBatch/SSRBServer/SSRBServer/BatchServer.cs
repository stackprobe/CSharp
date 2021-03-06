﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace Charlotte
{
	public class BatchServer
	{
		public SockServer SockServer;

		public BatchServer()
		{
			this.SockServer = new SockServer(Gnd.PortNo, this.Perform);
		}

		private SockServer.Connection Connection;

		private void Perform(SockServer.Connection connection)
		{
			Logger.WriteLine("Start");

			connection.RSTimeoutMillis = 2000; // 2 sec

			if (Consts.ENCODING_SJIS.GetString(connection.Recv(8)) != "SSRB/0.0")
				throw new Exception("シグネチャ不一致");

			connection.RSTimeoutMillis = 30000; // 30 sec

			this.Connection = connection;

			string workDir = Path.Combine(Gnd.RootWorkDir, Guid.NewGuid().ToString("B"));
			Directory.CreateDirectory(workDir);

			int sendFileNum = (int)this.RecvUInt();

			for (int index = 0; index < sendFileNum; index++)
			{
				string localName = this.RecvLine();
				byte[] fileData = this.RecvData();
				string file = Path.Combine(workDir, localName);

				File.WriteAllBytes(file, fileData);
			}
			int recvFileNum = (int)this.RecvUInt();
			string[] recvFiles = new string[recvFileNum];

			for (int index = 0; index < recvFileNum; index++)
			{
				string localName = this.RecvLine();
				string file = Path.Combine(workDir, localName);

				recvFiles[index] = file;
			}
			int commandNum = (int)this.RecvUInt();

			// Windows7で0バイトのバッチファイルを実行するとエラーdlgが出る。
			if (commandNum < 1)
				throw new Exception("コマンドがありません。");

			string[] commands = new string[commandNum];

			for (int index = 0; index < commandNum; index++)
			{
				commands[index] = this.RecvLine();
			}
			string uuid = Guid.NewGuid().ToString("B");
			string batFile = Path.Combine(workDir, uuid + "_Run.bat");
			string outFile = Path.Combine(workDir, uuid + "_Run.out");
			string callBatFile = Path.Combine(workDir, uuid + "_Call.bat");

			File.WriteAllLines(batFile, commands, Consts.ENCODING_SJIS);
			File.WriteAllLines(callBatFile, new string[] { "> " + Path.GetFileName(outFile) + " CALL " + Path.GetFileName(batFile) }, Consts.ENCODING_SJIS);

			{
				ProcessStartInfo psi = new ProcessStartInfo();

				psi.FileName = "cmd";
				psi.Arguments = "/c " + Path.GetFileName(callBatFile);
				psi.CreateNoWindow = true;
				psi.UseShellExecute = false;
				psi.WorkingDirectory = workDir;

				Process p = Process.Start(psi);

				Gnd.AbandonCurrentRunningBatchFlag = false;

				while (p.WaitForExit(2000) == false)
				{
					if (Gnd.AbandonCurrentRunningBatchFlag)
					{
						Gnd.AbandonCurrentRunningBatchFlag = false;

						// あるポート(P)をSocket.Bindしたプロセス(A)が生成したプロセス(B)がずっと生きていると、
						// プロセス(A)がSocketを閉じて終了した後でもプロセス(B)が終了するまでポート(P)をSocket.Bind出来なくなる。
						// なので放置ではなくKillすることにした。
						try
						{
							p.Kill();
						}
						catch (Exception e)
						{
							Logger.WriteLine(e);
						}

						throw new Exception("実行中のバッチファイルを強制終了しました。");
					}
				}
			}

			this.SendUInt((uint)recvFileNum);

			for (int index = 0; index < recvFileNum; index++)
			{
				string file = recvFiles[index];
				byte[] fileData = File.ReadAllBytes(file);

				this.SendLine(Path.GetFileName(file));
				this.SendData(fileData);
			}
			string[] outLines;

			try // Try twice
			{
				outLines = File.ReadAllLines(outFile, Consts.ENCODING_SJIS);
			}
			catch
			{
				Thread.Sleep(100);
				outLines = File.ReadAllLines(outFile, Consts.ENCODING_SJIS);
			}

			this.SendUInt((uint)outLines.Length);

			foreach (string outLine in outLines)
			{
				this.SendLine(outLine);
			}

			try // Try twice
			{
				Directory.Delete(workDir, true);
			}
			catch
			{
				Thread.Sleep(100);
				Directory.Delete(workDir, true);
			}

			Logger.WriteLine("End");
		}

		private string RecvLine()
		{
			return Consts.ENCODING_SJIS.GetString(this.RecvData());
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

		private void SendLine(string line)
		{
			this.SendData(Consts.ENCODING_SJIS.GetBytes(line));
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
	}
}
