using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;

namespace Charlotte.wb.t20191022
{
	public class Test0001
	{
		// 第25回算数オリンピックファイナル 第3問

		public void Test01()
		{
			// 1～13までの数字が1つずつ書かれた13枚のカードがあります。いま、先生がこの中から2枚をひいて、
			var ps = Enumerable.Range(0, 13 * 13).Select(v => new { A = v / 13 + 1, B = v % 13 + 1 }).Where(p => p.A < p.B).ToList();

			ps = ps.Where(p => ps.Where(q => q.A * q.B == p.A * p.B).Count() != 1).ToList(); // A君「わからないな。」
			ps = ps.Where(p => ps.Where(q => q.A + q.B == p.A + p.B).Count() != 1).ToList(); // B君「ぼくもわからないよ。」
			ps = ps.Where(p => ps.Where(q => q.A - q.B == p.A - p.B).Count() != 1).ToList(); // C君「うーん、やっぱりわからないなあ。」
			ps = ps.Where(p => ps.Where(q => q.A * q.B == p.A * p.B).Count() != 1).ToList(); // A君「まだわからない。」

			// B君,C君「ぼくたちもわからない。」
			{
				var ps2 = ps.Where(p => ps.Where(q => q.A + q.B == p.A + p.B).Count() != 1).ToList();
				var ps3 = ps.Where(p => ps.Where(q => q.A - q.B == p.A - p.B).Count() != 1).ToList();

				ps = ps2.Where(p => ps3.Any(q => q.A == p.A && q.B == p.B)).ToList();
			}

			ps.ForEach(p => Console.WriteLine(p.A + " " + p.B)); // 先生がひいた2枚のカードの数字を2つとも答えなさい。
		}
	}
}
