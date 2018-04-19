using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Charlotte.Tools;
using System.IO;

namespace Charlotte
{
	public class CrypTunnelProc
	{
		private string _crypTunnelExeFile = null;

		private string crypTunnelExeFile
		{
			get
			{
				if (_crypTunnelExeFile == null)
				{
					_crypTunnelExeFile = Path.Combine(Program.selfDir, "crypTunnel.exe");

					if (File.Exists(_crypTunnelExeFile) == false)
						_crypTunnelExeFile = @"C:\Factory\Labo\Socket\tunnel\crypTunnel.exe";
				}
				return _crypTunnelExeFile;
			}
		}

		private Process Proc = null;

		/// <summary>
		/// 停止していれば起動する。
		/// </summary>
		public void Wake()
		{
			CheckEnded();

			if (Proc == null)
			{
				Proc = ProcessTools.start(
					crypTunnelExeFile,
					Gnd.setting.crypTunnelPort + " " + Gnd.setting.ServerDomain + " " + Gnd.setting.ServerPort + " /C 1 /CVP " + Gnd.CliVerifyPtn + " *" + Gnd.setting.Password + Consts.PASSWORD_TRAILER
					);
			}
		}

		/// <summary>
		/// 起動中であれば停止する。
		/// 停止するまで何度も呼び出すこと。
		/// </summary>
		/// <returns>停止している</returns>
		public bool End()
		{
			TryEnd();
			CheckEnded();
			return Proc == null;
		}

		private int TE_FreezeCount = 0;
		private Process TE_Proc = null;

		private void TryEnd()
		{
			if (0 < TE_FreezeCount)
			{
				TE_FreezeCount--;
				return;
			}
			if (TE_Proc != null && TE_Proc.HasExited == false)
				return;

			TE_FreezeCount = 30;
			TE_Proc = ProcessTools.start(
				crypTunnelExeFile,
				Gnd.setting.crypTunnelPort + " " + Gnd.setting.ServerDomain + " " + Gnd.setting.ServerPort + " /S"
				);
		}

		private void CheckEnded()
		{
			if (Proc != null && Proc.HasExited)
				Proc = null;
		}
	}
}
