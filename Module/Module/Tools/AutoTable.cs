using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AutoTable<T>
	{
		private List<List<T>> _rows = new List<List<T>>();
		private int _w = 0;
		private int _h = 0;
		private T _defval;

		public AutoTable(T defval)
		{
			_defval = defval;
		}

		public void Clear()
		{
			_rows.Clear();
			_w = 0;
			_h = 0;
		}

		public int W
		{
			set
			{
				if (value < 0) throw null;
				_w = value;
			}
			get
			{
				return _w;
			}
		}

		public int H
		{
			set
			{
				if (value < 0) throw null;
				_h = value;
			}
			get
			{
				return _h;
			}
		}

		private void Touch(int x, int y)
		{
			if (x < 0) throw null;
			if (y < 0) throw null;

			while (_rows.Count <= y)
				_rows.Add(new List<T>());

			while (_rows[y].Count <= x)
				_rows[y].Add(_defval);
		}

		public T this[int x, int y]
		{
			set
			{
				this.Touch(x, y);
				_rows[y][x] = value;
				_w = Math.Max(_w, x + 1);
				_h = Math.Max(_h, y + 1);
			}
			get
			{
				this.Touch(x, y);
				return _rows[y][x];
			}
		}

		private void Capture(AutoTable<T> target)
		{
			_rows = target._rows;
			_w = target._w;
			_h = target._h;
		}

		public void Twist()
		{
			AutoTable<T> dest = new AutoTable<T>(_defval);

			for (int x = 0; x < _w; x++)
				for (int y = 0; y < _h; y++)
					dest[y, x] = this[x, y];

			this.Capture(dest);
		}

		public void Mirror()
		{
			foreach (List<T> row in _rows)
			{
				while (row.Count < _w)
					row.Add(_defval);

				row.Reverse();
			}
		}

		public void Reverse()
		{
			_rows.Reverse();
		}

		public void Rotate90()
		{
			this.Reverse();
			this.Twist();
		}

		public void Rotate180()
		{
			this.Mirror();
			this.Reverse();
		}

		public void Rotate270()
		{
			this.Twist();
			this.Reverse();
		}

		public void AddRow()
		{
			_rows.Add(new List<T>());
		}

		public void Add(T element)
		{
			this[_rows[_rows.Count - 1].Count - 1, _rows.Count - 1] = element;
		}
	}
}
