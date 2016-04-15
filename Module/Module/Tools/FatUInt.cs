using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatUInt
	{
		private List<uint> _figures = new List<uint>();
		public FatUInt Rem = null;

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

		public List<uint> Figures
		{
			get
			{
				return _figures;
			}
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

		public FatUInt GetClone()
		{
			this.Normalize();

			FatUInt ret = new FatUInt();

			foreach (uint value in _figures)
				ret.Figures.Add(value);

			return ret;
		}

		public void Shift(int bit)
		{
			if (bit < -IntTools.IMAX || IntTools.IMAX < bit) throw new ArgumentException();

			this.Normalize();

			if (_figures.Count == 0)
				return;

			List<uint> buff = new List<uint>();

			if (bit < 0)
			{
				bit = -bit;

				if (this.GetFarthestBit() <= bit)
				{
					_figures.Clear();
					return;
				}
				int index = bit / 32;
				bit %= 32;

				if (bit == 0)
				{
					for (; index < _figures.Count; index++)
						buff.Add(_figures[index]);
				}
				else
				{
					for (; index + 1 < _figures.Count; index++)
						buff.Add((_figures[index] >> bit) | (_figures[index + 1] << (32 - bit)));

					buff.Add(_figures[index] >> bit);
				}
			}
			else
			{
				int index = bit / 32;
				bit %= 32;

				while (buff.Count < index)
					buff.Add(0u);

				if (bit == 0)
				{
					for (index = 0; index < _figures.Count; index++)
						buff.Add(_figures[index]);
				}
				else
				{
					buff.Add(_figures[0] << bit);

					for (index = 0; index + 1 < _figures.Count; index++)
						buff.Add((_figures[index] >> (32 - bit)) | (_figures[index + 1] << bit));

					buff.Add(_figures[index] >> (32 - bit));
				}
			}
			_figures = buff;
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

			while (_figures.Count <= index)
				_figures.Add(0u);

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

		public static FatUInt Red(FatUInt a, FatUInt b)
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

			while (ret.Figures.Count < a.Figures.Count + b.Figures.Count)
				ret.Figures.Add(0u);

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
			ret.Normalize(); // 1 x 1 など、最上位(終端)が 0 になる場合がある。
			return ret;
		}

		public static FatUInt Div(FatUInt a, FatUInt b)
		{
			a.Normalize();
			b.Normalize();

			if (b.Figures.Count == 0)
				throw new DivideByZeroException();

			int af = a.GetFarthestBit();
			int bf = b.GetFarthestBit();

			FatUInt ret = new FatUInt();

			if (af < bf)
			{
				ret.Rem = a.GetClone();
				return ret;
			}
			int diff = af - bf;

			b = b.GetClone();
			b.Shift(diff);

			for (; ; )
			{
				FatUInt t = Red(a, b);

				if (t != null)
				{
					a = t;
					ret.SetBit_1(diff);

					af = a.GetFarthestBit();
					int d = bf - af;

					if (diff < d)
						break;

					b.Shift(-d);
					diff -= d;
				}
				else
				{
					if (diff < 1)
						break;

					b.Shift(-1);
					diff--;
				}
			}
			ret.Rem = a.GetClone();
			return ret;
		}

		public static FatUInt Mod(FatUInt a, FatUInt b)
		{
			return Div(a, b).Rem;
		}

		/// <summary>
		/// 10^19
		/// </summary>
		private const UInt64 Z19 = 0x8ac7230489e80000ul;

		private static FatUInt FromZ19(List<UInt64> src)
		{
			FatUInt ret = new FatUInt();
			FatUInt b = new FatUInt(Z19);

			if (1 <= src.Count)
			{
				ret = Add(ret, new FatUInt(src[src.Count - 1]));

				for (int index = src.Count - 2; 0 <= index; index--)
				{
					ret = Mul(ret, b);
					ret = Add(ret, new FatUInt(src[index]));
				}
			}
			ret.Normalize();
			return ret;
		}

		private static List<UInt64> ToZ19(FatUInt src)
		{
			src.Normalize();

			List<UInt64> ret = new List<UInt64>();
			FatUInt b = new FatUInt(Z19);

			while (1 <= src.Figures.Count)
			{
				src = Div(src, b);
				ret.Add(src.Rem.GetValue64());
			}
			return ret;
		}

		public static FatUInt FromString(string src)
		{
			src = StringTools.ReplaceChar(src, StringTools.ZEN_DIGIT, StringTools.DIGIT);

			List<UInt64> buff = new List<UInt64>();
			UInt64 value = 0;
			UInt64 scale = 1;

			for (int index = src.Length - 1; 0 <= index; index--)
			{
				int val = StringTools.DIGIT.IndexOf(src[index]);

				if (val != -1)
				{
					if (scale == 10000000000000000000ul)
					{
						buff.Add(value);
						value = (UInt64)val;
						scale = 10;
					}
					else
					{
						value += (UInt64)val * scale;
						scale *= 10;
					}
				}
			}
			buff.Add(value);
			return FromZ19(buff);
		}

		public string GetString()
		{
			StringBuilder buff = new StringBuilder();
			List<UInt64> src = ToZ19(this);

			if (1 <= src.Count)
			{
				buff.Append(src[src.Count - 1].ToString());

				for (int index = src.Count - 2; 0 <= index; index--)
					buff.Append(src[index].ToString("D19"));
			}
			else
				buff.Append('0');

			return buff.ToString();
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
