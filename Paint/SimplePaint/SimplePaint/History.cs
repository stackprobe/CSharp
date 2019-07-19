using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace Charlotte
{
	public class History : IDisposable
	{
		private WorkingDir WD = new WorkingDir();
		private long HeadIndex = 0;
		private long TailIndex = 0;

		public void Save(Image image)
		{
			image.Save(this.GetImageFile(this.HeadIndex++), ImageFormat.Png);

			if (this.TailIndex + Consts.HISTORY_MAX < this.HeadIndex)
				FileTools.Delete(this.GetImageFile(this.TailIndex++));
		}

		public Image Unsave()
		{
			if (this.HeadIndex == this.TailIndex)
				return null;

			string file = this.GetImageFile(--this.HeadIndex);
			Image image;
			using (FileStream reader = new FileStream(file, FileMode.Open, FileAccess.Read))
			{
				image = Image.FromStream(reader);
			}
			FileTools.Delete(file);
			return image;
		}

		private string GetImageFile(long index)
		{
			return this.WD.GetPath(index + ".png");
		}

		public int GetCount()
		{
			return (int)(this.HeadIndex - this.TailIndex);
		}

		public void Dispose()
		{
			if (this.WD != null)
			{
				this.WD.Dispose();
				this.WD = null;
			}
		}
	}
}
