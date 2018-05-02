using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test01.DateSpans.Tools;

namespace Test01.DateSpans
{
	public class DateSpanList
	{
		public const char DELIMITER = ',';
		public const char JOINT = '-';

		private class DateInfo
		{
			public int Day;

			public int Date
			{
				get { return DateToDay.ToDate(this.Day); }
			}

			public static DateInfo Create(string str)
			{
				return Create(int.Parse(str));
			}

			public static DateInfo Create(int date)
			{
				int day = DateToDay.ToDay(date);

				if (DateToDay.ToDate(day) != date)
					throw new ArgumentException(string.Format("”{0}”は日付ではありません。", date));

				return new DateInfo()
				{
					Day = day,
				};
			}
		}

		private class DateSpan
		{
			public DateInfo First;
			public DateInfo End;

			public static DateSpan Create(string str)
			{
				return Create(str, str);
			}

			public static DateSpan Create(string str1, string str2)
			{
				DateInfo a = DateInfo.Create(str1);
				DateInfo b = DateInfo.Create(str2);

				if (b.Day < a.Day)
					throw new ArgumentException(string.Format("”{0}”は”{1}”より先の日付です。", str1, str2));

				return new DateSpan()
				{
					First = a,
					End = b,
				};
			}

			public string GetString()
			{
				if (this.First.Day == this.End.Day)
				{
					return "" + this.First.Date;
				}
				else
				{
					return this.First.Date + ("" + JOINT) + this.End.Date;
				}
			}
		}

		private List<DateSpan> DateSpans = new List<DateSpan>();

		public void Add(string str)
		{
			foreach (string span in str.Split(DELIMITER))
			{
				string[] dates = span.Split(JOINT);

				if (dates.Length == 1)
				{
					this.DateSpans.Add(DateSpan.Create(dates[0]));
				}
				else if (dates.Length == 2)
				{
					this.DateSpans.Add(DateSpan.Create(dates[0], dates[1]));
				}
				else
				{
					throw new ArgumentException("”{0}”は日付又は期間ではありません。", span);
				}
			}
		}

		public void Sort()
		{
			this.DateSpans.Sort((a, b) =>
			{
				int ret = a.First.Day - b.First.Day;

				if (ret != 0)
					return ret;

				return a.End.Day - b.End.Day;
			});
		}

		public void Distinct()
		{
			for (int index = 1; index + 1 < this.DateSpans.Count; index++)
			{
				DateSpan a = this.DateSpans[index];

				for (int n = index + 1; n < this.DateSpans.Count; n++)
				{
					DateSpan b = this.DateSpans[n];

					if (b.First.Day < a.First.Day)
					{
						b.End.Day = Math.Min(b.End.Day, a.First.Day - 1);
					}
					else // ? a.First.Day <= b.First.Day
					{
						if (a.End.Day < b.End.Day)
						{
							b.First.Day = Math.Max(b.First.Day, a.End.Day + 1);
						}
						else
						{
							this.DateSpans.RemoveAt(n);
							n--;
						}
					}
				}
			}
		}

		public void Join()
		{
			for (int index = 1; index < this.DateSpans.Count; index++)
			{
				DateSpan a = this.DateSpans[index - 1];
				DateSpan b = this.DateSpans[index];

				if (a.End.Day + 1 == b.First.Day)
				{
					a.End.Day = b.End.Day;
					this.DateSpans.RemoveAt(index);
					index--;
				}
			}
		}

		public string GetString()
		{
			List<string> spans = new List<string>();

			foreach (DateSpan dateSpan in this.DateSpans)
			{
				spans.Add(dateSpan.GetString());
			}
			return string.Join("" + DELIMITER, spans);
		}

		public IEnumerable<int> GetDates()
		{
			foreach (DateSpan dateSpan in this.DateSpans)
			{
				for (int day = dateSpan.First.Day; day <= dateSpan.End.Day; day++)
				{
					yield return DateToDay.ToDate(day);
				}
			}
		}

		public string GetStringDates()
		{
			return string.Join("" + DELIMITER, this.GetDates().Select(date => "" + date));
		}
	}
}
