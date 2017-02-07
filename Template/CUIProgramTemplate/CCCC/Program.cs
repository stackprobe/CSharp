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
					main2(File.ReadAllLines(args[1], Encoding.GetEncoding(932)));
				}
				else
				{
					main2(args);
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

		public const string APP_IDENT = "{22eda4a5-9029-4bf3-b8d8-c687a5729ec3}";

		private static Queue<string> _argq;

		private static bool argIs(string spell)
		{
			if (1 <= _argq.Count && _argq.Peek().ToLower() == spell.ToLower())
			{
				_argq.Dequeue();
				return true;
			}
			return false;
		}

		private static string nextArg()
		{
			return _argq.Dequeue();
		}

		private static void main2(string[] args)
		{
			_argq = new Queue<string>(args);

			while (argIs("/-") == false)
			{
				if (argIs("/3"))
				{
					nextArg();
					nextArg();
					nextArg();
					continue;
				}
				break;
			}
		}
	}
}
