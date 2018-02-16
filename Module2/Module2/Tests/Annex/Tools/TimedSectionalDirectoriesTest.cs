using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Annex.Tools;

namespace Charlotte.Tests.Annex.Tools
{
	public class TimedSectionalDirectoriesTest
	{
		public void Test01()
		{
			TimedSectionalDirectories tsd = new TimedSectionalDirectories("{f6b9e94c-fa31-4e62-a325-e08d42c651b6}");
			string file = tsd.GetPath("{cbbf9b35-2656-46f9-ad83-e3818feba62e}");

			Console.WriteLine("file: " + file);
		}
	}
}
