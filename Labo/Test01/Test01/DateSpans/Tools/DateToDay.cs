using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.DateSpans.Tools
{
	public class DateToDay
	{
		public static int ToDay(int date)
		{
			return (int)(DateTimeToSec.ToSec(date * 1000000L) / 86400L);
		}

		public static int ToDate(int day)
		{
			return (int)(DateTimeToSec.ToDateTime(day * 86400L) / 1000000L);
		}

		public class Allow5To7Dig
		{
			public static int ToDay(int date)
			{
				return (int)(DateTimeToSec.Allow11To13Dig.ToSec(date * 1000000L) / 86400L);
			}

			public static int ToDate(int day)
			{
				return (int)(DateTimeToSec.Allow11To13Dig.ToDateTime(day * 86400L) / 1000000L);
			}
		}
	}
}
