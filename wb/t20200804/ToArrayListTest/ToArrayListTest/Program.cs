using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{4d46124f-74fd-43a0-ac39-3140abff295d}";
		public const string APP_TITLE = "ToArrayListTest";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			string successfulFile = ar.NextArg();
			string errorFile = ar.NextArg();
			int count = int.Parse(ar.NextArg());

			try
			{
				if (ar.ArgIs("/A"))
				{
					if (ar.ArgIs("/MS"))
					{
						ToArrayTest(count, v => v.ToArray());
					}
					if (ar.ArgIs("/Ch"))
					{
						ToArrayTest(count, v => ArrayTools.ToArray(v));
					}
				}
				if (ar.ArgIs("/L"))
				{
					if (ar.ArgIs("/MS"))
					{
						ToListTest(count, v => v.ToList());
					}
					if (ar.ArgIs("/Ch"))
					{
						ToListTest(count, v => ArrayTools.ToList(v));
					}
				}

				File.WriteAllBytes(successfulFile, BinTools.EMPTY);
			}
			catch (Exception e)
			{
				File.WriteAllText(errorFile, e.ToString(), Encoding.UTF8);
			}
		}

		private void ToArrayTest(int count, Func<IEnumerable<int>, int[]> conv)
		{
			IEnumerable<int> arr = GetIteration(count);

			int[] arr2 = conv(arr);

			if (arr2.Count() != count)
				throw null;

			for (int index = 0; index < count; index++)
				if (index != arr2[index])
					throw null;
		}

		private void ToListTest(int count, Func<IEnumerable<int>, List<int>> conv)
		{
			IEnumerable<int> arr = GetIteration(count);

			List<int> arr2 = conv(arr);

			if (arr2.Count() != count)
				throw null;

			for (int index = 0; index < count; index++)
				if (index != arr2[index])
					throw null;
		}

		private IEnumerable<int> GetIteration(int count)
		{
			for (int index = 0; index < count; index++)
				yield return index;
		}
	}
}
