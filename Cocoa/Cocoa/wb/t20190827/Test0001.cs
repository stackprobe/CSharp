using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Diagnostics;

namespace Charlotte.wb.t20190827
{
	public class Test0001
	{
		public void Test01()
		{
			Test01_01();
			//Test01_02();
		}

		private void Test01_01()
		{
			Test01_a(16, 0, 500, 1000);
			Test01_a(15, 0, 500, 1000);
			Test01_a(14, 0, 500, 1000); // ***
			Test01_a(13, 0, 500, 1000);
			Test01_a(12, 0, 500, 1000);
			Test01_a(11, 0, 500, 1000);
			Test01_a(10, 0, 500, 1000);
		}

		private void Test01_02()
		{
			Test01_a(14, 0, 500, 1000);
			Test01_a(14, 100, 400, 1000);
			Test01_a(14, 200, 300, 1000);
		}

		private void Test01_a(int b_num, int min_w, int max_w, int target)
		{
			Console.Write(b_num + ", " + min_w + " ～ " + max_w + ", " + target + " > ");

			int min_p = int.MaxValue;
			int max_p = -1;
			long max_lap = -1;

			for (int c = 0; c < 10000; c++)
			{
				int[] bs = GetBS(b_num, min_w, max_w);
				Stopwatch sw = new Stopwatch();
				sw.Start();
				int p = Choose(bs, target);
				sw.Stop();
				long lap = sw.ElapsedMilliseconds;

				min_p = Math.Min(min_p, p);
				max_p = Math.Max(max_p, p);
				max_lap = Math.Max(max_lap, lap);
			}
			Console.WriteLine(min_p + " ～ " + max_p + ", " + max_lap);
		}

		private int[] GetBS(int b_num, int min_w, int max_w)
		{
			int[] bs = new int[b_num];

			for (int i = 0; i < b_num; i++)
			{
				bs[i] = SecurityTools.CRandom.GetRange(min_w, max_w);
			}
			return bs;
		}

		private int[] _bs;
		private int _target;
		private int _currP;
		private int _bestP;

		private int Choose(int[] bs, int target)
		{
			_bs = bs;
			_target = target;
			_currP = 0;
			_bestP = -1;

			Search(0);

			return _bestP;
		}

		private void Search(int index)
		{
			if (Math.Abs(_currP - _target) < Math.Abs(_bestP - _target))
				_bestP = _currP;

			if (_bs.Length <= index)
				return;

			if (_target <= _currP)
				return;

			_currP += _bs[index];
			Search(index + 1);
			_currP -= _bs[index];
			Search(index + 1);
		}
	}
}
