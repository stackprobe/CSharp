using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public class FileSvMan
	{
		private static string _file;

		public static string GetFile()
		{
			if (_file == null)
			{
				_file = "FileSv.exe";

				FJammer.Decode(_file);

				if (File.Exists(_file) == false)
					_file = @"C:\Factory\SubTools\Chat\FileSv.exe";
			}
			return _file;
		}

		private ProcessMan ProcMan = new ProcessMan(1);
		private int LastIdentPort;

		public void Begin()
		{
			if (Gnd.I.Sd.FileSvEnabled == false)
				return;

			this.Start();
		}

		public void Start()
		{
			this.ProcMan.Start(
				GetFile(),
				"/CL #" + DataConv.GetString(Gnd.I.Sd.B_LinkColor) +
				" /CB #" + DataConv.GetString(Gnd.I.Sd.B_BackColor) +
				" /CT #" + DataConv.GetString(Gnd.I.Sd.B_TextColor) +
				" /DD \"" + Gnd.I.Sd.FileSvHomeDir + "\" " +
				(Gnd.I.Sd.FileSvVisibleHomeDirOnly ? "/VDDO " : "") +
				Gnd.I.Sd.FileSvRecvPort
				);
			this.LastIdentPort = Gnd.I.Sd.FileSvRecvPort;
		}

		public bool IsEnd(int count = 0)
		{
			if (this.ProcMan.IsEnd())
				return true;

			if (count % 20 == 0)
			{
				ProcessMan pm = new ProcessMan();

				pm.Start(GetFile(), "/S " + this.LastIdentPort);
				pm.End();
			}
			return false;
		}

		public void End()
		{
			for (int c = 0; this.IsEnd(c) == false; c++)
			{
				Thread.Sleep(100);
			}
		}

		public void EndAndBegin_MainWin()
		{
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.FileSvMan_End);
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				this.Begin();
			});
		}
	}
}
