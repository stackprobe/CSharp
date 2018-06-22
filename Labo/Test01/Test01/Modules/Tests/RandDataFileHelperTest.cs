using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Test01.Modules.Tests
{
	public class RandDataFileHelperTest
	{
		public void Test01()
		{
			string file = @"C:\temp\1.tmp";

			RandDataFileHelper.Make(file, 12300000L, 77777777ul, false);
			if (RandDataFileHelper.CheckHash(file))
				throw null;

			RandDataFileHelper.Make(file, 12300000L, 12300000ul, true);
			if (RandDataFileHelper.CheckHash(file) == false)
				throw null;
		}
	}
}
