using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.IO.MemoryMappedFiles;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{0531504d-dda1-45ef-b833-d5e4654b693d}";
		public const string APP_TITLE = "Client";

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
			byte[] data = Encoding.ASCII.GetBytes(ar.NextArg());

			using (MemoryMappedFile mmf = MemoryMappedFile.OpenExisting("SM-Test"))
			using (MemoryMappedViewAccessor mmva = mmf.CreateViewAccessor())
			{
				mmva.WriteArray(0, data, 0, data.Length);
			}
		}
	}
}
