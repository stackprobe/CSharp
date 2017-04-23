using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte.Tools
{
	public class MonitorCenter
	{
		private List<Monitor> _monitors = new List<Monitor>();

		public class Monitor
		{
			public int index;
			public int l;
			public int t;
			public int w;
			public int h;

			public int r
			{
				get
				{
					return l + w;
				}
			}

			public int b
			{
				get
				{
					return t + h;
				}
			}
		}

		public int l;
		public int t;
		public int r;
		public int b;

		public MonitorCenter()
		{
			foreach (Screen s in Screen.AllScreens)
			{
				_monitors.Add(new Monitor()
				{
					index = _monitors.Count,
					l = s.Bounds.Left,
					t = s.Bounds.Top,
					w = s.Bounds.Width,
					h = s.Bounds.Height,
				});
			}
			l = _monitors[0].l;
			t = _monitors[0].t;
			r = _monitors[0].r;
			b = _monitors[0].b;

			for (int index = 1; index < _monitors.Count; index++)
			{
				l = Math.Min(l, _monitors[index].l);
				t = Math.Min(t, _monitors[index].t);
				r = Math.Max(r, _monitors[index].r);
				b = Math.Max(b, _monitors[index].b);
			}
		}

		public int getCount()
		{
			return _monitors.Count;
		}

		public Monitor get(int index)
		{
			return _monitors[index];
		}

		public int w
		{
			get
			{
				return r - l;
			}
		}

		public int h
		{
			get
			{
				return b - t;
			}
		}
	}
}
