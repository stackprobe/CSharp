using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.TCalcs
{
	public class FatConverter
	{
		public int Radix;
		public int Rdx;
		public int RdxW;

		public FatConverter(int radix)
		{
			this.Radix = radix;
			this.Rdx = radix;
			this.RdxW = 1;

			while (this.Rdx <= IntTools.IMAX / radix)
			{
				this.Rdx *= radix;
				this.RdxW++;
			}
		}

		public static readonly string DIGIT_36 = StringTools.DECIMAL + StringTools.alpha;

		public List<int> Figures = new List<int>();
		public int Exponent = 0;
		public int Sign = 1;
		public bool Remained = false;

		public void SetString(string operand)
		{
			if (operand == null)
			{
				throw new ArgumentException("Bad operand: " + operand);
			}

			// init
			{
				this.Figures.Clear();
				this.Exponent = 0;
				this.Sign = 1;
				this.Remained = false;
			}

			bool decimalPointPassed = false;

			for (int index = 0; index < operand.Length; index++)
			{
				char chr = operand[index];

				if (chr == '-')
				{
					this.Sign = -1;
				}
				else if (chr == '.')
				{
					decimalPointPassed = true;
				}
				else if (chr == '[')
				{
					int value = 0;

					for (; ; )
					{
						chr = operand[++index];

						if (chr == ']')
						{
							break;
						}
						int val = StringTools.DECIMAL.IndexOf(chr);

						if (val == -1)
						{
							throw new ArgumentException("Bad character: " + chr);
						}
						if ((long)this.Radix <= value * 10L + val)
						{
							throw new ArgumentException("Overflow: " + value + ", " + val + ", " + this.Radix);
						}
						value *= 10;
						value += val;
					}
					this.AddToFigures(value, decimalPointPassed);
				}
				else
				{
					chr = char.ToLower(chr);
					int val = DIGIT_36.IndexOf(chr);

					if (val == -1)
						throw new ArgumentException("Bad character: " + chr);

					this.AddToFigures(val, decimalPointPassed);
				}
			}
			this.Figures.Reverse();
			this.Normalize();
		}

		private void AddToFigures(int value, bool decimalPointPassed)
		{
			if (this.Radix <= value)
			{
				throw new ArgumentException("Overflow: " + value + ", " + Radix);
			}
			this.Figures.Add(value);

			if (decimalPointPassed)
			{
				this.Exponent--;
			}
		}

		private void Normalize()
		{
			int start;

			for (start = 0; start < this.Figures.Count && this.Figures[start] == 0; start++)
			{ }

			if (start < this.Figures.Count)
			{
				int end;

				for (end = this.Figures.Count; this.Figures[end - 1] == 0; end--)
				{ }

				if (0 < start || end < this.Figures.Count)
				{
					this.Figures = new List<int>(TCalcArrays.CopyOfRange(this.Figures.ToArray(), start, end));
					this.Exponent += start;
				}
			}
			else
			{
				this.Figures.Clear();
				this.Exponent = 0;

				if (this.Remained == false)
				{
					Sign = 1;
				}
			}
		}

		public string GetString(int basement)
		{
			Normalize();

			StringBuilder buff = new StringBuilder();

			int start = Math.Min(0, -Exponent);
			int end = Math.Max(Figures.Count, -Exponent + 1);

			if (this.Remained)
			{
				buff.Append('*');

				if (basement == -1)
				{
					throw null; // never
				}
				start = -this.Exponent - basement;
			}
			for (int index = start; index < end; index++)
			{
				if (index == -this.Exponent)
				{
					buff.Append('.');
				}
				int value;

				if (0 <= index && index < this.Figures.Count)
				{
					value = this.Figures[index];
				}
				else
				{
					value = 0;
				}

				if (value < 36)
				{
					buff.Append(DIGIT_36[value]);
				}
				else
				{
					buff.Append(("[" + value + "]").Reverse().ToArray());
				}
			}
			if (Sign == -1)
			{
				buff.Append('-');
			}
			string ret = buff.ToString();
			ret = new string(ret.Reverse().ToArray());

			// normalize
			{
				if (ret.StartsWith("0"))
				{
					int index;

					for (index = 0; ret[index] == '0' && ret[index + 1] != '.'; index++)
					{ }

					ret = ret.Substring(index);
				}
				else if (ret.StartsWith("-0"))
				{
					int index;

					for (index = 1; ret[index] == '0' && ret[index + 1] != '.'; index++)
					{ }

					ret = "-" + ret.Substring(index);
				}

				if (ret.EndsWith("0") || ret.EndsWith("."))
				{
					int index;

					for (index = ret.Length; ret[index - 1] == '0'; index--)
					{ }

					if (ret[index - 1] == '.')
						index--;

					ret = ret.Substring(0, index);
				}
			}

			return ret;
		}

		public void SetFloat(FatFloat a)
		{
			this.Figures.Clear();

			for (int index = 0; index < a.Inner.Inner.Figures.Length; index++)
			{
				int value = a.Inner.Inner.Figures[index];

				for (int ndx = 0; ndx < RdxW; ndx++)
				{
					this.Figures.Add(value % Radix);
					value /= Radix;
				}
			}
			this.Exponent = a.Inner.Exponent * this.RdxW;
			this.Sign = a.Sign;
			this.Remained = a.Inner.Inner.Remained;

			this.Normalize();
		}

		public FatFloat GetFloat()
		{
			this.Normalize();

			// zantei
			{
				int a;
				int z;

				a = this.Exponent % this.RdxW;
				a += this.RdxW;
				a %= this.RdxW;

				this.Figures = new List<int>(TCalcArrays.Shift(this.Figures.ToArray(), a));
				this.Exponent -= a;

				z = this.Figures.Count % this.RdxW;
				z = this.RdxW - z;
				z %= this.RdxW;

				this.Figures = new List<int>(TCalcArrays.CopyOf(this.Figures.ToArray(), this.Figures.Count + z));
			}

			List<int> bundles = new List<int>();

			for (int index = 0; index < this.Figures.Count; index += this.RdxW)
			{
				int value = 0;
				int scale = 1;

				for (int ndx = 0; ndx < this.RdxW; ndx++)
				{
					value += this.Figures[index + ndx] * scale;
					scale *= this.Radix;
				}
				bundles.Add(value);
			}
			return new FatFloat(new FatUFloat(new FatUInt(bundles.ToArray()), this.Exponent / this.RdxW), this.Sign);
		}
	}
}
