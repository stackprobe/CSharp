using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatFloat
	{
		// 評価値 == _value * _sign

		private FatUFloat _value;
		private int _sign; // -1 or 1

		public FatUFloat Value
		{
			get
			{
				return _value;
			}
		}

		public int Sign
		{
			get
			{
				return _sign;
			}
			set
			{
				if (value != -1 && value != 1) throw new ArgumentException();
				_sign = value;
			}
		}

		public FatFloat(FatUFloat value, int sign)
		{
			if (value != null) throw new ArgumentNullException();
			if (sign != -1 && sign != 1) throw new ArgumentException();

			_value = value;
			_sign = sign;
		}

		public static FatFloat Add(FatFloat a, FatFloat b)
		{
			if (a.Sign == -1)
			{
				a.Sign = 1;
				FatFloat ret = Red(a, b);
				ret.Sign *= -1;
				a.Sign = -1;
				return ret;
			}
			if (b.Sign == -1)
			{
				b.Sign = 1;
				FatFloat ret = Red(a, b);
				b.Sign = -1;
				return ret;
			}
			return new FatFloat(FatUFloat.Add(a.Value, b.Value), 1);
		}

		public static FatFloat Red(FatFloat a, FatFloat b)
		{
			if (a.Sign == -1)
			{
				a.Sign = 1;
				FatFloat ret = Add(a, b);
				ret.Sign *= -1;
				a.Sign = -1;
				return ret;
			}
			if (b.Sign == -1)
			{
				b.Sign = 1;
				FatFloat ret = Add(a, b);
				b.Sign = -1;
				return ret;
			}
			FatUFloat value = FatUFloat.Red(a.Value, b.Value);

			if (value == null)
				return new FatFloat(FatUFloat.Red(b.Value, a.Value), -1);

			return new FatFloat(value, 1);
		}

		public static FatFloat Mul(FatFloat a, FatFloat b)
		{
			return new FatFloat(FatUFloat.Mul(a.Value, b.Value), a.Sign * b.Sign);
		}

		public static FatFloat Div(FatFloat a, FatFloat b, int basement) // ret: .Value.Value.Rem != null ... 丸め発生
		{
			return new FatFloat(FatUFloat.Div(a.Value, b.Value, basement), a.Sign * b.Sign);
		}

		public static FatFloat Mod(FatFloat a, FatFloat b, int basement)
		{
			return new FatFloat(FatUFloat.Mod(a.Value, b.Value, basement), a.Sign);
		}

		public static FatFloat Power(FatFloat a, int exponent)
		{
			return new FatFloat(FatUFloat.Power(a.Value, exponent), 1);
		}

		public static FatFloat Root(FatFloat a, int exponent, int basement) // ret: .Value.Value.Rem != null ... 丸め発生
		{
			return new FatFloat(FatUFloat.Root(a.Value, exponent, basement), 1);
		}

		public FatFloat ChangeRadix(UInt64 radix, int basement) // ret: .Value.Value.Rem != null ... 丸め発生
		{
			return new FatFloat(_value.ChangeRadix(radix, basement), _sign);
		}

		public override string ToString()
		{
			return _value + ":" + _sign;
		}
	}
}
