using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatUInt
	{
		private List<uint> _figures = new List<uint>();

		public FatUInt()
		{ }

		public FatUInt(uint value)
		{
			_figures.Add(value);
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

			List<uint> buff = new List<uint>();

			if (bit < 0)
			{
				bit = -bit;
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
				}
			}
			_figures = buff;
		}

		public int GetFarthestBit()
		{
			this.Normalize();

			if (_figures.Count == 0)
				return 0;

			return (_figures.Count - 1) * 32 + GetFarthestBit(_figures[_figures.Count - 1]);
		}

		private static int GetFarthestBit(uint value)
		{
			for (int bit = 31; ; bit--)
				if ((value & (1u << bit)) != 0)
					return bit;
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

			if (a._figures.Count < b._figures.Count)
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
				return ret; // return 0;

			a = a.GetClone();
			b = b.GetClone();

			throw null; // TODO
		}
	}
}
