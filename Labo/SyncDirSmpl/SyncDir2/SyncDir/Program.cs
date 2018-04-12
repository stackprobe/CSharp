using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace SyncDir
{
	class Program
	{
		static void Main(string[] args)
		{
			try
			{
				new SyncDir().Main(args[0], args[1], args[2]);
			}
			catch (Exception e)
			{
				Logger.WriteLine(e);
			}
		}
	}
}
