using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.wb.t20191022
{
	public class Test0001
	{
		public void Test01()
		{
			var ps = EnumerableTools.Linearize(
				Enumerable.Range(1, 13).Select(a => Enumerable.Range(1, 13).Where(b => a < b).Select(b => new { A = a, B = b }))
				)
				.ToList();

			ps = ps.Where(p => ps.Where(q => q.A * q.B == p.A * p.B).Count() != 1).ToList();
			ps = ps.Where(p => ps.Where(q => q.A + q.B == p.A + p.B).Count() != 1).ToList();
			ps = ps.Where(p => ps.Where(q => q.A - q.B == p.A - p.B).Count() != 1).ToList();
			ps = ps.Where(p => ps.Where(q => q.A * q.B == p.A * p.B).Count() != 1).ToList();

			{
				var pairs2 = ps.Where(p => ps.Where(q => q.A + q.B == p.A + p.B).Count() != 1).ToList();
				var pairs3 = ps.Where(p => ps.Where(q => q.A - q.B == p.A - p.B).Count() != 1).ToList();

				ps = pairs2.Where(p => pairs3.Any(q => q.A == p.A && q.B == p.B)).ToList();
			}

			ps.ForEach(p => Console.WriteLine(p.A + " " + p.B));
		}
	}
}
