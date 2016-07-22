using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public class NamedTrackMan
	{
		private static string _file;

		public static string GetFile()
		{
			if (_file == null)
			{
				_file = "namedTrack.exe";

				FJammer.Decode(_file);

				if (File.Exists(_file) == false)
					_file = @"C:\Factory\Labo\Socket\tunnel\namedTrack.exe";
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
				Gnd.I.Sd.NamedTrackPort + " " + Gnd.I.Sd.ServerDomain + " " + Gnd.I.Sd.FileSvPort + " " + Gnd.I.Sd.TrackName + ":SERVER.R"
				);
			this.LastIdentPort = Gnd.I.Sd.NamedTrackPort;
		}

		public bool End_IsEnd(int count = 0)
		{
			if (this.ProcMan.IsEnd())
				return true;

			if (count % 50 == 0)
			{
				ProcessMan pm = new ProcessMan();

				pm.Start(GetFile(), this.LastIdentPort + " a 1 /S");
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
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.NamedTrackMan_End);
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				this.Begin();
			});
		}
	}
}
