using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.wb.t20190827
{
	/// <summary>
	/// バケット・はかりの構造を勘違いしていた様なので没
	/// </summary>
	public class Test0002
	{
		private class ReportInfo
		{
			public int BacketCount;
			public List<int> Weights;
		}

		private List<ReportInfo> Reports = new List<ReportInfo>();

		public void Test01()
		{
			//Test01_a(1);
			//Test01_a(2);
			//Test01_a(3);
			//Test01_a(4);
			//Test01_a(5);
			//Test01_a(6);
			Test01_a(7);
			Test01_a(8);
			Test01_a(9);
			Test01_a(10);
			Test01_a(11);
			Test01_a(12);
			Test01_a(13);
			Test01_a(14);
			Test01_a(15);
			Test01_a(16);

			DoReport();
		}

		private const int BAGGING_COUNT = 1000;

		private int[] Backets;
		private List<int> Weights;

		private void Test01_a(int backetCount)
		{
			Console.WriteLine(DateTime.Now + " " + backetCount); // test

			Backets = new int[backetCount];
			Weights = new List<int>();

			while (Weights.Count < BAGGING_COUNT)
			{
				Charge();
				TryBagging();
			}

			Reports.Add(new ReportInfo()
			{
				BacketCount = backetCount,
				Weights = Weights,
			});

			Console.WriteLine(DateTime.Now); // test
			Console.WriteLine("Backets: " + string.Join(", ", Backets)); // test
		}

		private const int CHARGE_WEIGHT_MIN = 1;
		private const int CHARGE_WEIGHT_MAX = 100;

		private void Charge()
		{
			for (int index = 0; index < Backets.Length; index++)
			{
				Backets[index] += SecurityTools.CRandom.GetRange(CHARGE_WEIGHT_MIN, CHARGE_WEIGHT_MAX);
			}
		}

		private void TryBagging()
		{
			int pattern = GetBaggingBestPattern();

			if (pattern != -1)
			{
				int weight = 0;

				for (int index = 0; index < Backets.Length; index++)
				{
					if ((pattern & (1 << index)) != 0)
					{
						weight += Backets[index];
						Backets[index] = 0; // バケットの中を空にする。
					}
				}
				Weights.Add(weight);
			}
		}

		private const int BAGGING_WEIGHT = 1000;

		private int GetBaggingBestPattern()
		{
			if (Backets.Sum() < BAGGING_WEIGHT)
				return -1;

			int bestPattern = -1;
			int bestWeight = int.MaxValue;

			//int mask = 1 << GetHeaviestBacketIndex(); // 1番重いバケットは必ず選ばれるようにしてみる。

			for (int p = 0; p < (1 << Backets.Length); p++)
			{
				//int pattern = p | mask;
				int pattern = p;

				int weight = Enumerable.Range(0, Backets.Length).Where(index => (pattern & (1 << index)) != 0).Select(index => Backets[index]).Sum();

				if (BAGGING_WEIGHT <= weight && weight <= bestWeight)
				{
					bestPattern = pattern;
					bestWeight = weight;
				}
			}
			return bestPattern;
		}

		private int GetHeaviestBacketIndex()
		{
			int ret = 0;

			for (int index = 1; index < Backets.Length; index++)
				if (Backets[ret] < Backets[index])
					ret = index;

			return ret;
		}

		private const int OUTPUT_RANGE = 50;

		private void DoReport()
		{
			using (StreamWriter Writer = new StreamWriter(@"C:\temp\Report.txt", false, Encoding.UTF8))
			{
				Writer.Write("|*重さ ＼ バケット数");

				for (int c = 0; c < Reports.Count; c++)
				{
					Writer.Write("|*" + Reports[c].BacketCount);
				}
				Writer.WriteLine("|");

				for (int w = BAGGING_WEIGHT; w < BAGGING_WEIGHT + OUTPUT_RANGE; w++)
				{
					Writer.Write("|*" + w + " g");

					for (int c = 0; c < Reports.Count; c++)
					{
						Writer.Write("|" + Reports[c].Weights.Where(weight => w == weight).Count());
					}
					Writer.WriteLine("|");
				}
				Writer.Write("|*" + (BAGGING_WEIGHT + OUTPUT_RANGE) + " g 以上");

				for (int c = 0; c < Reports.Count; c++)
				{
					Writer.Write("|" + Reports[c].Weights.Where(weight => (BAGGING_WEIGHT + OUTPUT_RANGE) <= weight).Count());
				}
				Writer.WriteLine("|");
				Writer.WriteLine("");
				Writer.WriteLine("|*バケット数|*重さの平均|");

				for (int c = 0; c < Reports.Count; c++)
				{
					Writer.WriteLine(
						"|*" +
						Reports[c].BacketCount +
						"|" +
						(Reports[c].Weights.Sum() * 1.0 / Reports[c].Weights.Count).ToString("F4") +
						" g|"
						);
				}
			}
		}
	}
}
