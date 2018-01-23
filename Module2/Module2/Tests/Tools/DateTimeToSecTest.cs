using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class DateTimeToSecTest
	{
		public void Test01()
		{
			if (AddSecToDateTime_LongSec(10101000000L, DateTimeToSec.ToSec(10000101000000L)) != 10000101000000L)
				throw null; // bug

			Test01_a(10000101000000, 90001231235959, 86400 * 7);
			Test01_a(19000101000000, 21001231235959, 3600);
			Test01_a(19990101000000, 20011231235959, 60);
		}

		private void Test01_a(long minDateTime, long maxDateTime, int maxStep)
		{
			long dateTime = minDateTime;
			long sec = DateTimeToSec.ToSec(dateTime);

			do
			{
				//Console.WriteLine("" + dateTime); // test

				long retSec = DateTimeToSec.ToSec(dateTime);
				long retDateTime = DateTimeToSec.ToDateTime(sec);

				if (sec != retSec)
					throw null; // bug

				if (dateTime != retDateTime)
					throw null; // bug

				int step = SecurityTools.CRandom.GetRange(1, maxStep);

				sec += step;
				dateTime = AddSecToDateTime(dateTime, step);
			}
			while (dateTime <= maxDateTime);
		}

		private long AddSecToDateTime_LongSec(long dateTime, long sec)
		{
			while (1L <= sec)
			{
				int step = (int)Math.Min((long)IntTools.IMAX, sec);

				dateTime = AddSecToDateTime(dateTime, step);
				sec -= step;
			}
			return dateTime;
		}

		private long AddSecToDateTime(long dateTime, int sec)
		{
			int s = (int)(dateTime % 100L);
			dateTime /= 100L;
			int i = (int)(dateTime % 100L);
			dateTime /= 100L;
			int h = (int)(dateTime % 100L);
			dateTime /= 100L;
			int d = (int)(dateTime % 100L);
			dateTime /= 100L;
			int m = (int)(dateTime % 100L);
			int y = (int)(dateTime / 100L);

			s += sec;

			i += s / 60;
			s %= 60;

			h += i / 60;
			i %= 60;

			d += h / 24;
			h %= 24;

			for (; ; )
			{
				int days = DateTime.DaysInMonth(y, m);

				if (d <= days)
					break;

				m++;
				d -= days;

				if (12 < m)
				{
					y++;
					m = 1;
				}
			}

			return
				y * 10000000000L +
				m * 100000000L +
				d * 1000000L +
				h * 10000L +
				i * 100L +
				s;
		}
	}
}
