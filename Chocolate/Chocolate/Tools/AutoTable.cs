﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class AutoTable<T>
	{
		private AutoList<AutoList<T>> Rows;

		public int W;
		public int H;

		public AutoTable()
			: this(16, 16)
		{ }

		public AutoTable(int w, int h)
		{
			if (w < 1 || h < 1)
				throw new ArgumentException();

			this.Rows = new AutoList<AutoList<T>>(h);
			this.W = w;
			this.H = h;
		}

		private AutoList<T> GetRow(int y)
		{
			AutoList<T> row = this.Rows[y];

			if (row == null)
			{
				row = new AutoList<T>(this.W);
				this.Rows[y] = row;
			}
			return row;
		}

		public T this[int x, int y]
		{
			get
			{
				return this.GetRow(y)[x];
			}

			set
			{
				this.GetRow(y)[x] = value;
			}
		}
	}
}
