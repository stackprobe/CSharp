using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using Charlotte.Tools;
using System.IO.MemoryMappedFiles;
using System.Threading;

namespace Charlotte
{
	class Program
	{
		public const string APP_IDENT = "{e771f477-60c0-4c63-9a13-34b3b0d2939b}";
		public const string APP_TITLE = "Server";

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

		private const int MEM_SIZE = 16;

		private void Main2(ArgsReader ar)
		{
			using (MemoryMappedFile mmf = MemoryMappedFile.CreateNew("SM-Test", MEM_SIZE))
			{
				byte[] buff = new byte[MEM_SIZE];

				while (Console.KeyAvailable == false)
				{
					using (MemoryMappedViewAccessor mmva = mmf.CreateViewAccessor())
					{
						//Console.WriteLine("*1");
						//Console.ReadLine();
						//Console.WriteLine("*2");

						mmva.ReadArray(0, buff, 0, 16);

						//Console.WriteLine("*3");
						//Console.ReadLine();
						//Console.WriteLine("*4");
					}
					foreach (byte chr in buff)
					{
						Console.Write(chr.ToString("x2"));
					}
					Console.WriteLine("");

					//Thread.Sleep(100);
				}
			}
		}
	}
}
