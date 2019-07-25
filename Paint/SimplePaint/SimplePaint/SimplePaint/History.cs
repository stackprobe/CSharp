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
		private long RedoIndex = 0;

		public void Clear()
		{
			this.HeadIndex = 0;
			this.TailIndex = 0;
			this.RedoIndex = 0;
		}

		public void Save(Image image, bool keepRedo = false)
		{
			image.Save(this.GetImageFile(this.HeadIndex++), ImageFormat.Png);

			if (this.TailIndex + Consts.HISTORY_MAX < this.HeadIndex)
				FileTools.Delete(this.GetImageFile(this.TailIndex++));

			if (keepRedo == false)
				this.RedoIndex = 0;
		}

		public Image Undo(Image image)
		{
			if (this.HeadIndex == this.TailIndex)
				return null;

			SaveForRedo(image);

			string file = this.GetImageFile(--this.HeadIndex);
			image = new Canvas2(file).GetImage();
			FileTools.Delete(file);
			return image;
		}

		private void SaveForRedo(Image image)
		{
			image.Save(this.GetImageFileForRedo(this.RedoIndex++), ImageFormat.Png);
		}

		public Image Redo(Image image)
		{
			if (this.RedoIndex <= 0)
				return null;

			this.Save(image, true);

			string file = this.GetImageFileForRedo(--this.RedoIndex);
			image = new Canvas2(file).GetImage();
			FileTools.Delete(file);
			return image;
		}

		private string GetImageFile(long index)
		{
			return this.WD.GetPath(index + ".png");
		}

		private string GetImageFileForRedo(long index)
		{
			return this.WD.GetPath("Redo_" + index + ".png");
		}

		public int GetCount()
		{
			return (int)(this.HeadIndex - this.TailIndex);
		}

		public int GetCountForRedo()
		{
			return (int)(this.RedoIndex);
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
