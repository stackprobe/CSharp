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

		public FatUInt Value
		{
			get
			{
				return _value;
			}
		}

		public UInt64 Radix
		{
			get
			{
				return _radix;
			}
		}

		public int Exponent
		{
			get
			{
				return _exponent;
			}
		}

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

		private static void Synchronize(FatUFloat a, FatUFloat b, int basement = 0)
		{
			if (a == null) throw new ArgumentException();
			if (b == null) throw new ArgumentException();
			if (a.Radix != b.Radix) throw new ArgumentException("different radix");
			if (basement < 0 || IntTools.IMAX < basement) throw new ArgumentException();

			int e = a.Exponent - b.Exponent - basement;

			if (e < -IntTools.IMAX || IntTools.IMAX < e) throw new ArgumentException();

			if (e < 0)
				a.Synchronize(-e);
			else if (0 < e)
				b.Synchronize(e);
		}

		private void Synchronize(int e)
		{
			_value = FatUInt.Mul(_value, FatUInt.Power(new FatUInt(_radix), e));
			_exponent += e;
		}

		public static FatUFloat Add(FatUFloat a, FatUFloat b)
		{
			Synchronize(a, b);
			return new FatUFloat(FatUInt.Add(a.Value, b.Value), a.Radix, a.Exponent);
		}

		public static FatUFloat Red(FatUFloat a, FatUFloat b)
		{
			Synchronize(a, b);
			return new FatUFloat(FatUInt.Red(a.Value, b.Value), a.Radix, a.Exponent);
		}

		public static FatUFloat Mul(FatUFloat a, FatUFloat b)
		{
			if (IntTools.IMAX < a.Exponent + b.Exponent) throw new ArgumentException();
			return new FatUFloat(FatUInt.Mul(a.Value, b.Value), a.Radix, a.Exponent + b.Exponent);
		}

		public static FatUFloat Div(FatUFloat a, FatUFloat b, int basement = DEF_BASEMENT)
		{
			Synchronize(a, b, basement);
			FatUInt value = FatUInt.Div(a.Value, b.Value);
			FatUFloat ret = new FatUFloat(value, a.Radix, basement);
			ret.Rem = value.Rem;
			return ret;
		}

		public static FatUInt Mod(FatUFloat a, FatUFloat b, int basement = DEF_BASEMENT)
		{
			return Div(a, b, basement).Rem;
		}

		public static FatUFloat Power(FatUFloat a, int exponent)
		{
			if (exponent < 0 || IntTools.IMAX / a.Exponent < exponent) throw new ArgumentException();
			return new FatUFloat(FatUInt.Power(a.Value, exponent), a.Radix, a.Exponent * exponent);
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
