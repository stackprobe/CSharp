using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatValue
	{
		private List<UInt64> _figures = new List<UInt64>();
		private UInt64 _radix = Calc.DEF_RADIX; // 2 ～ 2^64-1
		private int _exponent = 0; // 0 ～ IMAX
		private int _sign = 1; // -1 or 1
		private bool _rem = false;

		public void SetString(string str, UInt64 radix = Calc.DEF_RADIX)
		{
			if (str == null) throw new ArgumentNullException();
			if (radix < 2) throw new ArgumentOutOfRangeException();

			throw null; // TODO
		}

		public string GetString(int bracketMin = 36) // bracketMin: 0 ～ 36
		{
			if (bracketMin < 0 || 36 < bracketMin) throw new ArgumentOutOfRangeException();

			throw null; // TODO
		}

		public void SetFatFloat(FatFloat src)
		{
			throw null; // TODO
		}

		public FatFloat GetFatFloat()
		{
			throw null; // TODO
		}
	}
}
