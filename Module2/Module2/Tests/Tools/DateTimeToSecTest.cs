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
			Test01_a(10000101000000L, 90001231235959L, 86400 * 7);
			Test01_a(19000101000000L, 21001231235959L, 3600);
			Test01_a(19990101000000L, 20011231235959L, 60);

			{
				long sec = DateTimeToSec.ToSec(10000101000003L);

				Test01_b(10000101000003L, sec); // min + 3
				Test01_b(10000101000002L, --sec); // min + 2
				Test01_b(10000101000001L, --sec); // min + 1
				Test01_b(10000101000000L, --sec); // min
			}

			{
				long sec = DateTimeToSec.ToSec(99991231235956L);

				Test01_b(99991231235956L, sec); // max - 3
				Test01_b(99991231235957L, ++sec); // max - 2
				Test01_b(99991231235958L, ++sec); // max - 1
				Test01_b(99991231235959L, ++sec); // max
			}

			{
				long sec = DateTimeToSec.ToSec(10000101000000L);

				if (DateTimeToSec.ToDateTime(sec) != 10000101000000L) throw null; // min sec
				if (DateTimeToSec.ToDateTime(--sec) != 10000101000000L) throw null; // min sec - 1
				if (DateTimeToSec.ToDateTime(--sec) != 10000101000000L) throw null; // min sec - 2
				if (DateTimeToSec.ToDateTime(--sec) != 10000101000000L) throw null; // min sec - 3
			}

			{
				long sec = DateTimeToSec.ToSec(99991231235959L);

				if (DateTimeToSec.ToDateTime(sec) != 99991231235959L) throw null; // max sec
				if (DateTimeToSec.ToDateTime(++sec) != 99991231235959L) throw null; // max sec + 1
				if (DateTimeToSec.ToDateTime(++sec) != 99991231235959L) throw null; // max sec + 2
				if (DateTimeToSec.ToDateTime(++sec) != 99991231235959L) throw null; // max sec + 3
			}

			{
				long sec = DateTimeToSec.ToSec(10000101000000L);

				if (DateTimeToSec.ToSec(10000101000000L) != sec) throw null; // min date
				if (DateTimeToSec.ToSec(09991231235959L) != 0L) throw null; // min date - 1
				if (DateTimeToSec.ToSec(09991231235958L) != 0L) throw null; // min date - 2
				if (DateTimeToSec.ToSec(09991231235957L) != 0L) throw null; // min date - 3
			}

			{
				long sec = DateTimeToSec.ToSec(99991231235959L);

				if (DateTimeToSec.ToSec(99991231235959L) != sec) throw null; // max date
				if (DateTimeToSec.ToSec(100000101000000L) != 0L) throw null; // max date + 1
				if (DateTimeToSec.ToSec(100000101000000L) != 0L) throw null; // max date + 2
				if (DateTimeToSec.ToSec(100000101000000L) != 0L) throw null; // max date + 3
			}

			{
				long sec = DateTimeToSec.ToSec(20180615123030L);

				if (DateTimeToSec.ToSec(20180615123030L) != sec) throw null;
				if (DateTimeToSec.ToSec(09990615123030L) != 0L) throw null; // min y - 1
				if (DateTimeToSec.ToSec(100000615123030L) != 0L) throw null; // max y + 1
				if (DateTimeToSec.ToSec(20180015123030L) != 0L) throw null; // min m - 1
				if (DateTimeToSec.ToSec(20181315123030L) != 0L) throw null; // max m + 1
				if (DateTimeToSec.ToSec(20180600123030L) != 0L) throw null; // min d - 1
				if (DateTimeToSec.ToSec(20180632123030L) != 0L) throw null; // max d + 1
				if (DateTimeToSec.ToSec(20180615243030L) != 0L) throw null; // max h + 1
				if (DateTimeToSec.ToSec(20180615126030L) != 0L) throw null; // max i + 1
				if (DateTimeToSec.ToSec(20180615123060L) != 0L) throw null; // max s + 1
			}
		}

		private void Test01_a(long minDateTime, long maxDateTime, int maxStep)
		{
			long dateTime = minDateTime;
			long sec = DateTimeToSec.ToSec(dateTime);

			if (AddSecToDateTime_LongSec(10101000000L, sec) != dateTime)
				throw null;

			do
			{
				long rSec = DateTimeToSec.ToSec(dateTime);
				long rDateTime = DateTimeToSec.ToDateTime(sec);

				if (sec != rSec)
					throw null;

				if (dateTime != rDateTime)
					throw null;

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

		private void Test01_b(long dateTime, long sec)
		{
			long rSec = DateTimeToSec.ToSec(dateTime);
			long rDateTime = DateTimeToSec.ToDateTime(sec);

			if (sec != rSec)
				throw null;

			if (dateTime != rDateTime)
				throw null;
		}
	}
}
