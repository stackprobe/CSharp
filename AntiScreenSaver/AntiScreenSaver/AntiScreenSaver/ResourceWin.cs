using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Charlotte
{
	public partial class ResourceWin : Form
	{
		public ResourceWin()
		{
			InitializeComponent();

			Gnd.IGreen = GetIcon(this.IGreen.Image);
			Gnd.IYellow = GetIcon(this.IYellow.Image);
			Gnd.IRed = GetIcon(this.IRed.Image);
		}

		private static Icon GetIcon(Image image)
		{
			return Icon.FromHandle(((Bitmap)image).GetHicon());
		}

		private void ResourceWin_Load(object sender, EventArgs e)
		{
			// noop
		}
	}
}
