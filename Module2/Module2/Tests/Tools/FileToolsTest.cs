using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Charlotte.Tools;
using System.IO;

namespace Charlotte.Tests.Tools
{
	public class FileToolsTest
	{
		public void Test01()
		{
			using (WorkingDir wd = WorkingDir.Root.Create())
			{
				string dir = wd.MakePath();

				DebugPrintTree(wd.GetPath("."));

				MakeRandDir(dir, 3);

				DebugPrintTree(wd.GetPath("."));

				FileTools.CleanupDir(dir);

				DebugPrintTree(wd.GetPath("."));
			}
		}

		private static void MakeRandDir(string dir, int depth)
		{
			FileTools.CreateDir(dir);

			for (int c = (int)SecurityTools.CRandom.GetRandom(20); 0 < c; c--)
			{
				string lPath = SecurityTools.MakePassword();
				string path = Path.Combine(dir, lPath);

				if (SecurityTools.CRandom.GetRandom(2) == 1u && 1 <= depth)
				{
					MakeRandDir(path, depth - 1);
				}
				else
				{
					File.WriteAllBytes(path, SecurityTools.CRandom.GetBytes((int)SecurityTools.CRandom.GetRandom(1000)));
				}
			}
		}

		private static void DebugPrintTree(string dir)
		{
			foreach (string line in ProcessTools.Batch(new string[] { "TREE /F " + dir }))
			{
				Console.WriteLine(line);
			}
		}
	}
}
