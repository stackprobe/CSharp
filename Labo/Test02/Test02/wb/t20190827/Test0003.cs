using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.wb.t20190827
{
	public class Test0003
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
			Test01_a(6);
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
			Test01_a(17);
			Test01_a(18);
			//Test01_a(19);
			//Test01_a(20);

			DoReport();
		}

		private const int BAGGING_COUNT = 100000;

		private class BacketInfo
		{
			public int Pool;
			public int Scale;
		}

		private BacketInfo[] Backets;
		private List<int> Weights;

		private void Test01_a(int backetCount)
		{
			Console.WriteLine(DateTime.Now + " " + backetCount); // test

			Backets = new BacketInfo[backetCount];
			Weights = new List<int>();

			for (int index = 0; index < backetCount; index++)
				Backets[index] = new BacketInfo();

			while (Weights.Count < 100) // 最初の方は初期状態による誤差が出るかもしれないので、少し運転して捨てる。
			{
				Charge();
				TryBagging();
			}
			Weights.Clear(); // 捨てる。

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
			Console.WriteLine("Pools: " + string.Join(", ", Backets.Select(i => i.Pool))); // test
			Console.WriteLine("Scales: " + string.Join(", ", Backets.Select(i => i.Scale))); // test
		}

		private const int POOL_MAX = 300;

		private void Charge()
		{
			for (int index = 0; index < Backets.Length; index++)
			{
				Backets[index].Pool += SecurityTools.CRandom.GetRange(0, BAGGING_WEIGHT * 2 / Backets.Length); // 総チャージ量がだいたい梱包量になるように
				Backets[index].Pool = Math.Min(Backets[index].Pool, POOL_MAX); // アンチオーバーフロー
			}
		}

		private void TryBagging()
		{
			int[] combination = GetBaggingBestCombination();

			if (combination == null) // ? 梱包量に達する組み合わせが見つからない。
			{
				for (int index = 0; index < Backets.Length; index++) // 全 Pool を開ける。
				{
					Backets[index].Scale += Backets[index].Pool;
					Backets[index].Pool = 0;
				}
			}
			else
			{
				int weight = 0;

				foreach (int index in combination)
				{
					weight += Backets[index].Scale;
					Backets[index].Scale = Backets[index].Pool;
					Backets[index].Pool = 0;
				}
				Weights.Add(weight);
			}
		}

		private const int BAGGING_WEIGHT = 1000; // 梱包量

		private int[] BestCombination;
		private int BestWeight;
		private List<int> CurrCombination;
		private int CurrWeight;

		private int[] GetBaggingBestCombination()
		{
			if (Backets.Select(i => i.Scale).Sum() < BAGGING_WEIGHT)
				return null;

			BestCombination = null;
			BestWeight = int.MaxValue;
			CurrCombination = new List<int>();
			CurrWeight = 0;

			Search(0);

			return BestCombination;
		}

		private void Search(int index)
		{
			if (BAGGING_WEIGHT <= CurrWeight && CurrWeight < BestWeight)
			{
				BestCombination = CurrCombination.ToArray();
				BestWeight = CurrWeight;
				return;
			}
			if (Backets.Length <= index)
				return;

			if (BestWeight <= CurrWeight)
				return;

			CurrCombination.Add(index);
			CurrWeight += Backets[index].Scale;

			Search(index + 1);

			CurrCombination.RemoveAt(CurrCombination.Count - 1);
			CurrWeight -= Backets[index].Scale;

			Search(index + 1);
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
