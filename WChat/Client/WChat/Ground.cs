using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Gnd
	{
		public static Gnd I = new Gnd();

		private Gnd()
		{ }

		public SaveData Sd = new SaveData();
		public ChatMan ChatMan;
		public FileSvMan FileSvMan = new FileSvMan();
		public NamedTrackHttpMan NamedTrackHttpMan = new NamedTrackHttpMan();
		public NamedTrackMan NamedTrackMan = new NamedTrackMan();
		public RevClientMan RevClientMan = new RevClientMan();
		public TimeLine TimeLine;
		public Heartbeat Heartbeat;
		public MainWin MainWin;

		public int ServerTimeDiff;
	}
}
