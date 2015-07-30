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

		/// <summary>
		/// ルートはウィンドウ・コントロールではない。
		/// ルートの直下がウィンドウになる。
		/// ウィンドウの配下は全部コントロール(多分..
		/// </summary>
		/// <returns></returns>
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

		// ---- DoPrint ----

		public void DoPrint(string indent = "")
		{
			EWndTools.ControlInfo ci = this.Info;

			Console.WriteLine(indent + "Title: " + ci.Title);
			Console.WriteLine(indent + "ClassName: " + ci.ClassName);
			Console.WriteLine(indent + "Text: " + ci.Text);
			Console.WriteLine(indent + "Rect: " + ci.Rect.L + ", " + ci.Rect.T + ", " + ci.Rect.R + ", " + ci.Rect.B);
			Console.WriteLine(indent + "HWnd: " + ci.HWnd);

			Console.WriteLine(indent + "{");

			this.DoPrintChildren(indent + "\t");

			Console.WriteLine(indent + "}");
		}

		public void DoPrintChildren(string indent = "")
		{
			foreach (ControlTree ct in this.Children)
			{
				ct.DoPrint(indent);
			}
		}

		public void DoPrint_2()
		{
			EWndTools.ControlInfo ci = this.Info;

			Console.WriteLine("----");
			Console.WriteLine("" + ci.HWnd);
			Console.WriteLine(this.GetTitlePath());
			Console.WriteLine(this.GetClassNamePath());
			//Console.WriteLine(ci.Text);

			MouseTools.POINT pt = ci.GetCenterPoint();

			Console.WriteLine("" + pt.X);
			Console.WriteLine("" + pt.Y);

			this.DoPrintChildren_2();
		}

		public void DoPrintChildren_2()
		{
			foreach (ControlTree ct in this.Children)
			{
				ct.DoPrint_2();
			}
		}

		// ---- GetAllUser ----

		public ControlTree Find(IntPtr hWnd)
		{
			foreach (ControlTree ct in this.GetAllChildren())
				if (ct.Info.HWnd == hWnd)
					return ct;

			return null;
		}

		// ---- GetAll ----

		private List<ControlTree> GetAll()
		{
			List<ControlTree> dest = new List<ControlTree>();

			this.AddAll(dest);
			return dest;
		}

		private List<ControlTree> GetAllChildren()
		{
			List<ControlTree> dest = new List<ControlTree>();

			this.AddAllChildren(dest);
			return dest;
		}

		private void AddAll(List<ControlTree> dest)
		{
			dest.Add(this);
			this.AddAllChildren(dest);
		}

		private void AddAllChildren(List<ControlTree> dest)
		{
			foreach (ControlTree ct in this.Children)
				ct.AddAll(dest);
		}

		// ---- GetPathUser ----

		public string GetTitlePath()
		{
			List<string> list = new List<string>();

			foreach (EWndTools.ControlInfo ci in this.GetPath())
				list.Add(ci.Title);

			return string.Join(":", list);
		}

		public string GetClassNamePath()
		{
			List<string> list = new List<string>();

			foreach (EWndTools.ControlInfo ci in this.GetPath())
				list.Add(ci.ClassName);

			return string.Join(":", list);
		}

		// ---- GetPath ----

		private List<EWndTools.ControlInfo> GetPath()
		{
			List<EWndTools.ControlInfo> list = new List<EWndTools.ControlInfo>();
			ControlTree ct = this;

			while (ct.Parent != null) // ? ! ルート
			{
				list.Add(ct.Info);
				ct = ct.Parent;
			}
			list.Reverse();
			return list;
		}

		// ----

		public ControlTree GetWindow()
		{
			ControlTree ct = this;

			while (ct.Parent.Parent != null) // ? ! ルートの1つ下
				ct = ct.Parent;

			return ct;
		}
	}
}
