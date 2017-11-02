using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class DateTimeToSecTest
	{
		public void test01()
		{
			Console.WriteLine("" + DateTimeToSec.Now.getDateTime());
			Console.WriteLine("" + DateTimeToSec.toDateTime(DateTimeToSec.Now.getSec()));
		}

		/// <summary>
		/// 全体
		/// 8h刻み
		/// </summary>
		public void test02()
		{
			long sec = DateTimeToSec.toSec(10000101000000L);

			for (int y = 1000; y <= 9999; y++)
			{
				for (int m = 1; m <= 12; m++)
				{
					int dnum = DateToDayTest.GetDayNum(y, m);

					for (int d = 1; d <= dnum; d++)
					{
						for (int h = 0; h < 24; h += 8)
						{
							long dateTime = y * 10000000000L + m * 100000000L + d * 1000000L + h * 10000L;

							//Console.WriteLine("" + sec);
							//Console.WriteLine("" + dateTime);

							if (DateTimeToSec.toSec(dateTime) != sec) throw null;
							if (DateTimeToSec.toDateTime(sec) != dateTime) throw null;

							sec += 3600 * 8;
						}
					}
				}
			}
		}

		/// <summary>
		/// 一部
		/// 1s刻み
		/// </summary>
		public void test03()
		{
			long sec = DateTimeToSec.toSec(19990101000000L);

			for (int y = 1999; y <= 2001; y++)
			{
				for (int m = 1; m <= 12; m++)
				{
					int dnum = DateToDayTest.GetDayNum(y, m);

					for (int d = 1; d <= dnum; d++)
					{
						for (int h = 0; h < 24; h++)
						{
							for (int i = 0; i < 60; i++)
							{
								for (int s = 0; s < 60; s += 15)
								{
									long dateTime = y * 10000000000L + m * 100000000L + d * 1000000L + h * 10000L + i * 100L + s;

									//Console.WriteLine("" + sec);
									//Console.WriteLine("" + dateTime);

									if (DateTimeToSec.toSec(dateTime) != sec) throw null;
									if (DateTimeToSec.toDateTime(sec) != dateTime) throw null;

									sec += 15;
								}
							}
						}
					}
				}
			}
		}
	}
}
