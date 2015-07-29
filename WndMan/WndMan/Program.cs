using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace WndMan
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				Main2(args);
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
		}

		private static void Main2(string[] args)
		{
			Queue<string> argq = new Queue<string>();

			foreach (string arg in args)
			{
				argq.Enqueue(arg);
			}

			while (1 <= argq.Count)
			{
				string command = argq.Dequeue().ToUpper();

				if (command == "/W")
				{
					int millis = int.Parse(argq.Dequeue());

					Thread.Sleep(millis);
					continue;
				}
				if (command == "/L")
				{
					ControlTree.GetControlTree().DoPrintChildren();
					continue;
				}
				if (command == "/C")
				{
					IntPtr hWnd = (IntPtr)int.Parse(argq.Dequeue());

					SndMsgTools.LeftClick(hWnd);
					continue;
				}
				if (command == "/S")
				{
					IntPtr hWnd = (IntPtr)int.Parse(argq.Dequeue());
					string text = argq.Dequeue();

					SetTextTools.SetText(hWnd, text);
					continue;
				}
				if (command == "/T")
				{
					IntPtr hWnd = (IntPtr)int.Parse(argq.Dequeue());

					WinTools.ToTop(hWnd);
					continue;
				}
				if (command == "/LC")
				{
					IntPtr hWnd = (IntPtr)int.Parse(argq.Dequeue());
					ControlTree ct = ControlTree.GetControlTree().Find(hWnd);

					WinTools.ToTop(ct.GetWindow().Info.HWnd);

					MouseTools.POINT pt = ct.GetCenterPoint();

					MouseTools.LeftClick_KeepPos(pt);
					continue;
				}
			}
		}
	}
}
