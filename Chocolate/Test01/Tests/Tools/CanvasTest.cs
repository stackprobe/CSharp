using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Charlotte.Tools;

namespace Charlotte.Tests.Tools
{
	public class CanvasTest
	{
		public void Test01()
		{
			Canvas canvas = new Canvas(800, 600);

			canvas.Fill(Color.Blue);
			canvas.DrawCircle(Color.Orange, 800 - 0.5, 600 - 0.5, 600);

			{
				Canvas2 c2 = canvas.ToCanvas2();

				c2.DrawString("Canvas", new Font("メイリオ", 100F, FontStyle.Regular), Color.White, 400, 300);

				canvas = c2.ToCanvas();
			}

			canvas.Save(@"C:\temp\CanvasTest.png");
		}
	}
}
