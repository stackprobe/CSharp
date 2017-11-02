using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class DateToDayTest
	{
		public void test01()
		{
			Console.WriteLine("" + DateToDay.Today.getDate());
			Console.WriteLine("" + DateToDay.toDate(DateToDay.Today.getDay()));
		}

		public static bool IsUruu(int year)
		{
			return (year % 4) == 0 && (year % 100) != 0 || (year % 400) == 0; // from Wikipedia
		}

		public static int GetDayNum(int y, int m)
		{
			switch (m)
			{
				case 1: return 31;
				case 2: return IsUruu(y) ? 29 : 28;
				case 3: return 31;
				case 4: return 30;
				case 5: return 31;
				case 6: return 30;
				case 7: return 31;
				case 8: return 31;
				case 9: return 30;
				case 10: return 31;
				case 11: return 30;
				case 12: return 31;

				default:
					throw null;
			}
		}

		public void test02()
		{
			int day = DateToDay.toDay(10000101);

			for (int y = 1000; y <= 9999; y++)
			{
				for (int m = 1; m <= 12; m++)
				{
					int dnum = GetDayNum(y, m);

					for (int d = 1; d <= dnum; d++)
					{
						int date = y * 10000 + m * 100 + d;

						//Console.WriteLine("" + day);
						//Console.WriteLine("" + date);

						if (DateToDay.toDay(date) != day) throw null;
						if (DateToDay.toDate(day) != date) throw null;

						day++;
					}
				}
			}
		}
	}
}
