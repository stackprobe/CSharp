﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class D2Point
	{
		public double X;
		public double Y;

		public D2Point()
		{ }

		public D2Point(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}

		public static D2Point operator +(D2Point a, D2Point b)
		{
			return new D2Point(a.X + b.X, a.Y + b.Y);
		}

		public static D2Point operator -(D2Point a, D2Point b)
		{
			return new D2Point(a.X - b.X, a.Y - b.Y);
		}

		public static D2Point operator *(D2Point a, double b)
		{
			return new D2Point(a.X * b, a.Y * b);
		}

		public static D2Point operator /(D2Point a, double b)
		{
			return new D2Point(a.X / b, a.Y / b);
		}
	}
}
