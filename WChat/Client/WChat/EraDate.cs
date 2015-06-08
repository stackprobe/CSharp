using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class EraDate
	{
		private static readonly string[] Names = new string[]
		{
			"明治",
			"大正",
			"昭和",
			"平成",
		};

		private static readonly int[] FirstDays = new int[]
		{
			18680125,
			19120730,
			19261225,
			19890108,
		};

		private int Day;

		public EraDate(int day)
		{
			this.Day = day;
		}

		public static EraDate Parse(int y, int m, int d)
		{
			return new EraDate(y * 10000 + m * 100 + d);
		}

		public static EraDate Parse(int eraIndex, int y, int m, int d)
		{
			if (eraIndex != -1)
				y += FirstDays[eraIndex] / 10000 - 1;

			return Parse(y, m, d);
		}

		public static EraDate Parse(string str, int y, int m, int d)
		{
			return Parse(GetEraIndex(str), y, m, d);
		}

		public static int GetEraIndex(string str)
		{
			for (int index = 0; index < Names.Length; index++)
				if (Names[index].Contains(str))
					return index;

			return -1; // not found
		}

		public static EraDate Parse(string str)
		{
			str = str.Replace("元", "1");

			List<string> tokens = StringTools.NumericTokenize(str);

			return Parse(str,
				int.Parse(tokens[0]),
				int.Parse(tokens[1]),
				int.Parse(tokens[2])
				);
		}

		public string GetString(string format)
		{
			string str = format;

			int e = GetEraIndex(this.Day);

			if (e == -1)
				throw new Exception("和暦に変換出来ません。");

			int y = this.Day / 10000;
			int m = (this.Day / 100) % 100;
			int d = this.Day % 100;

			y -= FirstDays[e] / 10000 - 1; // 西暦年 -> 和暦年

			string sy;

			if (y == 1)
				sy = "元";
			else
				sy = "" + y;

			str = str.Replace("E", Names[e]);
			str = str.Replace("Y", sy);
			str = str.Replace("M", StringTools.ZPad(m, 2));
			str = str.Replace("D", StringTools.ZPad(d, 2));

			return str;
		}

		public static int GetEraIndex(int day)
		{
			for (int index = FirstDays.Length - 1; 0 <= index; index--)
				if (FirstDays[index] <= day)
					return index;

			return -1; // not found
		}

		public override string ToString()
		{
			return this.GetString("EY年M月D日");
		}
	}
}
