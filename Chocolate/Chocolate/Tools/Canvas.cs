﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Charlotte.Tools
{
	public class Canvas
	{
		private uint[] Dots;
		private int Width;

		public Canvas(int w, int h)
		{
			if (CanvasTools.IsFairImageSize(w, h) == false)
				throw new ArgumentException();

			this.Dots = new uint[w * h];
			this.Width = w;
		}

		public Canvas(string file)
			: this(File.ReadAllBytes(file))
		{ }

		public Canvas(byte[] raw)
			: this(CanvasTools.GetImage(raw))
		{ }

		public Canvas(Image image)
		{
			int w = image.Width;
			int h = image.Height;

			this.Dots = new uint[w * h];
			this.Width = w;

			using (Bitmap bmp = new Bitmap(image))
			{
				for (int x = 0; x < w; x++)
				{
					for (int y = 0; y < h; y++)
					{
						this[x, y] = bmp.GetPixel(x, y);
					}
				}
			}
		}

		public int GetWidth()
		{
			return this.Width;
		}

		public int GetHeight()
		{
			return this.Dots.Length / this.Width;
		}

		private bool IsFairPoint(int x, int y)
		{
			return 0 <= x && x < this.GetWidth() && 0 <= y && y < this.GetHeight();
		}

		public static Color DEFAULT_COLOR = Color.FromArgb(255, 255, 255, 0);

		public Color this[int x, int y]
		{
			get
			{
				if (this.IsFairPoint(x, y))
				{
					return Color.FromArgb((int)this.Dots[x + y * this.Width]);
				}
				else
				{
					return DEFAULT_COLOR;
				}
			}

			set
			{
				if (this.IsFairPoint(x, y))
				{
					this.Dots[x + y * this.Width] = (uint)value.ToArgb();
				}
			}
		}

		public void Put(int x, int y, Color color)
		{
			this[x, y] = CanvasTools.Cover(this[x, y], color);
		}

		public Image GetImage()
		{
			Bitmap image = new Bitmap(this.GetWidth(), this.GetHeight());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					image.SetPixel(x, y, this[x, y]);
				}
			}
			return image;
		}

		public byte[] GetBytes()
		{
			return CanvasTools.GetBytes(this.GetImage());
		}

		public byte[] GetBytes(ImageFormat format, int quality = -1)
		{
			return CanvasTools.GetBytes(this.GetImage(), format, quality);
		}

		public void Save(string file)
		{
			File.WriteAllBytes(file, this.GetBytes());
		}

		public void Save(string file, ImageFormat format, int quality = -1)
		{
			File.WriteAllBytes(file, this.GetBytes(format, quality));
		}

		public Canvas2 ToCanvas2()
		{
			return new Canvas2(this.GetImage());
		}

		public void Paste(Canvas prm, int l = 0, int t = 0)
		{
			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					this[x, y] = prm[x, y];
				}
			}
		}

		public void Cover(Canvas prm, int l = 0, int t = 0)
		{
			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					this.Put(x, y, prm[x, y]);
				}
			}
		}

		public Canvas Copy()
		{
			return this.Copy(0, 0, this.GetWidth(), this.GetHeight());
		}

		public Canvas Copy(int l, int t, int w, int h)
		{
			Canvas ret = new Canvas(w, h);

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret[x, y] = this[l + x, t + y];
				}
			}
			return ret;
		}

		public void Fill(Color color)
		{
			this.FillRect(color, 0, 0, this.GetWidth(), this.GetHeight());
		}

		public void FillRect(Color color, int l, int t, int w, int h)
		{
			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					this[l + x, t + y] = color;
				}
			}
		}

		public void Cover(Color color)
		{
			this.CoverRect(color, 0, 0, this.GetWidth(), this.GetHeight());
		}

		public void CoverRect(Color color, int l, int t, int w, int h)
		{
			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					this.Put(l + x, t + y, color);
				}
			}
		}

		public void DrawCircle(Color color, double centerX, double centerY, double r, double r2 = 0.0)
		{
			const int LV = 16;

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					int inside = 0;

					for (int xc = 0; xc < LV; xc++)
					{
						for (int yc = 0; yc < LV; yc++)
						{
							double xx = x - 0.5 + (xc + 0.5) / LV;
							double yy = y - 0.5 + (yc + 0.5) / LV;

							xx -= centerX;
							yy -= centerY;

							double rr = xx * xx + yy * yy;

							if (r2 * r2 <= rr && rr <= r * r)
							{
								inside++;
							}
						}
					}

					this.Put(x, y, Color.FromArgb(
						(int)(color.A * inside / (double)(LV * LV) + 0.5),
						color.R,
						color.G,
						color.B
						));
				}
			}
		}

		public Canvas Twist()
		{
			Canvas ret = new Canvas(this.GetHeight(), this.GetWidth());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret[y, x] = this[x, y];
				}
			}
			return ret;
		}

		public Canvas VTurn()
		{
			Canvas ret = new Canvas(this.GetWidth(), this.GetHeight());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret[x, this.GetHeight() - 1 - y] = this[x, y];
				}
			}
			return ret;
		}

		public Canvas HTurn()
		{
			Canvas ret = new Canvas(this.GetWidth(), this.GetHeight());

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					ret[this.GetWidth() - 1 - x, y] = this[x, y];
				}
			}
			return ret;
		}

		public Canvas Rotate(int degree)
		{
			degree %= 360;
			degree += 360;
			degree %= 360;

			switch (degree)
			{
				case 0:
					//return this;
					return this.Copy();

				case 90:
					return this.VTurn().Twist();

				case 180:
					return this.VTurn().HTurn();

				case 270:
					return this.Twist().VTurn();

				default:
					throw new ArgumentException();
			}
		}

		public Canvas CutoutUnmatch(Predicate<Color> match)
		{
			int l = this.GetWidth();
			int t = this.GetHeight();
			int r = -1;
			int b = -1;

			for (int x = 0; x < this.GetWidth(); x++)
			{
				for (int y = 0; y < this.GetHeight(); y++)
				{
					if (match(this[x, y]))
					{
						l = Math.Min(l, x);
						t = Math.Min(t, y);
						r = Math.Max(r, x);
						b = Math.Max(b, y);
					}
				}
			}
			if (r == -1)
			{
				l = 0;
				t = 0;
				r = this.GetWidth();
				b = this.GetHeight();
			}
			else
			{
				r++;
				b++;
			}
			return this.Copy(l, t, r - l, b - t);
		}
	}
}
