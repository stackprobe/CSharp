using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.CalcTools
{
	public class FatUFloat
	{
		// 評価値 == _value / (_radix ^ _exponent)

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

		public FatUFloat(FatUInt value, UInt64 radix, int exponent)
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

		public FatUFloat ChangeExponent(int exponentNew) // ret: .Value.Rem != null ... 丸め発生
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
			if (a.Radix != b.Radix) throw new ArgumentException("diff radix");
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

		public static FatUFloat Red(FatUFloat a, FatUFloat b) // ret: null ... < 0
		{
			Synchronize(ref a, ref b);
			FatUInt value = FatUInt.Red(a.Value, b.Value);

			if (value == null)
				return null;

			return new FatUFloat(value, a.Radix, a.Exponent);
		}

		public static FatUFloat Mul(FatUFloat a, FatUFloat b)
		{
			if (IntTools.IMAX < a.Exponent + b.Exponent) throw new ArgumentException();
			return new FatUFloat(FatUInt.Mul(a.Value, b.Value), a.Radix, a.Exponent + b.Exponent);
		}

		public static FatUFloat Div(FatUFloat a, FatUFloat b, int basement) // ret: .Value.Rem != null ... 丸め発生
		{
			Synchronize(ref a, ref b, basement);
			return new FatUFloat(FatUInt.Div(a.Value, b.Value), a.Radix, basement);
		}

		public static FatUFloat Mod(FatUFloat a, FatUFloat b, int basement)
		{
			if (a == null) throw new ArgumentException();
			if (b == null) throw new ArgumentException();
			if (basement < 0 || IntTools.IMAX < basement) throw new ArgumentOutOfRangeException();

			return Red(a, Mul(Div(a, b, basement), b));
		}

		public static FatUFloat Power(FatUFloat a, int exponent)
		{
			if (a == null) throw new ArgumentException();

			if (exponent == 0)
				return new FatUFloat(new FatUInt(1), a.Radix, 0);

			if (exponent < 1 || IntTools.IMAX / exponent < a.Exponent) throw new ArgumentOutOfRangeException();

			return new FatUFloat(FatUInt.Power(a.Value, exponent), a.Radix, a.Exponent * exponent);
		}

		public static FatUFloat Root(FatUFloat a, int exponent, int basement) // ret: .Value.Rem != null ... 丸め発生
		{
			if (a == null) throw new ArgumentException();
			if (exponent < 1 || IntTools.IMAX < exponent) throw new ArgumentOutOfRangeException();
			if (basement < 0 || IntTools.IMAX / exponent < basement) throw new ArgumentOutOfRangeException();

			FatUFloat ret = new FatUFloat(FatUInt.Root(a.ChangeExponent(exponent * basement).Value, exponent), a.Radix, basement);
			FatUFloat rem = Red(a, Power(ret, exponent));

			if (rem.Value.IsZero() == false)
				ret.Value.Rem = rem.Value;

			return ret;
		}

		public FatUFloat ChangeRadix(UInt64 radixNew, int basement) // ret: .Value.Rem != null ... 丸め発生
		{
			if (radixNew < 2) throw new ArgumentOutOfRangeException();
			if (basement < 0 || IntTools.IMAX < basement) throw new ArgumentOutOfRangeException();

			FatUInt value = _value;
			value = FatUInt.Mul(value, FatUInt.Power(new FatUInt(radixNew), basement));
			value = FatUInt.Div(value, FatUInt.Power(new FatUInt(_radix), _exponent));

			return new FatUFloat(value, radixNew, basement);
		}

		public override string ToString()
		{
			return _value.ToString() + ":" + _radix + ":" + _exponent;
		}
	}
}
