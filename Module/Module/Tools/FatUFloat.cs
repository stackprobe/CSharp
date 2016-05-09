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

		public FatUFloat Rem = null;

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

		public FatUFloat ChangeExponent(int exponentNew)
		{
			if (exponentNew < 0 || IntTools.IMAX < exponentNew) throw new ArgumentOutOfRangeException();

			int e = exponentNew - _exponent;
			FatUInt value;

			if (e < 0)
				value = FatUInt.Div(_value, FatUInt.Power(new FatUInt(_radix), -e));
			else if (0 < e)
				value = FatUInt.Mul(_value, FatUInt.Power(new FatUInt(_radix), e));
			else
				value = _value.GetClone();

			return new FatUFloat(value, _radix, exponentNew);
		}

		private static void Synchronize(ref FatUFloat a, ref FatUFloat b, int basement = 0)
		{
			if (a == null) throw new ArgumentException();
			if (b == null) throw new ArgumentException();
			if (a.Radix != b.Radix) throw new ArgumentException("different radix");
			if (basement < 0 || IntTools.IMAX < basement) throw new ArgumentOutOfRangeException();

			int e = a.Exponent - b.Exponent - basement;

			if (e < 0)
				a = a.ChangeExponent(a.Exponent - e);
			else if (0 < e)
				b = b.ChangeExponent(b.Exponent + e);
		}

		public static FatUFloat Add(FatUFloat a, FatUFloat b)
		{
			Synchronize(ref a, ref b);
			return new FatUFloat(FatUInt.Add(a.Value, b.Value), a.Radix, a.Exponent);
		}

		public static FatUFloat Red(FatUFloat a, FatUFloat b)
		{
			Synchronize(ref a, ref b);
			return new FatUFloat(FatUInt.Red(a.Value, b.Value), a.Radix, a.Exponent);
		}

		public static FatUFloat Mul(FatUFloat a, FatUFloat b)
		{
			if (IntTools.IMAX < a.Exponent + b.Exponent) throw new ArgumentException();
			return new FatUFloat(FatUInt.Mul(a.Value, b.Value), a.Radix, a.Exponent + b.Exponent);
		}

		public static FatUFloat Div(FatUFloat a, FatUFloat b, int basement = DEF_BASEMENT)
		{
			Synchronize(ref a, ref b, basement);
			FatUInt value = FatUInt.Div(a.Value, b.Value);
			FatUFloat ret = new FatUFloat(value, a.Radix, basement);

			if (value.Rem != null)
				ret.Rem = new FatUFloat(value.Rem, a.Radix, basement);

			return ret;
		}

		public static FatUFloat Mod(FatUFloat a, FatUFloat b, int basement = DEF_BASEMENT)
		{
			return Div(a, b, basement).Rem;
		}

		public static FatUFloat Power(FatUFloat a, int exponent)
		{
			if (exponent < 0 || IntTools.IMAX / a.Exponent < exponent) throw new ArgumentOutOfRangeException();
			return new FatUFloat(FatUInt.Power(a.Value, exponent), a.Radix, a.Exponent * exponent);
		}

		public static FatUFloat Root(FatUFloat a, int exponent, int basement = DEF_BASEMENT)
		{
			if (a == null) throw new ArgumentException();
			if (exponent < 0 || IntTools.IMAX < exponent) throw new ArgumentOutOfRangeException();
			if (basement < 0 || IntTools.IMAX / exponent < basement) throw new ArgumentOutOfRangeException();

			FatUFloat b = a.GetClone();
			b.Exponent = exponent * basement;
			FatUFloat ret = new FatUFloat(FatUInt.Root(b.Value, exponent), a.Radix, basement);
			FatUFloat rem = FatUFloat.Red(a, Power(ret, exponent));

			if (rem.Value.IsZero() == false)
				ret.Rem = rem;

			return ret;
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
