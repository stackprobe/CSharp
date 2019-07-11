using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace Charlotte
{
	public class Ground
	{
		public static Ground I;

		public enum Nib_e
		{
			SIMPLE,
		}

		public Nib_e Nib = Nib_e.SIMPLE;
		public Color NibColor = Color.Black;

		public bool NibDown = false;
		public int LastNibX = 0;
		public int LastNibY = 0;
	}
}
