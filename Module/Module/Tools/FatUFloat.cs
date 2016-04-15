using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatUFloat
	{
		private FatUInt _value;
		private UInt64 _radix; // 2 ～ 2^64-1
		private int _exponent; // -IMAX ～ IMAX

		public FatUFloat(FatUInt value, UInt64 radix = 10, int exponent = 0)
		{
			if (value == null) throw new ArgumentException();
			if (radix < 2) throw new ArgumentException();
			if (exponent < -IntTools.IMAX || IntTools.IMAX < exponent) throw new ArgumentException();

			_value = value;
			_radix = radix;
			_exponent = exponent;
		}
	}
}
