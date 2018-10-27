using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.DDDDTMPL
{
	public class DDDDTMPL0001 // -- 0001
	{
		public string Echo(string message)
		{
			using (WorkingDir wd = new WorkingDir())
			{
				string file = wd.MakePath();

				File.WriteAllText(file, message, Encoding.UTF8);

				message = File.ReadAllText(file, Encoding.UTF8);
			}
			return message;
		}
	}
}
