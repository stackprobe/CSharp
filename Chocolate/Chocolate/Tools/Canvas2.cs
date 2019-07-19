﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace Charlotte.Tools
{
	public class Canvas2
	{
		private Image Image;

		public Canvas2(int w, int h)
		{
			if (CanvasTools.IsFairImageSize(w, h) == false)
			{
				throw new ArgumentException();
			}
			this.Image = new Bitmap(w, h);
		}

		public Canvas2(String file)
			: this(File.ReadAllBytes(file))
		{ }

		public Canvas2(byte[] raw)
			: this(CanvasTools.GetImage(raw))
		{ }

		public Canvas2(Image image)
		{
			this.Image = image;
		}

		public int GetWidth()
		{
			return this.Image.Width;
		}

		public int GetHeight()
		{
			return this.Image.Height;
		}

		public Image GetImage()
		{
			return this.Image;
		}

		public byte[] GetBytes()
		{
			return CanvasTools.GetBytes(this.Image);
		}

		public byte[] GetBytes(ImageFormat format, int quality = -1)
		{
			return CanvasTools.GetBytes(this.Image, format, quality);
		}

		public void Save(string file)
		{
			File.WriteAllBytes(file, this.GetBytes());
		}

		public void Save(string file, ImageFormat format)
		{
			File.WriteAllBytes(file, this.GetBytes(format));
		}

		public Canvas ToCanvas()
		{
			return new Canvas(this.Image);
		}

		public Graphics GetGraphics()
		{
			Graphics g = Graphics.FromImage(this.Image);

			g.TextRenderingHint = TextRenderingHint.AntiAlias;
			g.SmoothingMode = SmoothingMode.AntiAlias;

			return g;
		}

		public void DrawString(String str, Font font, Color color, int x, int y, double xRate = -0.5, double yRate = -0.5)
		{
			using (Graphics g = this.GetGraphics())
			{
				SizeF size = g.MeasureString(str, font);

				g.DrawString(str, font, new SolidBrush(color), (float)(x + size.Width * xRate), (float)(y + size.Height * yRate));
			}
		}
	}
}
