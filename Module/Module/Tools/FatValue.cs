using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class FatValue
	{
		private List<UInt64> _figures = new List<UInt64>();
		private UInt64 _radix = Calc.DEF_RADIX; // 2 ～ 2^64-1
		private int _exponent = 0; // 0 ～ IMAX
		private int _sign = 1; // -1 or 1
		private bool _rem = false;

		private const string DIGIT_36 = StringTools.DIGIT + StringTools.alpha;

		public void SetString(string str, UInt64 radix = Calc.DEF_RADIX)
		{
			if (str == null) throw new ArgumentNullException();
			if (radix < 2) throw new ArgumentOutOfRangeException();

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
							//if ((UInt64.MaxValue - (UInt64)val) / 10 < value) throw new OverflowException();

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

			// normalize
			{
				int bgn = 0;
				int end = _figures.Count;

				while (0 < end && _figures[end - 1] == 0)
					end--;

				while (bgn < end && _figures[bgn] == 0 && 1 <= _exponent)
				{
					bgn++;
					_exponent--;
				}
				_figures = _figures.GetRange(bgn, end - bgn);

				if (_figures.Count == 0)
				{
					_exponent = 0;
					_sign = 1;
				}
			}
		}

		private void AddToFigures(UInt64 value, bool readDot)
		{
			if (_radix <= value)
				throw new OverflowException();

			_figures.Add(value);

			if (readDot)
				_exponent++;
		}

		public string GetString(int bracketMin = 36) // bracketMin: 0 ～ 36
		{
			if (bracketMin < 0 || 36 < bracketMin) throw new ArgumentOutOfRangeException();

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
					buff.Append('[' + value + ']');
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
					ret.EndsWith("[0]")
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
			if (src == null) throw new ArgumentNullException();

			UInt64 denom = RadixToDenom();
		}

		public FatFloat GetFatFloat()
		{
			throw null; // TODO
		}
	}
}
