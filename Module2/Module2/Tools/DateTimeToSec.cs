using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class DateTimeToSec
	{
		public static long ToSec(long dateTime)
		{
			if (dateTime < 10000101000000L || 99991231235959L < dateTime)
				throw new ArgumentOutOfRangeException("dateTime: " + dateTime);

			int date = (int)(dateTime / 1000000);
			int h = (int)((dateTime / 10000) % 100);
			int m = (int)((dateTime / 100) % 100);
			int s = (int)(dateTime % 100);

			if (
				date < 10000101 || 99991231 < date ||
				h < 0 || 23 < h ||
				m < 0 || 59 < m ||
				s < 0 || 59 < s
				)
				throw new ArgumentOutOfRangeException("dateTime: " + dateTime + ", date: " + date + ", h: " + h + ", m: " + m + ", s: " + s);

			long sec = DateToDay.ToDay(date);
			sec *= 24;
			sec += h;
			sec *= 60;
			sec += m;
			sec *= 60;
			sec += s;

			return sec;
		}

		public static long ToDateTime(long sec)
		{
			if (sec < 0)
				throw new ArgumentOutOfRangeException("sec: " + sec);

			long lDay = sec / 86400;

			if (int.MaxValue < lDay)
				throw new ArgumentOutOfRangeException("sec: " + sec + ", lDay: " + lDay);

			int h = (int)((sec / 3600) % 24);
			int m = (int)((sec / 60) % 60);
			int s = (int)(sec % 60);

			long dateTime = DateToDay.ToDate((int)lDay);
			dateTime *= 100;
			dateTime += h;
			dateTime *= 100;
			dateTime += m;
			dateTime *= 100;
			dateTime += s;

			return dateTime;
		}

		public static class Now
		{
			public static long GetSec()
			{
				return ToSec(GetDateTime());
			}

			public static long GetDateTime()
			{
				DateTime dt = DateTime.Now;
				return dt.Year * 10000000000L + dt.Month * 100000000L + dt.Day * 1000000L + dt.Hour * 10000L + dt.Minute * 100L + dt.Second;
			}
		}

		public static readonly long POSIX_ZERO = ToSec(19700101000000L);
	}
}
