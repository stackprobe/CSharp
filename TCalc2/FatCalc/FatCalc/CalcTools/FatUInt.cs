using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.CalcTools
{
	public class FatUInt
	{
		private List<uint> _figures = new List<uint>();
		public FatUInt Rem = null;

		public List<uint> Figures
		{
			get
			{
				return _figures;
			}
		}

		public FatUInt()
		{ }

		public FatUInt(uint value)
		{
			_figures.Add(value);
		}

		public FatUInt(UInt64 value)
		{
			_figures.Add((uint)(value & 0xfffffffful));
			_figures.Add((uint)(value >> 32));
		}

		public void Normalize()
		{
			int count = _figures.Count;

			if (1 <= count && _figures[count - 1] == 0)
			{
				do
				{
					count--;
				}
				while (1 <= count && _figures[count - 1] == 0);

				_figures.RemoveRange(count, _figures.Count - count);
			}
		}

		public void Extend(int count)
		{
			Extend(_figures, count);
		}

		public static void Extend(List<uint> buff, int count)
		{
			while (buff.Count < count)
				buff.Add(0u);
		}

		public FatUInt GetClone()
		{
			this.Normalize();

			FatUInt ret = new FatUInt();

			foreach (uint value in _figures)
				ret.Figures.Add(value);

			return ret;
		}

		public int GetFarthestBit() // ret: 1 ～ == ビット位置, 0 == 無し
		{
			this.Normalize();

			if (_figures.Count == 0)
				return 0;

			return (_figures.Count - 1) * 32 + GetFarthestBit(_figures[_figures.Count - 1]);
		}

		private static int GetFarthestBit(uint value) // ret: 1 ～ 32
		{
			for (int bit = 31; ; bit--)
				if ((value & (1u << bit)) != 0)
					return bit + 1;
		}

		public void SetBit_1(int bit) // bit: 0 ～ == ビット位置
		{
			if (bit < 0 || IntTools.IMAX < bit) throw new ArgumentException();

			int index = bit / 32;
			bit %= 32;

			this.Extend(index + 1);

			_figures[index] |= 1u << bit;
		}

		public void SetBit_0(int bit) // bit: 0 ～ == ビット位置
		{
			if (bit < 0 || IntTools.IMAX < bit) throw new ArgumentException();

			int index = bit / 32;
			bit %= 32;

			if (_figures.Count <= index)
				return;

			_figures[index] &= ~(1u << bit);
		}

		public UInt64 GetValue64()
		{
			this.Normalize();

			switch (_figures.Count)
			{
				case 0: return 0;
				case 1: return _figures[0];
				case 2: return _figures[0] | ((UInt64)_figures[1] << 32);

				default:
					throw null;
			}
		}

		public void SetValue64(UInt64 value)
		{
			this.Extend(2);

			_figures[0] = (uint)(value & 0xfffffffful);
			_figures[1] = (uint)(value >> 32);
		}

		public static FatUInt Add(FatUInt a, FatUInt b)
		{
			a.Normalize();
			b.Normalize();

			FatUInt ret = new FatUInt();
			UInt64 val = 0;
			int end = Math.Min(a.Figures.Count, b.Figures.Count);
			int index;

			for (index = 0; index < end; index++)
			{
				val += a.Figures[index];
				val += b.Figures[index];
				ret.Figures.Add((uint)(val & 0xfffffffful));
				val >>= 32;
			}
			if (end < b.Figures.Count)
				a = b;

			for (; index < a.Figures.Count && val != 0; index++)
			{
				val += a.Figures[index];
				ret.Figures.Add((uint)(val & 0xfffffffful));
				val >>= 32;
			}
			for (; index < a.Figures.Count; index++)
				ret.Figures.Add(a.Figures[index]);

			if (val != 0)
				ret.Figures.Add(1u);

			return ret;
		}

		public static FatUInt Red(FatUInt a, FatUInt b) // ret: null ... < 0
		{
			a.Normalize();
			b.Normalize();

			if (a.Figures.Count < b.Figures.Count)
				return null;

			FatUInt ret = new FatUInt();
			UInt64 val = 1;
			int index;

			for (index = 0; index < b.Figures.Count; index++)
			{
				val += a.Figures[index];
				val += ~b.Figures[index];
				ret.Figures.Add((uint)(val & 0xfffffffful));
				val >>= 32;
			}
			if (val == 0)
			{
				for (; ; index++)
				{
					if (a.Figures.Count <= index)
						return null;

					if (a.Figures[index] != 0)
					{
						ret.Figures.Add(a.Figures[index] - 1);
						index++;
						break;
					}
					ret.Figures.Add(0xffffffffu);
				}
			}
			for (; index < a.Figures.Count; index++)
				ret.Figures.Add(a.Figures[index]);

			return ret;
		}

		public static FatUInt Mul(FatUInt a, FatUInt b)
		{
			a.Normalize();
			b.Normalize();

			FatUInt ret = new FatUInt();
			ret.Extend(a.Figures.Count + b.Figures.Count);

			for (int ai = 0; ai < a.Figures.Count; ai++)
			{
				for (int bi = 0; bi < b.Figures.Count; bi++)
				{
					UInt64 val = (UInt64)a.Figures[ai] * b.Figures[bi];
					int index = ai + bi;

					while (val != 0)
					{
						val += ret.Figures[index];
						ret.Figures[index] = (uint)(val & 0xfffffffful);
						val >>= 32;
						index++;
					}
				}
			}
			ret.Normalize(); // 1 * 1 など、最上位(終端)が 0 になる場合がある。
			return ret;
		}

		public static FatUInt Div(FatUInt a, FatUInt b) // ret: .Rem == null ... 余りなし
		{
			a.Normalize();
			b.Normalize();

			if (b.Figures.Count == 0)
				throw new DivideByZeroException();

			if (a.Figures.Count == 0)
				return new FatUInt();

			FatUInt ret = new FatUInt();

			if (a.Figures.Count < b.Figures.Count)
			{
				ret.Rem = a.GetClone();
				return ret;
			}
			if (a.Figures.Count <= 2)
			{
				UInt64 numer = a.GetValue64();
				UInt64 denom = b.GetValue64();
				UInt64 ans = numer / denom;
				UInt64 rem = numer % denom;

				ret.SetValue64(ans);

				if (rem != 0ul)
					ret.Rem = new FatUInt(rem);

				return ret;
			}
			if (b.Figures.Count == 1)
			{
				UInt64 numer = a.Figures[a.Figures.Count - 1];
				UInt64 denom = b.Figures[0];

				ret.Extend(a.Figures.Count);

				for (int index = a.Figures.Count - 2; 0 <= index; index--)
				{
					numer <<= 32;
					numer |= a.Figures[index];

					UInt64 val = numer / denom;

					for (int c = index; val != 0ul; c++)
					{
						val += ret.Figures[c];
						ret.Figures[c] = (uint)(val & 0xfffffffful);
						val >>= 32;
					}
					numer %= denom;
				}
				if (numer != 0ul)
					ret.Rem = new FatUInt(numer);

				return ret;
			}

			if (Gnd.MsdnDivFlag)
				FatUIntDivMS.Div(a, b, ret);
			else
				FatUIntDiv.Perform(a, b, ret);

			if (Gnd.DebugMode)
			{
				FatUInt r1 = new FatUInt();
				FatUInt r2 = new FatUInt();

				Logger.WriteLine("FatUIntDivMS.Div Before");
				FatUIntDivMS.Div(a, b, r1);
				Logger.WriteLine("FatUIntDiv.Perform Before");
				FatUIntDiv.Perform(a, b, r2);
				Logger.WriteLine("FatUIntDiv.Perform After");

				if (FatUInt.Red(r1, r2).IsZero() == false)
					throw null;

				if (r1.Rem != null || r2.Rem != null)
				{
					if (r1.Rem == null || r2.Rem == null)
						throw null;

					if (FatUInt.Red(r1.Rem, r2.Rem).IsZero() == false)
						throw null;
				}
			}

			return ret;
		}

		public static FatUInt Mod(FatUInt a, FatUInt b)
		{
			FatUInt ret = Div(a, b).Rem;

			if (ret == null)
				ret = new FatUInt();

			return ret;
		}

		public override string ToString()
		{
			StringBuilder buff = new StringBuilder();

			buff.Append("[");

			if (1 <= _figures.Count)
			{
				buff.Append(_figures[_figures.Count - 1].ToString("x8"));

				for (int index = _figures.Count - 2; 0 <= index; index--)
				{
					buff.Append(":");
					buff.Append(_figures[index].ToString("x8"));
				}
			}
			buff.Append("]");
			return buff.ToString();
		}

		public static FatUInt Power(FatUInt a, int exponent)
		{
			if (exponent < 0 || IntTools.IMAX < exponent) throw new ArgumentException();

			if (exponent == 0)
				return new FatUInt(1);

			if (exponent == 1)
				return a.GetClone();

			if (exponent == 2)
				return Mul(a, a);

			FatUInt ret = Power(a, exponent / 2);

			if (exponent % 2 == 1)
				ret = Mul(ret, Mul(ret, a));
			else
				ret = Mul(ret, ret);

			return ret;
		}

		public static FatUInt Root(FatUInt a, int exponent)
		{
			if (exponent < 1 || IntTools.IMAX < exponent) throw new ArgumentException();

			if (exponent == 1)
				return a.GetClone();

			int bit = a.GetFarthestBit();
			bit /= 2;
			bit += 5; // XXX マージン適当

			FatUInt ret = new FatUInt();

			for (; 0 <= bit; bit--)
			{
				ret.SetBit_1(bit);
				FatUInt t = Power(ret, exponent);

				if (Red(a, t) == null)
					ret.SetBit_0(bit);
			}
			ret.Normalize();
			return ret;
		}

		public bool IsZero()
		{
			this.Normalize();
			return _figures.Count == 0;
		}
	}
}
