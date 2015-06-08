using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class EventCenter
	{
		public static EventCenter I = new EventCenter();
		public delegate void Event_d();

		private EventCenter()
		{ }

		private MapData<string, QueueData<Event_d>> Map = new MapData<string, QueueData<Event_d>>();

		public void AddEvent(string name, Event_d d_event)
		{
			QueueData<Event_d> evq = this.Map.Get(name, null);

			if (evq == null)
			{
				evq = new QueueData<Event_d>();
				this.Map.Put(name, evq);
			}
			evq.Add(d_event);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="name"></param>
		/// <returns>イベントを実行した場合 true, そうでない場合 false</returns>
		public bool Perform(string name)
		{
			QueueData<Event_d> evq = this.Map.Get(name, null);

			if (evq != null)
			{
				Event_d d_event = evq.Poll(null);

				if (d_event != null)
				{
					try
					{
						d_event();
					}
					catch (Exception e)
					{
						SystemTools.WriteLog(e);
					}
					return true;
				}
			}
			return false;
		}

		public int GetCount(string name)
		{
			QueueData<Event_d> evq = this.Map.Get(name, null);

			if (evq == null)
				return 0;

			return evq.GetCount();
		}
	}
}
