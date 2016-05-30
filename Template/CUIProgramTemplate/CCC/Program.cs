using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				if (1 <= args.Length && args[0].ToUpper() == "//R")
				{
					Main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					Main2(args);
				}
			}
			catch (Exception e)
			{
				Console.WriteLine(e);
			}
#if DEBUG
			Console.WriteLine("Press ENTER");
			Console.ReadLine();
#endif
		}

		private static Queue<string> _argq;

		private static bool ArgIs(string spell)
		{
			if (1 <= _argq.Count && _argq.Peek().ToLower() == spell.ToLower())
			{
				_argq.Dequeue();
				return true;
			}
			return false;
		}

		private static string NextArg()
		{
			return _argq.Dequeue();
		}

		private static void Main2(string[] args)
		{
			_argq = new Queue<string>(args);

			while (ArgIs("/-") == false)
			{
				if (ArgIs("/3"))
				{
					NextArg();
					NextArg();
					NextArg();
					continue;
				}
				break;
			}
		}
	}
}
