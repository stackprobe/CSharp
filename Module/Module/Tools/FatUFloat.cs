using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatUFloat
	{
		// 評価値 == _value / (_radix ^ _exponent)

		private const int DEF_RADIX = 10;
		private const int DEF_BASEMENT = 30;

		private FatUInt _value;
		private UInt64 _radix; // 2 ～ 2^64-1
		private int _exponent; // 0 ～ IMAX

		public FatUInt Rem = null;

		public FatUFloat(FatUInt value, UInt64 radix = DEF_RADIX, int exponent = 0)
		{
			if (value == null) throw new ArgumentException();
			if (radix < 2) throw new ArgumentOutOfRangeException();
			if (exponent < 0 || IntTools.IMAX < exponent) throw new ArgumentOutOfRangeException();

			_value = value;
			_radix = radix;
			_exponent = exponent;
		}

		public FatUFloat GetClone()
		{
			return new FatUFloat(_value.GetClone(), _radix, _exponent);
		}

		public static FatUFloat Add(FatUFloat a, FatUFloat b)
		{
			throw null; // TODO
		}

		public static FatUFloat Red(FatUFloat a, FatUFloat b)
		{
			throw null; // TODO
		}

		public static FatUFloat Mul(FatUFloat a, FatUFloat b)
		{
			throw null; // TODO
		}

		public static FatUFloat Div(FatUFloat a, FatUFloat b, int basement = DEF_BASEMENT)
		{
			throw null; // TODO
		}

		public static FatUInt Mod(FatUFloat a, FatUFloat b, int basement = DEF_BASEMENT)
		{
			return Div(a, b, basement).Rem;
		}

		public static FatUFloat Power(FatUFloat a, int exponent)
		{
			throw null; // TODO
		}

		public static FatUFloat Root(FatUFloat a, int exponent, int basement = DEF_BASEMENT)
		{
			throw null; // TODO
		}

		public void ChangeRadix(int radix = DEF_RADIX, int basement = DEF_BASEMENT)
		{
			throw null; // TODO
		}

		public static FatUFloat FromString(string src, int radix = DEF_RADIX)
		{
			throw null; // TODO
		}

		public string GetString()
		{
			throw null; // TODO
		}

		public override string ToString()
		{
			return _value.ToString() + ":" + _radix + ":" + _exponent;
		}
	}
}
