using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class Cell
	{
		public enum State_e
		{
			EMPTY,
			WALL,
			BOX,
			POINT,
			POINT_BOX,
		};

		public State_e State = State_e.EMPTY;

		// <---- prm

		public bool Fill
		{
			get
			{
				return this.State == State_e.WALL || this.State == State_e.BOX || this.State == State_e.POINT_BOX;
			}
		}

		public bool Box
		{
			get
			{
				return this.State == State_e.BOX || this.State == State_e.POINT_BOX;
			}
		}

		public bool Point
		{
			get
			{
				return this.State == State_e.POINT || this.State == State_e.POINT_BOX;
			}
		}

		// Moment.Next {

		public bool Checked;

		// }

		// Moment.IsMovable {

		public bool Reached;

		// }
	}
}
