using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace KeysMon
{
	class Program
	{
		static void Main(string[] args)
		{
			bool[] last;
			bool[] stat = KeysStat.GetStat();

			for (; ; )
			{
				last = stat;
				stat = KeysStat.GetStat();

				for (int vk = 0; vk <= 255; vk++)
				{
					if (last[vk] && stat[vk] == false)
					{
						Console.WriteLine("U " + vk); // up
					}
					else if (last[vk] == false && stat[vk])
					{
						Console.WriteLine("D " + vk); // down
					}
				}

				Thread.Sleep(100);
				//Thread.Sleep(2000); // GetAsyncKeyStateの間でキー押下・放すしても検出してくれる⇒間隔は長くてもok
			}
		}
	}
}
