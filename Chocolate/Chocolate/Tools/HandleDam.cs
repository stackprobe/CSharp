using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HandleDam
	{
		public static void Section(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
			}
		}

		private List<IDisposable> Handles = new List<IDisposable>();

		public T Add<T>(T handle) where T : IDisposable
		{
			this.Handles.Add(handle);
			return handle;
		}

		public void Burst(Exception e)
		{
			ExceptionDam.Section(eDam =>
			{
				eDam.Add(e);

				for (int index = this.Handles.Count - 1; 0 <= index; index--)
					eDam.Invoke(() => this.Handles[index].Dispose());

				this.Handles.Clear();
			});
		}
	}
}
