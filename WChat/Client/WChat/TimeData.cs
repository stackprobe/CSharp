using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class TimeData
	{
		private long _t;

		public TimeData(long t)
		{
			_t = t;
		}

		public TimeData(int[] c)
		{
			_t = GetTime(c);
		}

		public TimeData(int y, int m, int d, int h, int i, int s)
		{
			_t = GetTime(y, m, d, h, i, s);
		}

		public long T
		{
			get
			{
				return _t;
			}
		}

		public int[] C
		{
			get
			{
				return ParseTime(_t);
			}
		}

		public static long GetTime(int[] c)
		{
			try
			{
				return GetTime(c[0], c[1], c[2], c[3], c[4], c[5]);
			}
			catch
			{
				return -1;
			}
		}

		public static long GetTime(int y, int m, int d, int h, int i, int s)
		{
			try
			{
				if (
						y < 1 ||
						m < 1 ||
						d < 1 ||
						h < 0 ||
						i < 0 ||
						s < 0
						)
				{
					throw new Exception("不正な日付");
				}
				long ly = y;

				m--;
				ly += m / 12;
				m %= 12;
				m++;

				if (m <= 2)
				{
					ly--;
				}
				long t = ly / 400;

				t *= 365 * 400 + 97;
				ly %= 400;
				t += ly * 365;
				t += ly / 4;
				t -= ly / 100;

				if (2 < m)
				{
					t -= 31 * 10 - 4;
					m -= 3;
					t += (m / 5) * (31 * 5 - 2);
					m %= 5;
					t += (m / 2) * (31 * 2 - 1);
					m %= 2;
					t += m * 31;
				}
				else
				{
					t += (m - 1) * 31;
				}
				t += d - 1;
				t *= 24;
				t += h;
				t *= 60;
				t += i;
				t *= 60;
				t += s;

				return t;
			}
			catch
			{
				return -1;
			}
		}

		public static int[] ParseTime(long t)
		{
			try
			{
				if (t < 0)
				{
					throw new Exception("紀元前");
				}
				int y = 0;
				int m = 0;

				for (int yb = 0x40000000; 0 < yb; yb /= 2)
				{
					int yt = y + yb;

					if (GetTime(yt, 1, 1, 0, 0, 0) <= t)
					{
						y = yt;
					}
				}
				for (int mb = 8; 0 < mb; mb /= 2)
				{
					int mt = m + mb;

					if (mt <= 12 && GetTime(y, mt, 1, 0, 0, 0) <= t)
					{
						m = mt;
					}
				}
				t -= GetTime(y, m, 1, 0, 0, 0);

				int s = (int)(t % 60);
				t /= 60;
				int i = (int)(t % 60);
				t /= 60;
				int h = (int)(t % 24);
				t /= 24;

				if (31 <= t)
				{
					throw new Exception("2^31年以降");
				}
				int d = (int)t + 1;

				return new int[] { y, m, d, h, i, s };
			}
			catch
			{
				return new int[] { 0, 0, 0, 0, 0, 0 };
			}
		}

		public static readonly TimeData POSIX_TIME_ZERO = new TimeData(1970, 1, 1, 0, 0, 0);

		public static TimeData Now()
		{
			DateTime now = DateTime.Now;
			return new TimeData(now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second);
		}

		public string GetString(string str)
		{
			int[] c = this.C;

			str = str.Replace("<P-Day>", "" + (this.GetPosixTime() / 86400.0));

			str = str.Replace("Y", StringTools.ZPad(c[0], 4));
			str = str.Replace("M", StringTools.ZPad(c[1], 2));
			str = str.Replace("D", StringTools.ZPad(c[2], 2));
			str = str.Replace("h", StringTools.ZPad(c[3], 2));
			str = str.Replace("m", StringTools.ZPad(c[4], 2));
			str = str.Replace("s", StringTools.ZPad(c[5], 2));
			str = str.Replace("W", this.GetJWeek());
			str = str.Replace("P", "" + this.GetPosixTime());

			return str;
		}

		public override string ToString()
		{
			return this.GetString("Y/M/D h:m:s");
		}

		public static TimeData Parse(string str)
		{
			string format = StringTools.ToFormat(str);

			if (format == "99999999999999")
			{
				return new TimeData(
					int.Parse(str.Substring(0, 4)),
					int.Parse(str.Substring(4, 2)),
					int.Parse(str.Substring(6, 2)),
					int.Parse(str.Substring(8, 2)),
					int.Parse(str.Substring(10, 2)),
					int.Parse(str.Substring(12))
					);
			}
			if (format == "9999/99/99 99:99:99")
			{
				List<string> tokens = StringTools.NumericTokenize(str);

				return new TimeData(
					int.Parse(tokens[0]),
					int.Parse(tokens[1]),
					int.Parse(tokens[2]),
					int.Parse(tokens[3]),
					int.Parse(tokens[4]),
					int.Parse(tokens[5])
					);
			}
			if (format == "99999999")
			{
				return new TimeData(
					int.Parse(str.Substring(0, 4)),
					int.Parse(str.Substring(4, 2)),
					int.Parse(str.Substring(6)),
					0,
					0,
					0
					);
			}
			if (format == "9999/99/99")
			{
				List<string> tokens = StringTools.NumericTokenize(str);

				return new TimeData(
					int.Parse(tokens[0]),
					int.Parse(tokens[1]),
					int.Parse(tokens[2]),
					0,
					0,
					0
					);
			}
			throw new Exception("そんな日時フォーマット知りません。" + str);
		}

		public string ToCompact()
		{
			return this.GetString("YMDhms");
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>0...6 as Mon...Sun</returns>
		public int GetWeek()
		{
			return (int)((_t / 86400) % 7);
		}

		public string GetJWeek()
		{
			return GetJWeek(this.GetWeek());
		}

		public static string GetJWeek(int week)
		{
			return new string[]
			{
				"月",
				"火",
				"水",
				"木",
				"金",
				"土",
				"日",
			}
			[week];
		}

		public long GetPosixTime()
		{
			return _t - POSIX_TIME_ZERO._t;
		}
	}
}
