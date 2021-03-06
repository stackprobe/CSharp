﻿using System;
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

			Ground.Icons[0] = GetIcon(this.pictureBox1.Image);
			Ground.Icons[1] = GetIcon(this.pictureBox2.Image);
			Ground.Icons[2] = GetIcon(this.pictureBox3.Image);
			Ground.Icons[3] = GetIcon(this.pictureBox4.Image);
			Ground.Icons[4] = GetIcon(this.pictureBox5.Image);
			Ground.Icons[5] = GetIcon(this.pictureBox6.Image);
			Ground.Icons[6] = GetIcon(this.pictureBox7.Image);
			Ground.Icons[7] = GetIcon(this.pictureBox8.Image);
			Ground.Icons[8] = GetIcon(this.pictureBox9.Image);
			Ground.Icons[9] = GetIcon(this.pictureBox10.Image);
			Ground.Icons[10] = GetIcon(this.pictureBox11.Image);
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
