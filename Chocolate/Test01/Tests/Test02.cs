using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests
{
	public class Test02
	{
		public void Main01()
		{
			MSDateTimeToDateTime();
			DateTimeToMSDateTime();
		}

		private void MSDateTimeToDateTime()
		{
			long dateTime = DateTimeToSec.Now.GetDateTime();

			Console.WriteLine("< " + dateTime);

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

			DateTime dt = new DateTime(y, m, d, h, i, s);

			Console.WriteLine("> " + dt);
		}

		private void DateTimeToMSDateTime()
		{
			DateTime dt = DateTime.Now;

			Console.WriteLine("< " + dt);

			int y = dt.Year;
			int m = dt.Month;
			int d = dt.Day;
			int h = dt.Hour;
			int i = dt.Minute;
			int s = dt.Second;

			long dateTime =
				y * 10000000000L +
				m * 100000000L +
				d * 1000000L +
				h * 10000L +
				i * 100L +
				s;

			Console.WriteLine("> " + dateTime);
		}
	}
}
