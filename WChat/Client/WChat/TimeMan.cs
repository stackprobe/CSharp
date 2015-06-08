using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public class TimeMan
	{
		private int Y;
		private int M;
		private int D;
		private int h;
		private int m;
		private int s;

		public TimeMan()
		{ }

		public TimeMan(string str)
		{
			this.SetString(str);
		}

		public void SetString(string str)
		{
			List<string> tokens = StringTools.NumericTokenize(str);

			if (tokens.Count == 1)
			{
				if (tokens[0].Length == 14)
				{
					this.Y = int.Parse(tokens[0].Substring(0, 4));
					this.M = int.Parse(tokens[0].Substring(4, 2));
					this.D = int.Parse(tokens[0].Substring(6, 2));
					this.h = int.Parse(tokens[0].Substring(8, 2));
					this.m = int.Parse(tokens[0].Substring(10, 2));
					this.s = int.Parse(tokens[0].Substring(12, 2));
				}
				else
				{
					TimeData td = new TimeData(long.Parse(tokens[0]) + TimeData.POSIX_TIME_ZERO.T);
					int[] c = td.C;

					this.Y = c[0];
					this.M = c[1];
					this.D = c[2];
					this.h = c[3];
					this.m = c[4];
					this.s = c[5];
				}
			}
			else if (tokens.Count == 6)
			{
				this.Y = int.Parse(tokens[0]);
				this.M = int.Parse(tokens[1]);
				this.D = int.Parse(tokens[2]);
				this.h = int.Parse(tokens[3]);
				this.m = int.Parse(tokens[4]);
				this.s = int.Parse(tokens[5]);
			}
		}

		public enum Mode_e
		{
			DEFAULT,
			DEFAULT_WEEK,
			SIMPLE,
			年月日,
			年月日曜日,
			POSIX_TIME,
		};

		public string GetString(Mode_e mode)
		{
			if (mode == Mode_e.DEFAULT)
			{
				return
					StringTools.ZPad(this.Y, 4) +
					"/" +
					StringTools.ZPad(this.M, 2) +
					"/" +
					StringTools.ZPad(this.D, 2) +
					" " +
					StringTools.ZPad(this.h, 2) +
					":" +
					StringTools.ZPad(this.m, 2) +
					":" +
					StringTools.ZPad(this.s, 2);
			}
			if (mode == Mode_e.DEFAULT_WEEK)
			{
				return
					StringTools.ZPad(this.Y, 4) +
					"/" +
					StringTools.ZPad(this.M, 2) +
					"/" +
					StringTools.ZPad(this.D, 2) +
					" (" +
					this.Get曜日() +
					") " +
					StringTools.ZPad(this.h, 2) +
					":" +
					StringTools.ZPad(this.m, 2) +
					":" +
					StringTools.ZPad(this.s, 2);
			}
			if (mode == Mode_e.SIMPLE)
			{
				return
					StringTools.ZPad(this.Y, 4) +
					StringTools.ZPad(this.M, 2) +
					StringTools.ZPad(this.D, 2) +
					StringTools.ZPad(this.h, 2) +
					StringTools.ZPad(this.m, 2) +
					StringTools.ZPad(this.s, 2);
			}
			if (mode == Mode_e.年月日)
			{
				return
					StringTools.ZPad(this.Y, 4) +
					"年" +
					StringTools.ZPad(this.M, 2) +
					"月" +
					StringTools.ZPad(this.D, 2) +
					"日 " +
					StringTools.ZPad(this.h, 2) +
					"時" +
					StringTools.ZPad(this.m, 2) +
					"分" +
					StringTools.ZPad(this.s, 2) +
					"秒";
			}
			if (mode == Mode_e.年月日曜日)
			{
				return
					StringTools.ZPad(this.Y, 4) +
					"年" +
					StringTools.ZPad(this.M, 2) +
					"月" +
					StringTools.ZPad(this.D, 2) +
					"日 (" +
					this.Get曜日() +
					"曜日) " +
					StringTools.ZPad(this.h, 2) +
					"時" +
					StringTools.ZPad(this.m, 2) +
					"分" +
					StringTools.ZPad(this.s, 2) +
					"秒";
			}
			if (mode == Mode_e.POSIX_TIME)
			{
				long posixTime = new TimeData(this.Y, this.M, this.D, this.h, this.m, this.s).T - TimeData.POSIX_TIME_ZERO.T;
				return "" + posixTime;
			}
			return "日時エラー";
		}

		/// <summary>
		/// 
		/// </summary>
		/// <returns>0～6 as 日曜日～土曜日</returns>
		public int GetWeekday()
		{
			DayOfWeek weekday = DayOfWeek.Sunday;

			try
			{
				weekday = new DateTime(this.Y, this.M, this.D).DayOfWeek;
			}
			catch
			{ }

			return (int)weekday;
		}

		public string Get曜日()
		{
			return "日月火水木金土".Substring(this.GetWeekday(), 1);
		}
	}
}
