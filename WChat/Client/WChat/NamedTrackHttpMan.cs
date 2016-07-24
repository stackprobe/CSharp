using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Threading;

namespace Charlotte
{
	public class NamedTrackHttpMan
	{
		private static string _file;

		public static string GetFile()
		{
			if (_file == null)
			{
				_file = "namedTrackHttp.exe";

				FJammer.Decode(_file);

				if (File.Exists(_file) == false)
					_file = @"C:\Factory\SubTools\Chat\namedTrackHttp.exe";
			}
			return _file;
		}

		private ProcessMan ProcMan = new ProcessMan(1);
		private int LastIdentPort;

		public void Begin()
		{
			//if (Gnd.I.Sd.FileSvEnabled == false)
				//return;

			this.Start();
		}

		public void Start()
		{
			this.ProcMan.Start(GetFile(), Gnd.I.Sd.NamedTrackHttpPort + " " + Gnd.I.Sd.ServerDomain + " " + Gnd.I.Sd.FileSvPort);
			this.LastIdentPort = Gnd.I.Sd.NamedTrackHttpPort;
		}

		public bool End_IsEnd(int count = 0)
		{
			if (this.ProcMan.IsEnd())
				return true;

			if (count % 20 == 0)
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
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, EventCollection.NamedTrackHttpMan_End);
			EventCenter.I.AddEvent(Consts.EVENT_REGULAR, delegate
			{
				this.Begin();
			});
		}
	}
}
