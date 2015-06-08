using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte
{
	public class WorkDir
	{
		public const string COMMON_ID = "{9d555494-047d-477c-b6a9-5d5ee730b60d}";
		public static readonly string ROOT_DIR = Path.Combine(SystemTools.GetTmp(), COMMON_ID);
		public static int InstanceCount;
		public string Id;
		public string Dir;

		public WorkDir()
		{
			if (InstanceCount == 0)
			{
				try
				{
					Directory.Delete(ROOT_DIR, true);
				}
				catch
				{ }

				Directory.CreateDirectory(ROOT_DIR);
			}
			this.Id = StringTools.MakeUUID();
			this.Dir = Path.Combine(ROOT_DIR, this.Id);

			Directory.CreateDirectory(this.Dir);
			InstanceCount++;
		}

		public void Destroy()
		{
			Directory.Delete(this.Dir, true);
			InstanceCount--;

			if (InstanceCount == 0)
				Directory.Delete(ROOT_DIR, true);
		}

		public string MakePath()
		{
			return Path.Combine(this.Dir, StringTools.MakeUUID());
		}

		public void Clear()
		{
			Directory.Delete(this.Dir, true);
			Directory.CreateDirectory(this.Dir);
		}
	}
}
