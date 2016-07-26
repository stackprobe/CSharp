using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.CalcTools
{
	public class FatValue
	{
		private List<UInt64> _figures = new List<UInt64>();
		private UInt64 _radix = Calc.DEF_RADIX; // 2 ～ 2^64-1
		private int _exponent = 0; // 0 ～ IMAX
		private int _sign = 1; // -1 or 1
		private bool _rem = false;

		private const string DIGIT_36 = StringTools.DIGIT + StringTools.alpha;

		public void SetString(string str, UInt64 radix = Calc.DEF_RADIX) // 丸め記号は無視する！
		{
			if (str == null) throw new ArgumentNullException();
			if (radix < 2) throw new ArgumentOutOfRangeException();

			BusyDlg.StatusBox.Post("表示可能な形式をセットしています。"); // app

			// init
			{
				_figures.Clear();
				_radix = radix;
				_exponent = 0;
				_sign = 1;
				_rem = false;
			}

			bool readDot = false;

			for (int index = 0; index < str.Length; index++)
			{
				char chr = str[index];

				if (chr == '-')
				{
					_sign = -1;
				}
				else if (chr == '.')
				{
					readDot = true;
				}
				else if (chr == '[')
				{
					UInt64 value = 0;

					for (; ; )
					{
						chr = str[++index];

						if (chr == ']')
							break;

						int val = StringTools.DIGIT.IndexOf(chr);

						if (val != -1)
						{
							if ((UInt64.MaxValue - (UInt64)val) / 10 < value) throw new OverflowException("角括弧表記の最大値を超えました。");

							value *= 10;
							value += (UInt64)val;
						}
					}
					AddToFigures(value, readDot);
				}
				else
				{
					chr = char.ToLower(chr);
					int val = DIGIT_36.IndexOf(chr);

					if (val != -1)
					{
						AddToFigures((UInt64)val, readDot);
					}
				}
			}
			_figures.Reverse();
			Normalize();
		}

		private void AddToFigures(UInt64 value, bool readDot)
		{
			if (_radix <= value)
				throw new OverflowException("基数以上の数字を読み込みました。");

			_figures.Add(value);

			if (readDot)
				_exponent++;
		}

		private void Normalize()
		{
			int bgn = 0;
			int end = _figures.Count;

			while (0 < end && _figures[end - 1] == 0)
				end--;

			if (_rem == false)
			{
				while (bgn < end && _figures[bgn] == 0 && 1 <= _exponent)
				{
					bgn++;
					_exponent--;
				}
			}
			_figures = _figures.GetRange(bgn, end - bgn);

			if (_figures.Count == 0)
			{
				_exponent = 0;
				_sign = 1;
			}
		}

		public string GetString(int bracketMin = Calc.DEF_BRACKET_MIN) // bracketMin: 0 ～ 36
		{
			if (bracketMin < 0 || 36 < bracketMin) throw new ArgumentOutOfRangeException();

			BusyDlg.StatusBox.Post("表示可能な形式を取得しています。"); // app

			StringBuilder buff = new StringBuilder();

			if (_rem)
				buff.Append('*');

			int fEnd = Math.Max(_figures.Count, _exponent + 1);

			for (int index = 0; index < fEnd; index++)
			{
				if (index == _exponent)
					buff.Append('.');

				UInt64 value;

				if (index < _figures.Count)
					value = _figures[index];
				else
					value = 0;

				if (value < (UInt64)bracketMin)
					buff.Append(DIGIT_36[(int)value]);
				else
					buff.Append(StringTools.Reverse("[" + value + "]"));
			}
			if (_sign == -1)
				buff.Append('-');

			string ret = buff.ToString();
			ret = StringTools.Reverse(ret);

			// normalize
			{
				if (
					ret.StartsWith("0") ||
					ret.StartsWith("[0]") ||
					ret.StartsWith("-0") ||
					ret.StartsWith("-[0]")
					)
				{
					int bgn = 0;

					if (ret[0] == '-')
						bgn = 1;

					int end = bgn;

					for (; ; )
					{
						if (ret[end] == '0' && ret[end + 1] != '.')
						{
							end++;
							continue;
						}
						if (
							ret[end] == '[' &&
							ret[end + 1] == '0' &&
							ret[end + 2] == ']' &&
							ret[end + 3] != '.'
							)
						{
							end += 3;
							continue;
						}
						break;
					}
					ret = StringTools.Remove(ret, bgn, end - bgn);
				}
				if (
					ret.EndsWith("0") ||
					ret.EndsWith("[0]") ||
					ret.EndsWith(".")
					)
				{
					int end = ret.Length;

					for (; ; )
					{
						if (ret[end - 1] == '0')
						{
							end--;
							continue;
						}
						if (
							ret[end - 1] == ']' &&
							ret[end - 2] == '0' &&
							ret[end - 3] == '['
							)
						{
							end -= 3;
							continue;
						}
						break;
					}
					if (ret[end - 1] == '.')
						end--;

					ret = ret.Substring(0, end);
				}
			}

			return ret;
		}

		public void SetFatFloat(FatFloat src)
		{
			// XXX 遅い！ -- 改善した。様子見 @ 2016.5.21 -- 元に戻してみたけど、遅かった。@ 2016.5.30
			// -- 2^(2^n) 進数のときは速く。@ 2016.7.25

			if (src == null) throw new ArgumentNullException();

			BusyDlg.StatusBox.Post("計算可能な形式をセットしています。\nこの処理は 2, 4, 16, 256, 65536, 4294967296 進数以外のとき時間が掛かります。"); // app

			// init
			{
				_figures.Clear();
				_radix = src.Value.Radix;
				_exponent = src.Value.Exponent;
				_sign = src.Sign;
				_rem = src.Value.Value.Rem != null;
			}

			UInt64 d;
			int z;
			//MkDZ(out d, out z);

			if (_radix == (_radix & (~_radix + 1) & 0x0000000100010116ul)) // ? 2^(2^n) 進数
			{
				for (int index = 0; index < src.Value.Value.Figures.Count; index++)
				{
					UInt64 val = src.Value.Value.Figures[index];

					for (UInt64 bit = 1; bit < (1ul << 32); bit *= _radix)
					{
						_figures.Add((val & (bit * (_radix - 1))) / bit);
					}
				}
			}
			else if (Gnd.SffBinaryMode)
			{
				MkDZ(out d, out z);

				List<FatUInt> denoms = new List<FatUInt>();
				FatUInt denom = new FatUInt(d);

				do
				{
					denoms.Add(denom);
					denom = FatUInt.Mul(denom, denom);
				}
				while (denom.GetFarthestBit() <= src.Value.Value.GetFarthestBit());

				new SetToFigures()
				{
					Dest = _figures,
					Denoms = denoms,
					Z = z,
					Radix = _radix,
				}
				.Perform(src.Value.Value, denoms.Count - 1);
			}
			else
			{
				MkDZ(out d, out z, _radix <= uint.MaxValue ? uint.MaxValue : UInt64.MaxValue);

				FatUInt numer = src.Value.Value;
				FatUInt denom = new FatUInt(d);

				while (numer.IsZero() == false)
				{
					numer = FatUInt.Div(numer, denom);
					UInt64 val = numer.Rem != null ? numer.Rem.GetValue64() : 0ul;

					for (int c = 0; c < z; c++)
					{
						_figures.Add(val % _radix);
						val /= _radix;
					}
				}
			}

			Normalize();
		}

		private class SetToFigures
		{
			public List<UInt64> Dest;
			public List<FatUInt> Denoms;
			public int Z;
			public UInt64 Radix;

			public void Perform(FatUInt value, int index)
			{
				if (index < 0)
				{
					UInt64 val = value.GetValue64();

					for (int c = 0; c < Z; c++)
					{
						Dest.Add(val % Radix);
						val /= Radix;
					}
					if (val != 0) throw null; // test
				}
				else
				{
					FatUInt hv = FatUInt.Div(value, Denoms[index]);
					FatUInt lv = hv.Rem != null ? hv.Rem : new FatUInt();

					Perform(lv, index - 1);
					Perform(hv, index - 1);
				}
			}
		}

		public FatFloat GetFatFloat() // _rem は無視する！
		{
			BusyDlg.StatusBox.Post("計算可能な形式を取得しています。"); // app

			FatUInt value = new FatUInt();

			if (_radix == (_radix & (~_radix + 1) & 0x0000000100010116ul)) // ? 2^(2^n) 進数
			{
				for (int index = 0; index < _figures.Count; )
				{
					UInt64 val = 0ul;

					for (UInt64 bit = 1; bit < (1ul << 32) && index < _figures.Count; bit *= _radix)
					{
						val += _figures[index++] * bit;
					}
					value.Figures.Add((uint)val);
				}
			}
			else
			{
				UInt64 d;
				int z;
				MkDZ(out d, out z);

				FatUInt denom = new FatUInt(d);

				for (int index = _figures.Count; 0 < index; )
				{
					UInt64 val = 0;

					do
					{
						val *= _radix;
						val += _figures[--index];
					}
					while (index % z != 0);

					value = FatUInt.Mul(value, denom);
					value = FatUInt.Add(value, new FatUInt(val));
				}
			}

			// 面倒なので normalize しない。ていうか多分必要無い。

			return new FatFloat(new FatUFloat(value, _radix, _exponent), _sign);
		}

		private void MkDZ(out UInt64 d, out int z, UInt64 maxval = UInt64.MaxValue)
		{
			if (maxval < _radix) throw null;

			d = _radix;
			z = 1;

			while (d <= maxval / _radix)
			{
				d *= _radix;
				z++;
			}
		}

		public static FatFloat GetFatFloat(string str, UInt64 radix = Calc.DEF_RADIX)
		{
			FatValue mid = new FatValue();
			mid.SetString(str, radix);
			return mid.GetFatFloat();
		}

		public static string GetString(FatFloat src, int bracketMin = Calc.DEF_BRACKET_MIN)
		{
			FatValue mid = new FatValue();
			mid.SetFatFloat(src);
			return mid.GetString(bracketMin);
		}
	}
}
