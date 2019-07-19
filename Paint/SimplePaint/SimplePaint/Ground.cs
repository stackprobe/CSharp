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
			THICK,
			THICK_x2,
			THICK_x3,
		}

		public Nib_e Nib = Nib_e.SIMPLE;
		public Color NibColor = Color.Black;
		public Func<int, int, bool> NibRoutine = null; // ボタン押下時に発動する。真を返すと解除する。偽を返すと解除しない。

		public bool NibDown = false;
		public int LastNibX = 0;
		public int LastNibY = 0;

		public string ActiveImageFile = null;
		public bool AntiAliasing = false;

		public History History = new History();
	}
}
