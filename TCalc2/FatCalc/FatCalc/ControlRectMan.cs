using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	public class ControlRectMan
	{
		public Control RefCtrl;
		public RectData RefBasePos;
		public RectData Rate;
		public Control Ctrl;
		public RectData BasePos;

		public ControlRectMan(Control refCtrl, double l, double t, double w, double h, Control ctrl)
			: this(refCtrl, new RectData(l, t, w, h), ctrl)
		{ }

		public ControlRectMan(Control refCtrl, RectData rate, Control ctrl)
		{
			this.RefCtrl = refCtrl;
			this.RefBasePos = new RectData(refCtrl);
			this.Rate = rate;
			this.Ctrl = ctrl;
			this.BasePos = new RectData(ctrl);

			refCtrl.Resize += delegate
			{
				this.RefResized();
			};
		}

		public void RefResized()
		{
			double diffX = this.RefCtrl.Width - this.RefBasePos.W;
			double diffY = this.RefCtrl.Height - this.RefBasePos.H;

			if (this.Rate.L != 0.0)
				this.Ctrl.Left = IntTools.ToInt(this.BasePos.L + diffX * this.Rate.L);

			if (this.Rate.T != 0.0)
				this.Ctrl.Top = IntTools.ToInt(this.BasePos.T + diffY * this.Rate.T);

			if (this.Rate.W != 0.0)
				this.Ctrl.Width = IntTools.ToInt(this.BasePos.W + diffX * this.Rate.W);

			if (this.Rate.H != 0.0)
				this.Ctrl.Height = IntTools.ToInt(this.BasePos.H + diffY * this.Rate.H);
		}

		public class RectData
		{
			public double L;
			public double T;
			public double W;
			public double H;

			public RectData(Control ctrl)
			{
				L = ctrl.Left;
				T = ctrl.Top;
				W = ctrl.Width;
				H = ctrl.Height;
			}

			public RectData(double l, double t, double w, double h)
			{
				L = l;
				T = t;
				W = w;
				H = h;
			}
		}
	}
}
