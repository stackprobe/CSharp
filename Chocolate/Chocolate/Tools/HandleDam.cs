using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Charlotte.Tools
{
	public class HandleDam
	{
		public static T Section_Get<T>(Func<HandleDam, T> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				return routine(hDam);
			}
			finally
			{
				hDam.Burst();
			}
		}

		public static void Section(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			finally
			{
				hDam.Burst();
			}
		}

		public static T Transaction_Get<T>(Func<HandleDam, T> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				return routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
				throw null; // never
			}
		}

		public static void Transaction(Action<HandleDam> routine)
		{
			HandleDam hDam = new HandleDam();
			try
			{
				routine(hDam);
			}
			catch (Exception e)
			{
				hDam.Burst(e);
				//throw null; // never
			}
		}

		private List<IDisposable> Handles = new List<IDisposable>();

		public T Add<T>(T handle) where T : IDisposable
		{
			this.Handles.Add(handle);
			return handle;
		}

		public void Burst()
		{
			ExceptionDam.Section(eDam =>
			{
				this.Burst(eDam);
			});
		}

		public void Burst(Exception e)
		{
			ExceptionDam.Section(eDam =>
			{
				eDam.Add(e);
				this.Burst(eDam);
			});
		}

		private void Burst(ExceptionDam eDam)
		{
			for (int index = this.Handles.Count - 1; 0 <= index; index--)
				eDam.Invoke(() => this.Handles[index].Dispose());

			this.Handles.Clear();
		}
	}
}
