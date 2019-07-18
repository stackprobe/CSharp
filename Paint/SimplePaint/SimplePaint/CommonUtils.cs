using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;

namespace Charlotte
{
	public class CommonUtils
	{
		public static ImageFormat ExtToImageFormat(string ext)
		{
			switch (ext.ToLower())
			{
				case ".bmp":
					return ImageFormat.Bmp;

				case ".gif":
					return ImageFormat.Gif;

				case ".jpg":
				case ".jpeg":
					return ImageFormat.Jpeg;

				case ".png":
					return ImageFormat.Png;

				default:
					throw new Exception("不明な画像ファイルの拡張子");
			}
		}
	}
}
