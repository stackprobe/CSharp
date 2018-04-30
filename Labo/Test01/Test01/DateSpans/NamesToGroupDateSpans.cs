using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Test01.DateSpans.Tools;

namespace Test01.DateSpans
{
	public class NamesToGroupDateSpans
	{
		public class Group
		{
			public string Prefix;
			public string Suffix;
			public DateSpanList DateSpanList = new DateSpanList();

			public bool IsSame(Group otherside)
			{
				return
					this.Prefix == otherside.Prefix &&
					this.Suffix == otherside.Suffix;
			}
		}

		public class Other
		{
			public string Name;
			public int Count;

			public bool IsSame(Other otherside)
			{
				return this.Name == otherside.Name;
			}
		}

		public List<Group> Groups = new List<Group>();
		public List<Other> Others = new List<Other>();

		public void Add(IEnumerable<string> names)
		{
			foreach (string name in names)
			{
				this.Add(name);
			}
		}

		private void Add(string name)
		{
			int index = ToFormat(name).IndexOf("99999999");
			int date = -1;

			if (index != -1)
			{
				date = int.Parse(name.Substring(index, 8));

				if (DateToDay.ToDate(DateToDay.ToDay(date)) != date)
					date = -1;
			}
			if (date != -1)
			{
				Group group = new Group()
				{
					Prefix = name.Substring(0, index),
					Suffix = name.Substring(index + 8),
				};

				index = this.IndexOf(group);

				if (index != -1)
					group = this.Groups[index];
				else
					this.Groups.Add(group);

				group.DateSpanList.Add("" + date);
			}
			else
			{
				Other other = new Other()
				{
					Name = name,
				};

				index = this.IndexOf(other);

				if (index != -1)
					other = this.Others[index];
				else
					this.Others.Add(other);

				other.Count++;
			}
		}

		private int IndexOf(Other target)
		{
			for (int index = 0; index < this.Others.Count; index++)
				if (this.Others[index].IsSame(target))
					return index;

			return -1;
		}

		private int IndexOf(Group target)
		{
			for (int index = 0; index < this.Groups.Count; index++)
				if (this.Groups[index].IsSame(target))
					return index;

			return -1;
		}

		private string ToFormat(string str)
		{
			foreach (char chr in "012345678")
				str = str.Replace(chr, '9');

			return str;
		}

		public string GetString()
		{
			// ---- sort ----

			this.Groups.Sort((a, b) =>
			{
				int ret = StringTools.Comp(a.Prefix, b.Prefix);

				if (ret != 0)
					return ret;

				return StringTools.Comp(a.Suffix, b.Suffix);
			});

			this.Others.Sort((a, b) => StringTools.Comp(a.Name, b.Name));

			// ---- adjust ----

			foreach (Group group in this.Groups)
			{
				group.DateSpanList.Sort();
				//group.DateSpanList.Distinct(); // dont
				group.DateSpanList.Join();
			}

			// ----

			List<string> dest = new List<string>();

			foreach (Group group in this.Groups)
			{
				dest.Add("\"" + group.Prefix + "<YYYYMMDD>" + group.Suffix + "\" (" + group.DateSpanList.GetString() + ")");
			}
			foreach (Other other in this.Others)
			{
				dest.Add("\"" + other.Name + "\" (" + other.Count + ")");
			}
			return string.Join(", ", dest);
		}
	}
}
