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
		public const string APP_IDENT = "{548dd95d-5520-4b9d-88d1-89034f551486}";
		public const string APP_TITLE = "MessageTerminal";

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
			using (SerialPortTerminal spt = new SerialPortTerminal(ar))
			{
				if (ar.ArgIs("Client"))
				{
					Console.WriteLine("[クライアントモード]空行を入力すると終了します。");

					for (; ; )
					{
						string line = Console.ReadLine();

						if (line == "")
							break;

						spt.WriteLine(line);
					}
				}
				else if (ar.ArgIs("Server"))
				{
					Console.WriteLine("[サーバーモード]何かキーを押すと終了します。");

					while (Console.KeyAvailable == false)
					{
						try
						{
							string line = spt.ReadLine();

							Console.WriteLine(line);
						}
						catch (TimeoutException)
						{ }
						catch (Exception e)
						{
							Console.WriteLine(e);
						}
					}
				}
				else
				{
					throw new Exception("不明なモード");
				}
			}
		}
	}
}
