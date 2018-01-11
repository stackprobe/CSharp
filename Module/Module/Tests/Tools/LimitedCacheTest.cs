using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Tools
{
	public class LimitedCacheTest
	{
		public void test01()
		{
			LimitedCache<WorkingDir> _cache = new LimitedCache<WorkingDir>();

			for (int c = 0; c < 1000; c++)
			//for (int c = 0; c < 10000; c++)
			{
				int room = (int)(MathTools.random.getUInt() & 0x0f);

				WorkingDir ret = _cache.get(
					delegate(WorkingDir wd)
					{
						return File.Exists(wd.getPath("" + room));
					},
					delegate
					{
						WorkingDir wd = WorkingDir.root.create();

						FileTools.createFile(wd.getPath("" + room));

						return wd;
					},
					delegate(WorkingDir wd)
					{
						wd.Dispose();
						wd = null;
					}
					);

				if (File.Exists(ret.getPath("" + room)) == false)
					throw null;
			}
			_cache.clear(
				delegate(WorkingDir wd)
				{
					wd.Dispose();
					wd = null;
				}
				);
		}
	}
}
