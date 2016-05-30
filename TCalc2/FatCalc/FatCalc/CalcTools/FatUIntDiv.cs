using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.CalcTools
{
	public class FatUIntDiv
	{
		public static FatUInt Perform(FatUInt a, FatUInt b, FatUInt ret)
		{
			UInt8L numer = new UInt8L();
			UInt8L denom = new UInt8L();
			UInt8L ans = new UInt8L();

			numer.SetFatUInt(a);
			denom.SetFatUInt(b);

			if (numer.Count <= 8) throw null;
			if (denom.Count <= 4) throw null;

			Div(numer, denom, ans);

			ans.GetFatUInt(ret);

			if (1 <= numer.Count)
				ret.Rem = numer.GetFatUInt();

			return ret;
		}

		private static void Div(UInt8L numer, UInt8L denom, UInt8L ans)
		{
			int ni;
			int di;
			UInt64 n;
			UInt64 d;
			UInt64 a;
			UInt8L dml;

			di = denom.Count - 2;
			d = ((UInt64)denom[di + 0] << 0) |
				((UInt64)denom[di + 1] << 8);
			d++;

			while (denom.Count + 6 <= numer.Count)
			{
				ni = numer.Count - 8;
				n = ((UInt64)numer[ni + 0] << 0) |
					((UInt64)numer[ni + 1] << 8) |
					((UInt64)numer[ni + 2] << 16) |
					((UInt64)numer[ni + 3] << 24) |
					((UInt64)numer[ni + 4] << 32) |
					((UInt64)numer[ni + 5] << 40) |
					((UInt64)numer[ni + 6] << 48) |
					((UInt64)numer[ni + 7] << 56);

				a = n / d;

				Add(ans, a, ni - di);
				dml = Mul(denom, a);
				Red(numer, dml, ni - di);
			}
			while (denom.Count <= numer.Count)
			{
				ni = di;
				n = ((UInt64)numer[ni + 0] << 0) |
					((UInt64)numer[ni + 1] << 8) |
					((UInt64)numer[ni + 2] << 16) |
					((UInt64)numer[ni + 3] << 24) |
					((UInt64)numer[ni + 4] << 32) |
					((UInt64)numer[ni + 5] << 40) |
					((UInt64)numer[ni + 6] << 48) |
					((UInt64)numer[ni + 7] << 56);

				a = n / d;

				if (a == 0ul)
					break;

				Add(ans, a, 0);
				dml = Mul(denom, a);
				Red(numer, dml, 0);
			}
			while (Comp(denom, numer) <= 0)
			{
				Add(ans, 1ul, 0);
				Red(numer, denom, 0);
			}
		}

		private static void Add(UInt8L dest, UInt64 val, int index)
		{
			while (val != 0ul)
			{
				val += dest[index];
				dest[index] = (byte)(val & 0xfful);
				val >>= 8;
				index++;
			}
		}

		private static UInt8L Mul(UInt8L a, UInt64 b)
		{
			UInt8L dest = new UInt8L();
			int size = a.Count;

			for (int index = 0; index < size; index++)
			{
				UInt64 val = a[index] * b;
				int c = index;

				while (val != 0ul)
				{
					val += dest[c];
					dest[c] = (byte)(val & 0xfful);
					val >>= 8;
					c++;
				}
			}
			return dest;
		}

		private static void Red(UInt8L a, UInt8L b, int aPos)
		{
			uint val = 1;
			int size = b.Count;

			for (int index = 0; index < size; index++)
			{
				val += a[index + aPos];
				val += 0xffu - b[index];
				a[index + aPos] = (byte)(val & 0xffu);
				val >>= 8;
			}
			for (int index = aPos + b.Count; val == 0u; index++)
			{
				val += a[index];
				val += 0xffu;
				a[index] = (byte)(val & 0xffu);
				val >>= 8;
			}
		}

		private static int Comp(UInt8L a, UInt8L b)
		{
			if (a.Count < b.Count)
				return -1;

			if (b.Count < a.Count)
				return 1;

			for (int index = a.Count - 1; 0 <= index; index--)
			{
				if (a[index] < b[index])
					return -1;

				if (b[index] < a[index])
					return 1;
			}
			return 0;
		}

		private class UInt8L
		{
			private List<byte> _buff = new List<byte>();

			public byte this[int index]
			{
				set
				{
					while (_buff.Count <= index)
						_buff.Add((byte)0);

					_buff[index] = value;
				}
				get
				{
					return index < _buff.Count ? _buff[index] : (byte)0;
				}
			}

			public int Count
			{
				get
				{
					int size = _buff.Count;

					if (1 <= size && _buff[size - 1] == (byte)0)
					{
						do
						{
							size--;
						}
						while (1 <= size && _buff[size - 1] == (byte)0);

						_buff.RemoveRange(size, _buff.Count - size);
					}
					return size;
				}
			}

			public void SetFatUInt(FatUInt src)
			{
				byte[] dest = new byte[src.Figures.Count * 4];

				for (int index = 0; index < src.Figures.Count; index++)
				{
					dest[index * 4 + 0] = (byte)((src.Figures[index] >> 0) & 0xff);
					dest[index * 4 + 1] = (byte)((src.Figures[index] >> 8) & 0xff);
					dest[index * 4 + 2] = (byte)((src.Figures[index] >> 16) & 0xff);
					dest[index * 4 + 3] = (byte)((src.Figures[index] >> 24) & 0xff);
				}
				_buff = new List<byte>(dest);
			}

			public FatUInt GetFatUInt()
			{
				FatUInt dest = new FatUInt();
				this.GetFatUInt(dest);
				return dest;
			}

			public void GetFatUInt(FatUInt dest)
			{
				int size = this.Count;

				for (int index = 0; index + 4 <= size; index += 4)
				{
					dest.Figures.Add(
						((uint)_buff[index + 0] << 0) |
						((uint)_buff[index + 1] << 8) |
						((uint)_buff[index + 2] << 16) |
						((uint)_buff[index + 3] << 24)
						);
				}
				switch (size % 4)
				{
					case 0:
						break;
					case 1:
						dest.Figures.Add(
							(uint)_buff[size - 1]
							);
						break;
					case 2:
						dest.Figures.Add(
							((uint)_buff[size - 2] << 0) |
							((uint)_buff[size - 1] << 8)
							);
						break;
					case 3:
						dest.Figures.Add(
							((uint)_buff[size - 3] << 0) |
							((uint)_buff[size - 2] << 8) |
							((uint)_buff[size - 1] << 16)
							);
						break;
					default:
						throw null;
				}
			}

			public void Clear()
			{
				_buff.Clear();
			}
		}
	}
}
