using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Charlotte.Tools;

namespace Charlotte
{
	public static class ShowAnswer
	{
		public static void Perform(Moment[] route)
		{
			using (StreamWriter writer = new StreamWriter(@"C:\temp\route.html", false, Encoding.UTF8))
			{
				writer.WriteLine("<html>");
				writer.WriteLine("<head>");
				writer.WriteLine("<meta http-equiv=\"Content-Type\" content=\"text/html; charset=utf-8\" />");
				writer.WriteLine("</head>");
				writer.WriteLine("<body>");

				for (int index = 0; index < route.Length; index++)
				{
					Moment curr = route[index];

					writer.WriteLine(index + ".");
					writer.WriteLine(@"<table style=""border-collapse: collapse;"">");

					for (int y = 0; y < curr.Map.H; y++)
					{
						writer.WriteLine("<tr>");

						for (int x = 0; x < curr.Map.W; x++)
						{
							string[] pDTkns = GetPrintData(curr.Map.Table[x][y].State, x == curr.X && y == curr.Y).Split(':');

							writer.WriteLine(string.Format(
								@"<td
width=""30""
height=""30""
align=""center""
valign=""middle""
style=""font: normal 20px Meiryo; background-color: #{0}; color: #{1};"">{2}</td>",
								pDTkns[0],
								pDTkns[1],
								pDTkns[2]
								));
						}
						writer.WriteLine("</tr>");
					}
					writer.WriteLine("</table>");
				}
				writer.WriteLine("</body>");
				writer.WriteLine("</html>");
			}
			ProcessTools.Batch(new string[] { @"START C:\temp\route.html" });
		}

		private static string GetPrintData(Cell.State_e state, bool katasukeIsHere)
		{
			switch (state)
			{
				case Cell.State_e.EMPTY:
					return "000:fff:" + (katasukeIsHere ? "○" : "");

				case Cell.State_e.WALL:
					return "a00:fff:";

				case Cell.State_e.BOX:
					return "0aa:fff:";

				case Cell.State_e.POINT:
					return "000:fff:" + (katasukeIsHere ? "●" : "・");

				case Cell.State_e.POINT_BOX:
					return "0ff:fff:";

				default:
					throw null;
			}
		}
	}
}
