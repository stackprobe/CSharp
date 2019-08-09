using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte
{
	public static class Sort0001
	{
		public static void Sort(int count, Comparison<int> comp, Action<int, int> swap)
		{
			int[] order = new int[count];

			for (int index = 0; index < count; index++)
				order[index] = index;

			Array.Sort<int>(order, comp);

			for (int index = 0; index < count; index++)
			{
				if (order[index] != -1)
				{
					for (; ; )
					{
						int prev = index;

						index = order[index];
						order[prev] = -1;

						if (order[index] == -1)
							break;

						swap(prev, index);
					}
				}
			}
		}

		public static void Sort2(int count, Comparison<int> comp, Action<int, int> swap)
		{
			int[] order = new int[count];
			int[] elementToPosition = new int[count];
			int[] positionToElement = new int[count];

			for (int index = 0; index < count; index++)
			{
				order[index] = index;
				elementToPosition[index] = index;
				positionToElement[index] = index;
			}

#if !true
			Array.Sort<int>(order, comp);
#else
			Array.Sort<int>(order, (int a, int b) =>
			{
				if (a == b)
					return 0;

				return comp(a, b);
			});
#endif

			for (int index = 0; index + 1 < count; index++)
			{
				if (order[index] != positionToElement[index])
				{
					int far = elementToPosition[order[index]];

					swap(index, far);

					{
						int e1 = positionToElement[index];
						//int e2 = positionToElement[far];

						//positionToElement[index] = e2;
						positionToElement[far] = e1;

						elementToPosition[e1] = far;
						//elementToPosition[e2] = index;
					}
				}
			}
		}

		public static void SortRetractable(int count, Comparison<int> comp, Action<int, int> move)
		{
			int[] order = new int[count];

			for (int index = 0; index < count; index++)
				order[index] = index;

			Array.Sort<int>(order, comp);

			for (int index = 0; index < count; index++)
			{
				if (order[index] != -1 && order[index] != index)
				{
					move(index, -1);

					for (; ; )
					{
						int prev = index;

						index = order[index];
						order[prev] = -1;

						if (order[index] == -1)
						{
							move(-1, prev);
							break;
						}
						move(index, prev);
					}
				}
			}
		}
	}
}
