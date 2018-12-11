using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class WrapperTest
	{
		public void Test01()
		{
			{
				string val = Wrapper.Create("123")
					.Change(w => int.Parse(w))
					.Change(w => w + 1)
					.Change(w => "" + w)
					.Value;

				Console.WriteLine(val);
			}

			{
				var a = "123";
				var b = int.Parse(a);
				var c = b + 1;
				var val = "" + c;

				Console.WriteLine(val);
			}

			// ----

			{
				string val = Wrapper.Create("2:5:3:1:4")
					.Change(w => w.Split(':'))
					.Change(w => w.ToList())
					.Change(w => w.Select(value => int.Parse(value)))
					.Change(w => new List<int>(w))
					.Accept(w => w.Sort(IntTools.Comp))
					.Change(w => w.Select(value => "" + value))
					.Change(w => string.Join(":", w))
					.Value;

				Console.WriteLine(val);
			}

			{
				var a = "2:5:3:1:4";
				var b = a.Split(':');
				var c = b.ToList();
				var d = c.Select(value => int.Parse(value));
				var e = new List<int>(d);
				e.Sort(IntTools.Comp);
				var f = e.Select(value => "" + value);
				var val = string.Join(":", f);

				Console.WriteLine(val);
			}
		}
	}
}
