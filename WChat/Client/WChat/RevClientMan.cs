using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public class RevClientMan
	{
		private static string _file;

		public static string GetFile()
		{
			if (_file == null)
			{
				_file = "revClient.exe";

				FJammer.Decode(_file);

				if (File.Exists(_file) == false)
					_file = @"C:\Factory\Labo\Socket\tunnel\revClient.exe";
			}
			return _file;
		}

		private ProcessMan ProcMan = new ProcessMan(1);
		private string LastConArgs;

		public void Begin()
		{
			if (Gnd.I.Sd.FileSvEnabled == false)
				return;

			this.Start();
		}

		public void Start()
		{
			this.LastConArgs = "localhost " + Gnd.I.Sd.NamedTrackPort + " localhost " + Gnd.I.Sd.FileSvRecvPort;
			this.ProcMan.Start(GetFile(), this.LastConArgs);
		}

		public bool End_IsEnd(int count = 0)
		{
			if (this.ProcMan.IsEnd())
				return true;

			if (count % 50 == 0)
			{
				ProcessMan pm = new ProcessMan();

				pm.Start(GetFile(), this.LastConArgs + " /S");
				pm.End();
			}
			return false;
		}

		public void End()
		{
			for (int c = 0; this.End_IsEnd(c) == false; c++)
			{
				Thread.Sleep(100);
			}
		}

		public void EndAndBegin_MainWin()
		{
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.RevClientMan_End);
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				this.Begin();
			});
		}
	}
}
