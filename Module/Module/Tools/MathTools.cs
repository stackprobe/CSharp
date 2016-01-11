using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class MathTools
	{
		private static Random SharedRandom = new Random();

		public static int Random(int modulo)
		{
			return SharedRandom.Next(modulo);
		}
	}
}
