using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.wb.t20190513
{
	public class Test0001
	{
		public void Test01()
		{
			Test01_07();
		}

		private void Test01_07()
		{
			Test01_a(new Status()
			{
				Map = new int[][]
				{
					new int[] { 301, 402, 402, 303 },
					new int[] { 301, 402, 402, 303 },
					new int[] { 204, 204, 205, 205 },
					new int[] { 106, 207, 207, 108 },
					new int[] { 109, 0, 0, 110 },
				},
			});
		}

		private class Bingo : Exception
		{ }

		private const int W = 4;
		private const int H = 5;

		private class Status
		{
			public Status Prev; // null == 初期状態

			// 0 -- empty cell

			// 100 -- 1x1
			// 200 -- 2x1 横長
			// 300 -- 1x2 縦長
			// 400 -- 2x2

			public int[][] Map;
		}

		private int Comp(Status a, Status b)
		{
			return ArrayTools.Comp(a.Map, b.Map, (aa, bb) => ArrayTools.Comp(aa, bb, (aaa, bbb) => IntTools.Comp(aaa / 100, bbb / 100)));
		}

		private List<Status> Tree = new List<Status>();

		private void Test01_a(Status status)
		{
			DateTime startedTime = DateTime.Now;

			try
			{
				this.Tree.Add(status);

				for (int index = 0; index < this.Tree.Count; index++)
				{
					TrySlide(this.Tree[index]);
				}
				throw new Exception("nSol");
			}
			catch (Bingo)
			{
				List<Status> dest = new List<Status>();

				dest.Add(Tree[Tree.Count - 1]);

				while (dest[dest.Count - 1].Prev != null)
				{
					dest.Add(dest[dest.Count - 1].Prev);
				}
				dest.Reverse();

				PrintAnswer(dest);
			}

			Console.WriteLine("exec time: " + (DateTime.Now - startedTime));
		}

		private void TrySlide(Status status)
		{
			for (int x = 0; x < W; x++)
			{
				for (int y = 0; y < H; y++)
				{
					if (status.Map[y][x] == 0) // ? empty cell
					{
						TrySlideCell(status, x, y);
					}
				}
			}
		}

		private void TrySlideCell(Status status, int x, int y)
		{
			TrySlideCell2(status, x, y, 1, 0);
			TrySlideCell2(status, x, y, -1, 0);
			TrySlideCell2(status, x, y, 0, 1);
			TrySlideCell2(status, x, y, 0, -1);
		}

		private void TrySlideCell2(Status status, int x, int y, int mx, int my)
		{
			int sx = x - mx;
			int sy = y - my;

			if (
				sx < 0 || W <= sx ||
				sy < 0 || H <= sy
				)
				return;

			int b = status.Map[sy][sx];

			Status statusNew = new Status()
			{
				Prev = status,
				Map = new int[H][],
			};

			for (int yi = 0; yi < H; yi++)
			{
				statusNew.Map[yi] = new int[W];

				for (int xi = 0; xi < W; xi++)
				{
					int c = status.Map[yi][xi];

					if (c == b)
						c = 0;

					statusNew.Map[yi][xi] = c;
				}
			}
			for (int yi = 0; yi < H; yi++)
			{
				for (int xi = 0; xi < W; xi++)
				{
					int c = status.Map[yi][xi];

					if (c == b)
					{
						int sxi = xi + mx;
						int syi = yi + my;

						if (
							sxi < 0 || W <= sxi ||
							syi < 0 || H <= syi ||
							statusNew.Map[syi][sxi] != 0
							)
							return;

						statusNew.Map[syi][sxi] = c;
					}
				}
			}
			for (int i = 0; i < Tree.Count; i++)
				if (Comp(statusNew, Tree[i]) == 0)
					return;

			Tree.Add(statusNew);

			{
				Status s = statusNew;

				if (
					s.Map[3][1] == s.Map[3][2] &&
					s.Map[3][1] == s.Map[4][1] &&
					s.Map[3][1] == s.Map[4][2]
					)
					throw new Bingo();
			}
		}

		private void PrintAnswer(List<Status> route)
		{
			for (int index = 0; index < route.Count; index++)
			{
				for (int y = 0; y < H; y++)
				{
					for (int x = 0; x < W; x++)
					{
						Console.Write("\t" + route[index].Map[y][x]);
					}
					Console.WriteLine("");
				}
				Console.WriteLine("");
			}

			// ----

			using (StreamWriter writer = new StreamWriter(@"C:\temp\t20190513_Test0001_answer.html", false, Encoding.UTF8))
			{
				writer.WriteLine("<html>");
				writer.WriteLine("<head>");
				writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
				writer.WriteLine("</head>");
				writer.WriteLine("<body>");

				for (int index = 0; index < route.Count; index++)
				{
					Status status = route[index];

					writer.WriteLine(index + ".");
					writer.WriteLine("<table>");

					for (int y = 0; y < H; y++)
					{
						writer.WriteLine("<tr>");

						for (int x = 0; x < W; x++)
						{
							writer.WriteLine("<td width=\"30\" height=\"30\" bgcolor=\"" + CellToHtmlColor(status.Map[y][x]) + "\" />");
						}
						writer.WriteLine("</tr>");
					}
					writer.WriteLine("</table>");
				}
				writer.WriteLine("</body>");
				writer.WriteLine("</html>");
			}
		}

		private string CellToHtmlColor(int cell)
		{
			switch (cell / 100)
			{
				case 0: return "#eee";
				case 1: return "#00c";
				case 2: return "#aa0";
				case 3: return "#0c0";
				case 4: return "#c00";

				default:
					throw null; // never
			}
			throw null; // never
		}
	}
}
