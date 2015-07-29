using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WndMan
{
	public class ControlTree
	{
		public EWndTools.ControlInfo Info;
		public List<ControlTree> Children = new List<ControlTree>();
		public ControlTree Parent;

		public static ControlTree GetControlTree()
		{
			ControlTree root = new ControlTree();

			EWndTools.FindWindows(delegate(EWndTools.ControlInfo ci)
			{
				ControlTree ct = new ControlTree();

				ct.Parent = root;
				ct.Info = ci;

				root.Children.Add(ct);

				root = ct;
			},
			delegate
			{
				root = root.Parent;
			});

			return root;
		}

		public void DoPrintChildren(string indent = "")
		{
			foreach (ControlTree ct in this.Children)
			{
				ct.DoPrint(indent);
			}
		}

		public void DoPrint(string indent = "")
		{
			EWndTools.ControlInfo ci = this.Info;

			Console.WriteLine(indent + "Title: " + ci.Title);
			Console.WriteLine(indent + "ClassName: " + ci.ClassName);
			Console.WriteLine(indent + "Rect: " + ci.Rect.L + ", " + ci.Rect.T + ", " + ci.Rect.R + ", " + ci.Rect.B);
			Console.WriteLine(indent + "HWnd: " + ci.HWnd);

			Console.WriteLine(indent + "{");

			this.DoPrintChildren(indent + "\t");

			Console.WriteLine(indent + "}");
		}

		public ControlTree Find(IntPtr hWnd)
		{
			foreach (ControlTree ct in this.GetAllChildren())
				if (ct.Info.HWnd == hWnd)
					return ct;

			return null;
		}

		public List<ControlTree> GetAllChildren()
		{
			List<ControlTree> dest = new List<ControlTree>();

			this.AddAllChildren(dest);
			return dest;
		}

		private void AddAllChildren(List<ControlTree> dest)
		{
			foreach (ControlTree ct in this.Children)
				ct.AddAll(dest);
		}

		private void AddAll(List<ControlTree> dest)
		{
			dest.Add(this);
			this.AddAllChildren(dest);
		}

		public MouseTools.POINT GetCenterPoint()
		{
			MouseTools.POINT pt;

			pt.X = (this.Info.Rect.L + this.Info.Rect.R) / 2;
			pt.Y = (this.Info.Rect.T + this.Info.Rect.B) / 2;

			return pt;
		}

		public ControlTree GetWindow()
		{
			ControlTree ct = this;

			while(ct.Parent.Parent != null) // ? ! ルートの1つ下
				ct = ct.Parent;

			return ct;
		}
	}
}
