using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Charlotte.Tools
{
	public class DirectoryTools
	{
		public delegate void FoundPath_d(string path);

		public static List<string> GetAllPath(string dir, bool dirFlag = true, bool fileFlag = true, bool recursive = true)
		{
			if (dir == null)
				throw new Exception("dir == null");

			if (Directory.Exists(dir) == false)
				throw new Exception("dir not found: " + dir);

			dir = Path.GetFullPath(dir);

			List<string> dest = new List<string>();
			Stack<string> entryDirs = new Stack<string>();
			entryDirs.Push(dir);

			while (1 <= entryDirs.Count)
			{
				dir = entryDirs.Pop();

				foreach (string sDir in Directory.GetDirectories(dir))
				{
					string aDir = StringTools.Combine(dir, Path.GetFileName(sDir));

					if (dirFlag)
						dest.Add(aDir);

					if (recursive)
						entryDirs.Push(aDir);
				}
				foreach (string sFile in Directory.GetFiles(dir))
				{
					string aFile = StringTools.Combine(dir, Path.GetFileName(sFile));

					if (fileFlag)
						dest.Add(aFile);
				}
			}
			return dest;
		}

		public static List<string> GetAllDir(string dir)
		{
			return GetAllPath(dir, true, false);
		}

		public static List<string> GetAllFile(string dir)
		{
			return GetAllPath(dir, false, true);
		}

		public static void Delete(string dir)
		{
			if (dir == null)
				return;

			if (Directory.Exists(dir) == false)
				return;

			List<string> paths = GetAllPath(dir);

			paths.Add(dir);

			foreach (string path in paths)
			{
				FileInfo fi = new FileInfo(path);

				fi.Attributes &= ~FileAttributes.ReadOnly;
			}
			Directory.Delete(dir, true);
		}
	}
}
