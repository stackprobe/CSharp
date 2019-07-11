using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Cell
	{
		public bool Wall;
		public bool Box;
		public bool Point;
		public bool Reachable;

		public string GetString()
		{
			return ((this.Wall ? 1 : 0) + (this.Box ? 2 : 0) + (this.Point ? 4 : 0)) + "" + (this.Reachable ? 'R' : '-');
		}

		public void CopyTo(Cell dest)
		{
			dest.Wall = this.Wall;
			dest.Box = this.Box;
			dest.Point = this.Point;
			dest.Reachable = this.Reachable;
		}
	}
}
