using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte
{
	public class RandomizedErrorHandle : IDisposable
	{
		private const double ERROR_RATE = 0.1;

		public static int OpenedHandleCount = 0;

		public RandomizedErrorHandle()
		{
			if (SecurityTools.CRandom.GetReal() < ERROR_RATE)
				throw new Exception("from Ctor");

			OpenedHandleCount++;
		}

		public void Dispose()
		{
			OpenedHandleCount--;

			if (SecurityTools.CRandom.GetReal() < ERROR_RATE)
				throw new Exception("from Dispose");
		}
	}
}
