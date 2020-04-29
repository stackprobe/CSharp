using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing.Imaging;
using System.Drawing;

namespace Charlotte.Tests.Tools
{
	public class Canvas2Test
	{
		public void Test01()
		{
			new Canvas2(@"C:\var\PaceMap.bmp").Save(@"C:\temp\PaceMap.jpg", ImageFormat.Jpeg, 100);
		}

		public void Test02()
		{
			double RATE = 3.3;

			Canvas2 canvas = new Canvas2(@"C:\var\B-aCrH0CUAA5wtn.jpg");

			int dest_w = (int)(canvas.GetWidth() * RATE);
			int dest_h = (int)(canvas.GetHeight() * RATE);

			Canvas2 dest = new Canvas2(dest_w, dest_h);

			using (Graphics g = dest.GetGraphics())
			{
				g.DrawImage(canvas.GetImage(), 0, 0, dest.GetWidth(), dest.GetHeight());
			}
			dest.Save(@"C:\temp\1.png");

#if false // test test test
			using (Graphics g = dest.GetGraphics())
			{
				g.DrawImage(canvas.GetImage(), 0f, 0f, (float)dest.GetWidth(), (float)dest.GetHeight());
			}
			dest.Save(@"C:\temp\1f.png");

			using (Graphics g = dest.GetGraphics())
			{
				g.DrawImage(canvas.GetImage(), -50f, -50f, (float)(dest.GetWidth() + 100), (float)(dest.GetHeight() + 100));
			}
			dest.Save(@"C:\temp\1f2.png");
#endif

			ProcessTools.Batch(new string[]
			{
				string.Format(@"C:\app\Kit\ImgTools\ImgTools.exe /RF C:\var\B-aCrH0CUAA5wtn.jpg /WF C:\temp\2.png /E {0} {1}", dest_w, dest_h),
			});
		}

		public void Test03()
		{
			Canvas2 canvas = new Canvas2(@"C:\var\B-aCrH0CUAA5wtn.jpg");

			for (int c = 1; ; c++)
			{
				canvas.Save(string.Format(@"C:\temp\{0:D2}.png", c));

				if (99 <= c)
					break;

				// ぼかし - 横方向
				{
					Canvas2 dest = new Canvas2(canvas.GetWidth(), canvas.GetHeight());

					using (Graphics g = dest.GetGraphics())
					{
						g.DrawImage(canvas.GetImage(), -1, 0, canvas.GetWidth(), canvas.GetHeight());

						{
							ColorMatrix cm = new ColorMatrix();
							cm.Matrix00 = 1;
							cm.Matrix11 = 1;
							cm.Matrix22 = 1;
							cm.Matrix33 = 0.5f;
							cm.Matrix44 = 1;
							ImageAttributes ia = new ImageAttributes();
							ia.SetColorMatrix(cm);

							g.DrawImage(
								canvas.GetImage(),
								new Rectangle(0, 0, canvas.GetWidth(), canvas.GetHeight()),
								0, 0, canvas.GetWidth(), canvas.GetHeight(),
								GraphicsUnit.Pixel,
								ia
								);
						}

						{
							ColorMatrix cm = new ColorMatrix();
							cm.Matrix00 = 1;
							cm.Matrix11 = 1;
							cm.Matrix22 = 1;
							cm.Matrix33 = (float)(1.0 / 3.0);
							cm.Matrix44 = 1;
							ImageAttributes ia = new ImageAttributes();
							ia.SetColorMatrix(cm);

							g.DrawImage(
								canvas.GetImage(),
								new Rectangle(1, 0, canvas.GetWidth(), canvas.GetHeight()),
								0, 0, canvas.GetWidth(), canvas.GetHeight(),
								GraphicsUnit.Pixel,
								ia
								);
						}
					}
					canvas = dest;
				}

				// ぼかし - 縦方向
				{
					Canvas2 dest = new Canvas2(canvas.GetWidth(), canvas.GetHeight());

					using (Graphics g = dest.GetGraphics())
					{
						g.DrawImage(canvas.GetImage(), 0, -1, canvas.GetWidth(), canvas.GetHeight());

						{
							ColorMatrix cm = new ColorMatrix();
							cm.Matrix00 = 1;
							cm.Matrix11 = 1;
							cm.Matrix22 = 1;
							cm.Matrix33 = 0.5f;
							cm.Matrix44 = 1;
							ImageAttributes ia = new ImageAttributes();
							ia.SetColorMatrix(cm);

							g.DrawImage(
								canvas.GetImage(),
								new Rectangle(0, 0, canvas.GetWidth(), canvas.GetHeight()),
								0, 0, canvas.GetWidth(), canvas.GetHeight(),
								GraphicsUnit.Pixel,
								ia
								);
						}

						{
							ColorMatrix cm = new ColorMatrix();
							cm.Matrix00 = 1;
							cm.Matrix11 = 1;
							cm.Matrix22 = 1;
							cm.Matrix33 = (float)(1.0 / 3.0);
							cm.Matrix44 = 1;
							ImageAttributes ia = new ImageAttributes();
							ia.SetColorMatrix(cm);

							g.DrawImage(
								canvas.GetImage(),
								new Rectangle(0, 1, canvas.GetWidth(), canvas.GetHeight()),
								0, 0, canvas.GetWidth(), canvas.GetHeight(),
								GraphicsUnit.Pixel,
								ia
								);
						}
					}
					canvas = dest;
				}
			}
		}
	}
}
