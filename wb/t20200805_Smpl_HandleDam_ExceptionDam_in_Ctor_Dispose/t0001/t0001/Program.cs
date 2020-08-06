using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{db4622c0-0576-49c4-b8aa-2ee0e0c2f356}";
		public const string APP_TITLE = "t0001";

		static void Main(string[] args)
		{
			ProcMain.CUIMain(new Program().Main2, APP_IDENT, APP_TITLE);

#if DEBUG
			//if (ProcMain.CUIError)
			{
				Console.WriteLine("Press ENTER.");
				Console.ReadLine();
			}
#endif
		}

		private void Main2(ArgsReader ar)
		{
			//Test01();
			Test02();
		}

		private void Test01()
		{
			using (ManyHandleClass0001 h = new ManyHandleClass0001())
			{
				h.Write01();
			}
		}

		private void Test02()
		{
			for (int c = 0; c < 1000; c++)
			{
				try
				{
					using (new ManyHandleClass0002())
					{
						Console.WriteLine("handles(5): " + RandomizedErrorHandle.OpenedHandleCount);

						if (RandomizedErrorHandle.OpenedHandleCount != 5)
							throw null; // bugged !!!
					}
				}
				catch (Exception e)
				{
					Console.WriteLine("messages: (");

					while (e != null)
					{
						Console.WriteLine(e.Message);
						e = e.InnerException;
					}
					Console.WriteLine(")");
				}

				Console.WriteLine("handles(0): " + RandomizedErrorHandle.OpenedHandleCount);

				if (RandomizedErrorHandle.OpenedHandleCount != 0)
					throw null; // bugged !!!
			}
		}
	}
}
